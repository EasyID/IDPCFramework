using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Software.Common;
using Software.Toolkit;
namespace Software.Main
{
    public partial class FrmMain : Form
    {
        #region field

        private static ILog logger = LogManager.GetLogger(typeof(FrmMain));

        private WholeStationData runningData = new WholeStationData();
        private TaskFactory taskFactory = new TaskFactory();
        private ManualResetEventSlim loadSign = new ManualResetEventSlim(false);

        private Thread MachineRunThread = null;

        #endregion

        #region constructor

        public FrmMain()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #endregion

        #region station

        [DeviceNode(typeof(WholeStationView))]
        public StationOne StationOne { get; set; }

        #endregion

        #region param read/write

        /// <summary>
        /// 获取程序参数
        /// </summary>
        public void LoadParam()
        {
            PathDefine.CreatDirectory();

            GlobalParam.Instance = XMLSerializeTool.Load<GlobalParam>(PathDefine.GlobalParamFilePath);
            Recipe.Instance = XMLSerializeTool.Load<Recipe>(PathDefine.CurrentRecipeFilePath);
            Statistics.Instance = XMLSerializeTool.Load<Statistics>(PathDefine.StatisticsFilePath);
        }

        /// <summary>
        /// 保存程序参数
        /// </summary>
        public void SaveParam()
        {
            StationOne.Save();

            XMLSerializeTool.Save(PathDefine.GlobalParamFilePath, GlobalParam.Instance);
            XMLSerializeTool.Save(PathDefine.CurrentRecipeFilePath, Recipe.Instance);
            XMLSerializeTool.Save(PathDefine.StatisticsFilePath, Statistics.Instance);
        }

        #endregion

        #region loading

        private void MainFrm_Load(object sender, EventArgs e)
        {
            Text = NameDefine.StationOne;

            StartPosition = FormStartPosition.Manual;
            Rectangle screen = SystemInformation.VirtualScreen;
            Location = new Point(0, 0);
            Width = screen.Width;
            Height = screen.Height - 40;

            WorkInitialize();
        }

        void WorkInitialize()
        {

            #region 过渡窗体

            Action<string> ShowLoadingState = null;

            taskFactory.StartNew(new Action(() =>
            {
                Form_Loading loading = new Form_Loading(2);
                ShowLoadingState = new Action<string>(loading.ShowState);
                loadSign.Set();
                loading.ShowDialog();
            }));

            WaitHandle.WaitAny(new WaitHandle[] { loadSign.WaitHandle });
            Thread.Sleep(500);

            #endregion

            #region 工位初始化

            ShowLoadingState("工位初始化...");

            StationOne = new StationOne()
            {
                EntityName = NameDefine.StationOne,
                ShowMessage = new Action<string>(ShowMessage)
            };
            StationOne.Initial();
            dgvRunningData.DataSource = StationOne.dataBS;

            #endregion

            #region 加载窗体资源

            ShowLoadingState("加载窗体资源...");
            FormManager.Instance.RegisterForm<FrmConfig>(NameDefine.ConfigFormName, new object[] { this });
            FormManager.Instance.RegisterForm<FrmDatabase>(NameDefine.DatabaseFormName);
            FormManager.Instance.RegisterForm<FrmLogin>(NameDefine.UserLoginFormName);
            FormManager.Instance.RegisterForm<FrmIOMonitor>(NameDefine.IOMonitorFormName, new object[] { this });
            FormManager.Instance.RegisterForm<FrmRecipe>(NameDefine.RecipeFormName);
            FormManager.Instance.RegisterForm<TestTray>(NameDefine.TrayFormName);

            UIRefresh();

            #endregion

            #region 设置表格初始列宽

            dgvRunningData.Columns[NameDefine.OperateDate].Width = 200;
            dgvRunningData.Columns[NameDefine.Remark].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            #endregion

            if (StationOne.IsInitialCompleted)
            {
                ShowMessage("加载完毕...");
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            else
            {
                ShowMessage("加载失败。请根据提示检查相关设备后，再重新启动！");
                btnStart.Enabled = false;
                btnStop.Enabled = false;
            }
        }

        #endregion

        #region closing

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnStop_Click(null, null);
            Thread.Sleep(500);

            try
            {
                SaveParam();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show($"参数保存失败：{ex.Message}", "提示");
            }
        }

