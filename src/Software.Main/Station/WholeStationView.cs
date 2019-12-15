using System;
using System.Windows.Forms;

namespace Software.Main
{
    public partial class WholeStationView : UserControl
    {

        private StationOne station;

        public WholeStationView(StationOne station)
        {
            InitializeComponent();
            this.station = station;
        }

        private void WholeStationView_Load(object sender, EventArgs e)
        {

            chkEnableMES.Checked = GlobalParam.Instance.EnableMES;
            cmbMESShieldResult.SelectedIndex = GlobalParam.Instance.MESShieldResult ? 0 : 1;
            chkHeadMatch.Checked = GlobalParam.Instance.EnableSNHeadControl;
            chkLengthMatch.Checked = GlobalParam.Instance.EnableSNLengthControl;
            chkEnableConstLength.Checked = GlobalParam.Instance.EnableSNConstLengthControl;

            txtHeadMatch.Text = GlobalParam.Instance.SNHeadMatchRule;

            int length = GlobalParam.Instance.SNLengthMatchRule;
            nudLengthMatch.Value = length > 0 ? length : 1;

            int constLength = GlobalParam.Instance.SNConstLengthRule;
            nudEnableConstLength.Value = constLength > 0 ? constLength : 1;
        }

        private void WholeStationView_Leave(object sender, EventArgs e)
        {
            if (chkHeadMatch.Checked && string.IsNullOrWhiteSpace(txtHeadMatch.Text))
            {
                MessageBox.Show("在启用前端匹配的情况下，匹配内容不能为空！");
                return;
            }

            GlobalParam.Instance.EnableMES = chkEnableMES.Checked;
            GlobalParam.Instance.MESShieldResult = cmbMESShieldResult.SelectedIndex == 0 ? true : false;
            GlobalParam.Instance.EnableSNHeadControl = chkHeadMatch.Checked;
            GlobalParam.Instance.EnableSNLengthControl = chkLengthMatch.Checked;
            GlobalParam.Instance.EnableSNConstLengthControl = chkEnableConstLength.Checked;

            GlobalParam.Instance.SNHeadMatchRule = txtHeadMatch.Text;
            GlobalParam.Instance.SNLengthMatchRule = (int)nudLengthMatch.Value;
            GlobalParam.Instance.SNConstLengthRule = (int)nudEnableConstLength.Value;
        }

        private void ChkEnableMES_CheckedChanged(object sender, EventArgs e)
        {
            cmbMESShieldResult.Visible = !chkEnableMES.Checked;
        }
    }
}
