using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Software.Contract;
using Software.Device;
namespace Software.Main
{
    public partial class FrmManualScan : Form
    {
        public delegate void ScanCompleteEventHandle(string sn);
        public event ScanCompleteEventHandle ScanCompleteEvent;
        public string SN;

        private Terminal device;

        public FrmManualScan(Terminal device)
        {
            InitializeComponent();

            this.device = device;
            this.device.DataReceived += CodeReceived;
        }

        private void FrmManualScan_Load(object sender, EventArgs e)
        {
            txtSN.Text = "";
        }

        private void CodeReceived(string content)
        {
            SN = content;
            txtSN.Text = SN;
            btnConfirm_Click(this, EventArgs.Empty);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Visible && Regex.IsMatch(txtSN.Text, GlobalParam.Instance.CodeRule))
            {
                DialogResult = DialogResult.Yes;
                ScanCompleteEvent?.Invoke(SN);
                Hide();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定取消吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DialogResult = DialogResult.No;
                ScanCompleteEvent?.Invoke(SN);
                Hide();
            }
        }

    }
}