        #endregion

        #region menu

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDevice_Click(object sender, EventArgs e)
        {
            logger.Debug("用户进入设备界面");
            FormManager.Instance.GetForm<FrmConfig>(NameDefine.ConfigFormName).ShowDialog();
            UIRefresh();
        }

        private void btnRecipe_Click(object sender, EventArgs e)
        {
            logger.Debug($"用户进入{NameDefine.RecipeFormName}界面");
            FormManager.Instance.GetForm<FrmRecipe>(NameDefine.RecipeFormName).ShowDialog();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            logger.Debug($"用户进入{NameDefine.DatabaseFormName}界面");
            FormManager.Instance.GetForm<FrmDatabase>(NameDefine.DatabaseFormName).ShowDialog();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            logger.Debug($"用户进入{NameDefine.UserLoginFormName}界面");
            FormManager.Instance.GetForm<FrmLogin>(NameDefine.UserLoginFormName).ShowDialog();
            UIRefresh();
        }

        private void btnIOMonitor_Click(object sender, EventArgs e)
        {
            logger.Debug($"用户进入{NameDefine.IOMonitorFormName}界面");
            FormManager.Instance.GetForm<FrmIOMonitor>(NameDefine.IOMonitorFormName).ShowDialog();
        }

        private void BtnTray_Click(object sender, EventArgs e)
        {
            logger.Debug($"用户进入{NameDefine.TrayFormName}界面");
            FormManager.Instance.GetForm<TestTray>(NameDefine.TrayFormName).ShowDialog();
        }

        #endregion

        #region helper

        /// <summary>
        /// 在界面显示信息
        /// </summary>
        public void ShowMessage(string message)
        {
            if (txtWholeStation.InvokeRequired)
            {
                Invoke(new Action<string>(ShowMessage), message);
            }
            else
            {
                txtWholeStation.AppendText(string.Format("{0}   {1}" + Environment.NewLine, DateTime.Now.ToString("MM-dd HH:mm:ss"), message));
                txtWholeStation.ScrollToCaret();
                logger.Info(message);

                if (txtWholeStation.Lines.Length > 3000)
                {
                    txtWholeStation.Clear();
                }
            }
        }

        #endregion

        #region refresh

        byte errorCount = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;

                UIRefresh();

                StationOne.Update();

                errorCount = 0;
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);

                errorCount++;

