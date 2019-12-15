using System;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using log4net;
using Software.Common;

namespace Software.Device
{
    public class SerialPortDevice : Terminal
    {

        #region field

        private static ILog logger = LogManager.GetLogger(typeof(SerialPortDevice));

        private Thread DataListenThread;
        private ManualResetEventSlim threadStatus = new ManualResetEventSlim();

        private SerialPort serialPort = new SerialPort();
        private bool IsClosing;

        #endregion

        #region constructor

        public SerialPortDevice(TerminalMode mode = TerminalMode.Sync)
        {
            Mode = mode;
        }

        #endregion

        public override void Start()
        {
            serialPort = new SerialPort();

            //分配参数
            string[] param = Param.Split(',');
            serialPort.PortName = param[0];
            serialPort.BaudRate = int.Parse(param[1]);
            serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), param[2]);
            serialPort.DataBits = int.Parse(param[3]);
            serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), param[4]);
            serialPort.ReadTimeout = int.Parse(param[5]);
            serialPort.WriteTimeout = int.Parse(param[6]);
            TryTimes = int.Parse(Param.Split(',')[7]);

            //启动操作
            serialPort.Open();
            IsStart = true;

            //根据模式设置行为
            switch (Mode)
            {
                case TerminalMode.Async:
                    DataListenThread = new Thread(ReceiveBehavior);
                    DataListenThread.IsBackground = true;
                    DataListenThread.Start();
                    break;
                default:
                    break;
            }

        }

        public override void End()
        {

            IsClosing = true;

            //等待线程关闭
            if (DataListenThread != null && !threadStatus.Wait(20000))
            {
                DataListenThread.Abort();
            }
            threadStatus.Reset();

            Application.DoEvents();
            serialPort?.Close();

            IsStart = false;
            IsClosing = false;
        }

        [ExecuteInfo("TRG", "开始读码", "TRG")]
        public string Trigger(object param = null)
        {
            string receiveString = "";

            switch (Mode)
            {
                case TerminalMode.Async:
                    {
                        try
                        {
                            serialPort.Write("+\r");
                        }
                        catch (Exception ex)
                        {
                            logger.Debug(string.Format("Trigger Error：{0}", ex.Message));
                            receiveString = ResultFlags.Error + ex.Message;
                        }
                    }
                    break;
                default:
                    {
                        serialPort.Write("-\r");

                        for (int i = 0; i < TryTimes; i++)
                        {
                            try
                            {
                                receiveString = "";
                                serialPort.DiscardInBuffer();
                                serialPort.Write("+\r\n");
                                receiveString = serialPort.ReadTo("\r\n").Trim();

                                if (string.IsNullOrEmpty(receiveString))
                                    throw new Exception("图像解析失败");

                                break;
                            }
                            catch (Exception ex)
                            {
                                logger.Debug(string.Format("Trigger Error：{0}", ex.Message));
                                receiveString = ResultFlags.Error + ex.Message;
                            }
                            finally
                            {
                                serialPort.Write("-\r");
                            }
                        }
                    }
                    break;
            }

            return receiveString;
        }

        private void ReceiveBehavior()
        {
            string result = "";
            while (true)
            {
                try
                {
                    if (IsClosing)
                    {
                        break;
                    }

                    result = serialPort.ReadTo("\r").Trim();
                    RaiseDataReceivedEvent(result);
                }
                catch (Exception ex)
                {
                    if (ex.GetType() != typeof(TimeoutException))
                    {
                        logger.Debug(ex);
                        RaiseDataReceivedEvent(ResultFlags.Error + ex.Message);
                        break;
                    }
                }
            }
            threadStatus.Set();
        }

        public override string Execute(string content)
        {
            string result = "";
            content = content.Trim().ToUpper();

            if (Regex.IsMatch(content, "^HELP"))
            {
                Attribute[] attributes = GetType().GetMethods()
                    .Select(s => Attribute.GetCustomAttribute(s, typeof(ExecuteInfoAttribute)))
                    .Where(s => s != null).ToArray();

                result = Environment.NewLine + "-----------------------------------" + Environment.NewLine;
                result += Environment.NewLine + "该设备支持以下指令：" + Environment.NewLine;
                foreach (Attribute attribute in attributes)
                {
                    ExecuteInfoAttribute executeInfo = (ExecuteInfoAttribute)attribute;
                    result += Environment.NewLine + executeInfo.Command + " - " + executeInfo.Description + Environment.NewLine
                        + "示例：" + executeInfo.Example + Environment.NewLine;
                }
                result += Environment.NewLine + "-----------------------------------" + Environment.NewLine;
            }
            else if (Regex.IsMatch(content, "^TRG"))
            {
                if (content.Length > 3)
                {
                    string cmd = content.Substring(3);
                    result = Trigger(cmd);
                }
                else
                {
                    result = "文本长度不正确，请重新输入！";
                }
            }
            else
            {
                result = "不支持指令" + content;
            }
            return result;
        }

    }
}
