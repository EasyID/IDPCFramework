using System;
using System.Data;
using System.Data.SQLite;
using System.Threading;
using System.Windows.Forms;
using log4net;
using Software.Common;
using Software.Device;
using Software.Toolkit;
using CardClassLib;
using BZClass;
namespace Software.Main
{
    public class StationOne : IStation
    {

        #region PLC sign Define

        #region input

        /// <summary>
        /// 扫码触发
        /// </summary>
        public readonly int CodeScanTrigger = 0;

        /// <summary>
        /// 扫码屏蔽
        /// </summary>
        public readonly int CodeScanShield = 1;

        /// <summary>
        /// 设备复位
        /// </summary>
        public readonly int machineResetTrigger = 30;

        #endregion

        #region output

        /// <summary>
        /// 扫码OK
        /// </summary>
        public readonly int CodeScanOK = 0;

        /// <summary>
        /// 扫码NG
        /// </summary>
        public readonly int CodeScanNG = 1;

        /// <summary>
        /// 上位机与机器人通讯失败
        /// </summary>
        public readonly int RobotConnectError = 16;

        /// <summary>
        /// 上位机与AGV通讯失败
        /// </summary>
        public readonly int AGVConnectError = 17;

        /// <summary>
        /// 复位完成
        /// </summary>
        public readonly int machineResetOK = 30;

        /// <summary>
        /// 心跳信号
        /// </summary>
        public readonly int Heartbeat = 31;

        #endregion

        #endregion

        #region field

        private static ILog logger = LogManager.GetLogger(typeof(StationOne));

        public Action<string> ShowMessage;

        private Thread robotListenThread = null;

        private WindowsFormsSynchronizationContext SynchronizationContext;
        private SendOrPostCallback InsertDataViewDelegate;

        private IAsyncResult StationResetAsyncResult = null;
        private IAsyncResult CodeScanAsyncResult = null;

        public WholeStationData runningData = new WholeStationData();
        public DataTable testDataTable = new DataTable();
        public BindingSource dataBS = new BindingSource();

        public MitsubishiBuffer triggerBuffer = new MitsubishiBuffer("M4000", 2);
        public MitsubishiBuffer resultBuffer = new MitsubishiBuffer("M4032", 2);
        public MitsubishiBuffer readDataBuffer = new MitsubishiBuffer("D4000", 8);
        public MitsubishiBuffer writeDataBuffer = new MitsubishiBuffer("D4016", 8);

        public CardClass1 card = new CardClass1();
        public PKSLJ mes = new PKSLJ();

        #endregion

        #region property

        public bool IsInitialCompleted { get; set; }

        public string EntityName { get; set; }

        public string Description { get; set; }

        #endregion

        #region part

        [DeviceNode(typeof(SerialPortDeviceView))]
        public Honeywell CodeReader { get; set; }

        [DeviceNode(typeof(NetworkDeviceView))]
        public Robot Robot { get; set; }

        public MitsubishiPLC PLC { get; set; }

        #endregion

        #region constructor

        public StationOne() { }

        #endregion

        #region loading

        /// <summary>
        /// 工位初始化
        /// </summary>
        public void Initial()
        {

            IsInitialCompleted = true;

            #region 同步线程上下文

            SynchronizationContext = new WindowsFormsSynchronizationContext();

            InsertDataViewDelegate = new SendOrPostCallback(InsertDataView);

            #endregion

            #region 部件初始化

            try
            {
                CodeReader = new Honeywell()
                {
                    EntityName = NameDefine.WholeCodeReader,
                    Param = GlobalParam.Instance.CodeReaderConnectionParam
                };
                CodeReader.Start();
            }
            catch (Exception ex)
            {
                ShowMessage($"{NameDefine.StationOne}{NameDefine.WholeCodeReader}连接失败：{ex.Message}");
                IsInitialCompleted = false;
            }

            try
            {
                Robot = new Robot()
                {
                    EntityName = NameDefine.Robot,
                    Param = GlobalParam.Instance.RobotConnectionParam
                };
                //Robot.Start();

                //RobotListenThread = new Thread(RobotListen);
                //RobotListenThread.IsBackground = true;
                //RobotListenThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessage($"{NameDefine.StationOne}{NameDefine.Robot}连接失败：{ex.Message}");
                IsInitialCompleted = false;
            }

            try
            {
                PLC = new MitsubishiPLC()
                {
                    EntityName = NameDefine.PLC,
                    Param = GlobalParam.Instance.PLCConnectionParam
                };
                PLC.Connect();
            }
            catch (Exception ex)
            {
                ShowMessage($"{EntityName}{NameDefine.PLC}连接失败：{ex.Message}");
                IsInitialCompleted = false;
            }

            try
            {
                card.MachineInfo("0");
            }
            catch (Exception ex)
            {
                ShowMessage($"上传开机历史失败：{ex.Message}");
                IsInitialCompleted = false;
            }

            #endregion

            #region 本地数据存储初始化

            testDataTable = new DataTable();
            testDataTable.Columns.Add(new DataColumn("ID", typeof(int))
            {
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            });
            testDataTable.Columns.Add(NameDefine.SN);
            testDataTable.Columns.Add(NameDefine.Result);
            testDataTable.Columns.Add(NameDefine.OperateDate);
            testDataTable.Columns.Add(NameDefine.Remark);

            dataBS.DataSource = testDataTable;

            #endregion

            #region 恢复运行数据

            try
            {
                runningData = (WholeStationData)BinarySerializeTool.DeserializationFromTxt(PathDefine.WholeStationCacheFilePath);
            }
            catch (Exception ex)
            {
                ShowMessage(string.Format("已将{0}运行数据设置为初始值，原因：{1}", NameDefine.StationOne, ex.Message));
            }

            #endregion

            #region 启动工作流

            StationResetAsyncResult = new Action(StationResetWorkflow).BeginInvoke(null, null);
            CodeScanAsyncResult = new Action(CodeScanWorkflow).BeginInvoke(null, null);

            #endregion

        }