                if (errorCount > 3)
                {
                    Invoke(new Action(() =>
                    {
                        btnStop_Click(null, EventArgs.Empty);
                    }));
                    ShowMessage(string.Format("程序异常停止:{0}", ex.ToString()));
                }
                else
                {
                    ShowMessage(string.Format("程序检测到异常:{0}", ex.Message));
                }
            }
            finally
            {
                if (Marking.IsRunning)
                {
                    timer1.Enabled = true;
                }
            }
        }

        private void UIRefresh()
        {
            RecipeRefresh();
            StatisticsRefresh();

            if (GlobalParam.Instance.EnableMES)
            {
                lblMES.Text = "已启用";
                lblMES.ForeColor = Color.Green;
            }
            else
            {
                lblMES.Text = $"未启用,默认为{(GlobalParam.Instance.MESShieldResult ? "OK" : "NG")}";
                lblMES.ForeColor = Color.Red;
            }

            if (StationOne.triggerBuffer.GetBit(StationOne.CodeScanShield))
            {
                lblCodereader.Text = "未启用";
                lblCodereader.ForeColor = Color.Red;
            }
            else
            {
                lblCodereader.Text = "已启用";
                lblCodereader.ForeColor = Color.Green;
            }
        }

        private void RecipeRefresh()
        {
            lblMachineNo.Text = GlobalParam.Instance.MachineNo;
            lblUserid.Text = Marking.Userid;
        }

        private void StatisticsRefresh()
        {
            lblSNOK.Text = Statistics.Instance.SNOK.ToString();
            lblSNTotal.Text = Statistics.Instance.SNTotal.ToString();
            lblSNPercent.Text = Statistics.Instance.SNOKPercent.ToString("P3");
        }

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalParam.Instance.EnableMES)
                {
                    if (string.IsNullOrWhiteSpace(cmbFloor.SelectedItem?.ToString()))
                    {
                        MessageBox.Show("楼层号为空，无法启动程序！", "提示");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(cmbLine.SelectedItem?.ToString()))
                    {
                        MessageBox.Show("线体名称为空，无法启动程序！", "提示");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(cmbProjectCode.SelectedItem?.ToString()))
                    {
                        MessageBox.Show("产品型号为空，无法启动程序！", "提示");
                        return;
                    }

                    Marking.ProjectCode = cmbProjectCode.SelectedItem.ToString();
                    Marking.Floor = cmbFloor.SelectedItem.ToString();
                    Marking.Line = cmbLine.SelectedItem.ToString();

                    logger.Debug($"Program Start __ ProjectCode:{Marking.ProjectCode} __ Floor:{Marking.Floor} __ Line:{Marking.Line}");
                }
                else
                {
                    logger.Debug("Program Start __ MES Shield");
                }

                Marking.IsRunning = true;
                timer1.Enabled = true;
                btnStop.Enabled = true;

                btnStart.Enabled = false;
                btnDevice.Enabled = false;
                btnRecipe.Enabled = false;
                btnUser.Enabled = false;

                cmbProjectCode.Enabled = false;
                cmbFloor.Enabled = false;
                cmbLine.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动失败：" + ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            logger.Debug("Program Stop");

            btnStart.Enabled = true;
            btnDevice.Enabled = true;
            btnRecipe.Enabled = true;
            btnUser.Enabled = true;

            cmbProjectCode.Enabled = true;
            cmbFloor.Enabled = true;
            cmbLine.Enabled = true;

            Marking.IsRunning = false;
            timer1.Enabled = false;
            btnStop.Enabled = false;
        }

        private void CmbProjectCode_DropDown(object sender, EventArgs e)
        {
            try
            {
                cmbProjectCode.Items.Clear();
                cmbProjectCode.Items.AddRange(StationOne.mes.GetProjCode().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取产品型号列表失败：{ex.Message}", "提示");
                logger.Warn($"获取产品型号列表失败：{ex.Message}");
            }
        }

        private void CmbFloor_DropDown(object sender, EventArgs e)
        {
            try
            {
                cmbFloor.Items.Clear();
                cmbFloor.Items.AddRange(StationOne.mes.GetPkFloorList().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取楼层号列表失败：{ex.Message}", "提示");
                logger.Warn($"获取楼层号列表失败：{ex.Message}");
            }
        }

        private void CmbLine_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                cmbLine.Items.Clear();
                cmbLine.Items.AddRange(StationOne.mes.GetLineNameList().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取线体编号列表失败：{ex.Message}", "提示");
                logger.Warn($"获取线体编号列表失败：{ex.Message}");
            }
        }

        private void BtnStatisticClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定清除统计数据吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                logger.Debug("用户清除统计数据");
                Statistics.Instance.SNOK = 0;
                Statistics.Instance.SNNG = 0;
            }
        }

        private void DgvRunningData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int index;
            DataGridView dgv = sender as DataGridView;
            if (dgv != null)
            {
                for (int i = 0; i < e.RowCount; i++)
                {
                    index = i + e.RowIndex;
                    DataGridViewRow selectRow = dgv.Rows[index];
                    if (selectRow.Cells.Count == StationOne.testDataTable.Columns.Count
                        && selectRow.Cells[NameDefine.Result].Value?.ToString() == NameDefine.ProductNG)
                    {
                        selectRow.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }
    }
}
