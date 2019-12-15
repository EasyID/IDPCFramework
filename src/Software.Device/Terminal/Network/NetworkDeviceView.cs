using System;
using System.Net.Sockets;
using System.Windows.Forms;
using Software.Common;

namespace Software.Device
{

    public partial class NetworkDeviceView : UserControl
    {
        #region field

        private Terminal terminal;

        #endregion

        #region constructor

        private NetworkDeviceView()
        {
            InitializeComponent();
        }

        public NetworkDeviceView(Terminal terminal) : this()
        {
            this.terminal = terminal;

            if (this.terminal.Mode == TerminalMode.Async)
            {
                this.terminal.DataReceived += DataReceived;
            }
        }

        #endregion

        #region loading

        private void TerminalDeviceView_Load(object sender, EventArgs e)
        {
            txtInfo.Text = "";
            txtSend.Text = "";

            GetParam();

            SetButtonState(terminal.IsStart);

            ShowMessage(terminal.Execute("HELP"));

        }

        #endregion

        #region Closing



        #endregion

        #region Verify

        private void txtTryTimes_Leave(object sender, EventArgs e)
        {
            int value = 0;
            if (!int.TryParse(txtTryTimes.Text, out value))
            {
                ShowMessage("测试次数必须为数字，请确认后重试！");
                txtTryTimes.Text = "1";
            }
            else if (value < 1)
            {
                ShowMessage("测试次数必须为大于0的正整数，请确认后重试！");
                txtTryTimes.Text = "1";
            }
        }

        #endregion

        /// <summary>    
        ///     将控件值保存到通讯参数
        /// </summary>
        private void SetParam()
        {
            terminal.Param = txtIP.Text.Trim() + ","
                + txtPortNumber.Text.Trim() + ","
                + txtReadTimeOut.Text.Trim() + ","
                + txtWriteTimeOut.Text.Trim() + ","
                + txtTryTimes.Text.Trim();
        }

        /// <summary>
        ///     将通讯参数更新到控件
        /// </summary>
        private void GetParam()
        {
            string[] connectParams = terminal.Param.Split(',');

            if (connectParams.Length != 5)
            {
                ShowMessage($"更新通讯参数失败：长度{connectParams.Length}不为5");
                return;
            }

            txtIP.Text = connectParams[0];
            txtPortNumber.Text = connectParams[1];
            txtReadTimeOut.Text = connectParams[2];
            txtWriteTimeOut.Text = connectParams[3];
            txtTryTimes.Text = connectParams[4];

        }

        /// <summary>
        ///     在窗体显示消息
        /// </summary>
        private void ShowMessage(string message)
        {
            if (txtInfo.InvokeRequired)
            {
                Invoke(new Action<string>(ShowMessage), message);
            }
            else
            {
                txtInfo.AppendText(message + Environment.NewLine);
            }
        }

        private void SetButtonState(bool isStart)
        {
            btnStart.Enabled = !isStart;
            btnStop.Enabled = isStart;
        }

        private void DataReceived(string content)
        {
            ShowMessage($"获取结果：{content}");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                terminal.Start();
                ShowMessage("启动成功！");
            }
            catch (Exception ex)
            {
                ShowMessage($"启动失败！{ex.Message}");
            }
            finally
            {
                SetButtonState(terminal.IsStart);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                terminal.End();
                ShowMessage("停止成功！");
            }
            catch (Exception ex)
            {
                ShowMessage($"停止失败！{ex.Message}");
            }
            finally
            {
                SetButtonState(terminal.IsStart);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string result = terminal.Execute(txtSend.Text);
                if (terminal.Mode == TerminalMode.Sync)
                {
                    ShowMessage($"获取结果：{result}");
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"发送失败！{ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetParam();
            ShowMessage("参数保存成功");
        }

    }
}
