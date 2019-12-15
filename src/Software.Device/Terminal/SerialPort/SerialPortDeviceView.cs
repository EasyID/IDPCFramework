using System;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;
using Software.Common;

namespace Software.Device
{

    public partial class SerialPortDeviceView : UserControl
    {
        #region field

        private string[] baudRates = { "1200", "2400", "4800", "9600", "14400", "19200", "28800", "38400", "57600", "115200" };
        private string[] dataBits = { "7", "8" };
        private Terminal terminal;

        #endregion

        #region constructor

        private SerialPortDeviceView()
        {
            InitializeComponent();

            cmbPortName.Items.AddRange(SerialPort.GetPortNames());
            cmbBaudRate.Items.AddRange(baudRates);
            cmbDataBit.Items.AddRange(dataBits);
            cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBit.Items.AddRange(Enum.GetNames(typeof(StopBits)).SkipWhile(s => s == "None").ToArray<string>());
        }

        public SerialPortDeviceView(Terminal terminal) : this()
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
            else if (value < 1 && value != -2)
            {
                ShowMessage("测试次数必须为-2或者大于0的正整数，请确认后重试！");
                txtTryTimes.Text = "1";
            }
        }

        #endregion

        /// <summary>
        ///     将控件值保存到通讯参数
        /// </summary>
        public void SetParam()
        {
            terminal.Param = cmbPortName.Text.Trim() + ","
                + cmbBaudRate.Text.Trim() + ","
                + cmbParity.Text.Trim() + ","
                + cmbDataBit.Text.Trim() + ","
                + cmbStopBit.Text.Trim() + ","
                + txtReadTimeOut.Text.Trim() + ","
                + txtWriteTimeOut.Text.Trim() + ","
                + txtTryTimes.Text.Trim();
        }

        /// <summary>
        ///     将通讯参数更新到控件
        /// </summary>
        public void GetParam()
        {
            string[] connectParams = terminal.Param.Split(',');

            if (connectParams.Length != 8)
            {
                ShowMessage($"更新通讯参数失败：长度{connectParams.Length}不为8");
                return;
            }

            cmbPortName.Text = connectParams[0];
            cmbBaudRate.Text = connectParams[1];
            cmbParity.Text = connectParams[2];
            cmbDataBit.Text = connectParams[3];
            cmbStopBit.Text = connectParams[4];
            txtReadTimeOut.Text = connectParams[5];
            txtWriteTimeOut.Text = connectParams[6];
            txtTryTimes.Text = connectParams[7];

        }

        private void ShowMessage(string message)
        {
            if(txtInfo.InvokeRequired)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
