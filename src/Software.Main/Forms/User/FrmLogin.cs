using System;
using System.Windows.Forms;
using log4net;
using Software.Toolkit;
using CardClassLib;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Software.Main
{
    public partial class FrmLogin : Form
    {

        #region field

        static ILog logger = LogManager.GetLogger(typeof(FrmLogin));

        SerialPort sp;
        CardClass1 card = new CardClass1();

        #endregion

        #region 构造函数

        public FrmLogin()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        #endregion

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            GlobalParam.Instance = XMLSerializeTool.Load<GlobalParam>(PathDefine.GlobalParamFilePath);

            txtMachineNo.Text = GlobalParam.Instance.MachineNo;
            txtMachineNo.Enabled = false;

            txtUserid.Text = "";
            txtUserid.Focus();

            OpenPort();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMachineNo.Text))
            {
                ShowMessage("设备编号不能为空,请确认！");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUserid.Text))
            {
                ShowMessage("IC卡卡号不能为空,请确认！");
                return;
            }

            try
            {
                string message = "";
                if (txtUserid.Text != "local")
                {
                    logger.Debug($"IC Card：{txtUserid.Text}");

                    message = card.CardCheck(txtMachineNo.Text, txtUserid.Text, "BZ");

                    logger.Debug($" CardCheck 返回数据：{message}");
                }
                else
                {
                    message = "0,Success,,";
                    logger.Debug("使用本地账户登录");
                }

                string[] results = message.Split(',');

                if (results[0] == "0")
                {
                    Marking.Level = UserLevel.Designer;
                    Marking.Userid = results[2].Length == 0 ? "Local" : results[2];
                    GlobalParam.Instance.MachineNo = txtMachineNo.Text;

                    logger.Info($"用户 {Marking.Userid} 已登录，设备编号：{GlobalParam.Instance.MachineNo}");

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    ShowMessage($"MES校验失败：{message.Substring(2)}");
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"MES校验异常：{ex.Message}");
            }
        }

        private void btnLogoff_Click(object sender, EventArgs e)
        {
            Marking.Level = UserLevel.Operater;

            DialogResult = DialogResult.No;
            Close();
        }

        private void ShowMessage(string content)
        {
            MessageBox.Show(content, "提示");
            logger.Debug(content);
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            txtMachineNo.Enabled = true;
        }

        private void OpenPort()
        {
            try
            {
                string[] param = GlobalParam.Instance.CardReaderConnectionParam.Split(',');

                sp = new SerialPort()
                {
                    PortName = param[0],
                    BaudRate = int.Parse(param[1]),
                    Parity = (Parity)Enum.Parse(typeof(Parity), param[2]),
                    DataBits = int.Parse(param[3]),
                    StopBits = (StopBits)Enum.Parse(typeof(StopBits), param[4])
                };

                sp.Open();
                sp.DataReceived += DataReceived;
                pictureBox1.Image = Properties.Resources.greenSign;
            }
            catch (Exception ex)
            {
                logger.Warn(ex.Message);
                pictureBox1.Image = Properties.Resources.redSign;
            }
        }

        private void ClosePort()
        {
            if (sp != null)
            {
                Application.DoEvents();
                sp.Close();
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosePort();
        }

        public void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            List<byte> receive = new List<byte>();
            while (sp.BytesToRead > 0)
            {
                receive.Add((byte)sp.ReadByte());
            }

            byte[] bytes = new byte[4];
            Array.Copy(receive.ToArray(), 7, bytes, 0, 4);
            string cardNum = BitConverter.ToUInt32(bytes.ToArray(), 0).ToString();

            //需要在新线程中操作，防止关闭串口时被阻塞
            new TaskFactory().StartNew(() =>
            {
                Invoke(new Action(() =>
                {
                    txtUserid.Text = cardNum;
                    btnLogin_Click(null, null);
                }));
            });
        }
    }
}
