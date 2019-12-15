using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Software.Device;
namespace Software.Main
{
    public partial class IOSignView : UserControl
    {
        #region field

        Func<bool> refreshAction;

        #endregion

        #region constructor

        public IOSignView()
        {
            InitializeComponent();
        }

        public IOSignView(Func<bool> refreshAction, string deviceName,string description) : this()
        {
            this.refreshAction = refreshAction;
            lblName.Text = deviceName;
            lblDes.Text = description;
        }

        #endregion

        public void RefreshState()
        {
            pic.Image = refreshAction() ? Properties.Resources.greenSign : Properties.Resources.redSign;
        }
    }
}