        #endregion

        #region closing

        /// <summary>
        ///工位保存
        /// </summary>
        public void Save()
        {
            GlobalParam.Instance.CodeReaderConnectionParam = CodeReader?.Param;

            BinarySerializeTool.SerializeToTxt(PathDefine.WholeStationCacheFilePath, runningData);

            card.MachineInfo("1");
        }

        #endregion

        #region workflow

        private ProductInfo product;
        public void Update()
        {
            resultBuffer.SetBit(Heartbeat, true);

            //  Note:
            //  先读触点，再读数据
            //  先写数据，再写触点

            //PLC刷新
            PLC.ReadDeviceBlock(ref triggerBuffer);
            PLC.ReadDeviceBlock(ref readDataBuffer);
            PLC.WriteDeviceBlock(ref writeDataBuffer);
            PLC.WriteDeviceBlock(ref resultBuffer);
        }

        private StationOneResetStep resetStep = 0;
        public void StationResetWorkflow()
        {
            while (true)
            {
                Thread.Sleep(100);

                switch (resetStep)
                {
                    case StationOneResetStep.等待PLC信号ON:
                        if (triggerBuffer.GetBit(machineResetTrigger))
                        {
                            ShowMessage("复位扫码工作流...");

                            resetStep = StationOneResetStep.复位读码工作流;
                        }
                        else
                        {
                            resultBuffer.SetBit(machineResetOK, false);
                        }
                        break;
                    case StationOneResetStep.复位读码工作流:
                        codeScanStep = StationOneCodeScanStep.等待PLC信号ON;
                        ShowMessage("写入复位结果...");

                        resetStep = StationOneResetStep.向PLC写入结果;
                        break;
                    case StationOneResetStep.向PLC写入结果:
                        resultBuffer.SetBit(machineResetOK, true);
                        ShowMessage("等待复位信号OFF...");

                        resetStep = StationOneResetStep.等待PLC信号OFF;
                        break;
                    case StationOneResetStep.等待PLC信号OFF:
                        if (!triggerBuffer.GetBit(machineResetTrigger))
                        {
                            ShowMessage("复位工作流结束...");
                            resultBuffer.SetBit(machineResetOK, false);

                            resetStep = StationOneResetStep.等待PLC信号ON;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private StationOneCodeScanStep codeScanStep;
        public void CodeScanWorkflow()
        {
            string errorMsg = "";

            while (true)
            {
                Thread.Sleep(100);

                if (Marking.IsRunning && resetStep == StationOneResetStep.等待PLC信号ON)
                {
                    switch (codeScanStep)
                    {
                        case StationOneCodeScanStep.等待PLC信号ON:
                            if (triggerBuffer.GetBit(CodeScanTrigger) && !triggerBuffer.GetBit(CodeScanShield))
                            {
                                ShowMessage("开始执行扫码流程");

                                codeScanStep = StationOneCodeScanStep.初始化产品信息;
                            }
                            else
                            {
                                resultBuffer.SetBit(CodeScanOK, false);
                                resultBuffer.SetBit(CodeScanNG, false);
                            }
                            break;
                        case StationOneCodeScanStep.初始化产品信息:
                            product.Clear();
                            errorMsg = "";

                            codeScanStep = StationOneCodeScanStep.读取条码;
                            break;
                        case StationOneCodeScanStep.读取条码:
                            try
                            {
                                string result = CodeReader.Trigger();

                                if (result.Contains(ResultFlags.Error))
                                {
                                    Statistics.Instance.SNNG++;
                                    throw new Exception(result);
                                }

                                if (GlobalParam.Instance.EnableSNHeadControl && !SNMatchStrategy.HeadMatch(result, GlobalParam.Instance.SNHeadMatchRule))
                                {
                                    Statistics.Instance.SNNG++;
                                    throw new Exception($"二维码 {result} 不符合设置的前端匹配内容 {GlobalParam.Instance.SNHeadMatchRule} ");
                                }

                                if (GlobalParam.Instance.EnableSNLengthControl && !SNMatchStrategy.LengthMatch(result, GlobalParam.Instance.SNLengthMatchRule))
                                {
                                    Statistics.Instance.SNNG++;
                                    throw new Exception($"二维码 {result} 不符合设置的长度匹配内容 {GlobalParam.Instance.SNLengthMatchRule} ");
                                }

                                if (GlobalParam.Instance.EnableSNConstLengthControl)
                                {
                                    result = SNMatchStrategy.CreatConstLength(result, GlobalParam.Instance.SNConstLengthRule);
                                }

                                product.sn = result;
                                Statistics.Instance.SNOK++;
                                ShowMessage($"获取托盘码：{result}");

                                codeScanStep = StationOneCodeScanStep.向PLC写入结果;
                            }
                            catch (Exception ex)
                            {
                                errorMsg = ex.Message;

                                codeScanStep = StationOneCodeScanStep.异常处理;
                            }
                            break;
                        case StationOneCodeScanStep.向PLC写入结果:
                            SynchronizationContext.Send(InsertDataViewDelegate, product);

                            resultBuffer.SetBit(CodeScanOK, product.result);
                            resultBuffer.SetBit(CodeScanNG, !product.result);

                            codeScanStep = StationOneCodeScanStep.等待PLC信号OFF;
                            break;
                        case StationOneCodeScanStep.等待PLC信号OFF:
                            {
                                if (!triggerBuffer.GetBit(CodeScanTrigger))
                                {
                                    resultBuffer.SetBit(CodeScanOK, false);
                                    resultBuffer.SetBit(CodeScanNG, false);

                                    codeScanStep = StationOneCodeScanStep.等待PLC信号ON;
                                }
                            }
                            break;
                        default:
                            ShowMessage($"扫码流程异常：{errorMsg}");

                            if (!string.IsNullOrWhiteSpace(product.sn))
                            {
                                product.remark = errorMsg;
                                SynchronizationContext.Send(InsertDataViewDelegate, product);
                            }

                            resultBuffer.SetBit(CodeScanOK, false);
                            resultBuffer.SetBit(CodeScanNG, true);

                            codeScanStep = StationOneCodeScanStep.等待PLC信号OFF;
                            break;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 机器人轮询
        /// </summary>
        public void RobotListen()
        {
            bool Pulse = false;

            while (true)
            {
                if (Marking.IsRunning)
                {
                    if (!Robot.Connected || resultBuffer.GetBit(RobotConnectError))
                    {
                        if (!Pulse)
                        {
                            ShowMessage("检测到机器人通讯已断开！");
                            Pulse = true;
                        }

                        Robot.Start();

                        Thread.Sleep(1000);

                        resultBuffer.SetBit(RobotConnectError, false);
                    }
                    else
                    {
                        if (Pulse)
                        {
                            ShowMessage("已重新连接到机器人！");
                            Pulse = false;
                        }

                        Thread.Sleep(200);
                    }
                }
            }

        }

        /// <summary>
        /// 向表格添加产品数据
        /// </summary>
        public void InsertDataView(object state)
        {
            ProductInfo product = (ProductInfo)state;
            product.operateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            DataRow row = testDataTable.NewRow();

            row[NameDefine.SN] = product.sn;
            row[NameDefine.Result] = product.result ? NameDefine.ProductOK : NameDefine.ProductNG;
            row[NameDefine.OperateDate] = product.operateDate;
            row[NameDefine.Remark] = product.remark;

            testDataTable.Rows.Add(row);

            if (testDataTable.Rows.Count > 500)
            {
                testDataTable.Rows.RemoveAt(0);
            }

            dataBS.MoveLast();

            logger.Debug("添加至数据表成功");

            using (SQLiteConnection conn = new SQLiteConnection($"data source={PathDefine.DataBaseFilePath}"))
            {
                conn.Open();

                SQLiteCommand cmd = conn.CreateCommand();

                cmd.CommandText = $"create table IF NOT EXISTS [{NameDefine.SQLTableName}] ("
                                + $"[ID] integer primary key autoincrement NOT NULL,"
                                + $"[{NameDefine.SN}] Text,"
                                + $"[{NameDefine.Result}] Text,"
                                + $"[{NameDefine.OperateDate}] DATETIME,"
                                + $"[{NameDefine.Remark}] Text)";
                cmd.ExecuteNonQuery();


                cmd.CommandText = $"insert into [{NameDefine.SQLTableName}] values("
                                + $"NULL,"
                                + $"'{product.sn}',"
                                + $"'{(product.result ? NameDefine.ProductOK : NameDefine.ProductNG)}',"
                                + $"'{product.operateDate}',"
                                + $"'{product.remark}');";
                cmd.ExecuteNonQuery();

            }

            logger.Debug("添加至本地数据库成功！");
        }

    }
}
