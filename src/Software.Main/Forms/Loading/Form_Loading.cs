using System;
using System.Threading;
using System.Windows.Forms;

namespace Software.Main
{
    public partial class Form_Loading : Form
    {

        private Form_Loading()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public Form_Loading(byte maxValue)
            : this()
        {
            progressBar1.Maximum = maxValue;
            progressBar1.Step = 1;
            label1.Text = "加载中";
        }

        public void ShowState(string str)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ShowState), str);
            }
            else
            {
                label1.Text = str;
                progressBar1.PerformStep();
                if (progressBar1.Value >= progressBar1.Maximum)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

    }
}
