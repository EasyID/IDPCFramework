using System;
using System.Windows.Forms;

namespace Software.Main
{
    public partial class FrmIOMonitor : Form
    {
        private IOSignView[] input0, input1;
        private IOSignView[] output0, output1;
        private bool enableRunning;

        public FrmIOMonitor(FrmMain main)
        {
            InitializeComponent();

            input0 = new IOSignView[]
            {
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(0); }),"M4000","扫码触发"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(1); }),"M4001","扫码屏蔽"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(2); }),"M4002","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(3); }),"M4003","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(4); }),"M4004","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(5); }),"M4005","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(6); }),"M4006","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(7); }),"M4007","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(8); }),"M4008","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(9); }),"M4009","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(10); }),"M4010","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(11); }),"M4011","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(12); }),"M4012","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(13); }),"M4013","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(14); }),"M4014","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(15); }),"M4015","备用")
            };

            input1 = new IOSignView[]
            {
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(16); }),"M4016","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(17); }),"M4017","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(18); }),"M4018","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(19); }),"M4019","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(20); }),"M4020","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(21); }),"M4021","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(22); }),"M4022","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(23); }),"M4023","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(24); }),"M4024","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(25); }),"M4025","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(26); }),"M4026","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(27); }),"M4027","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(28); }),"M4028","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(29); }),"M4029","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(30); }),"M4030","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.triggerBuffer.GetBit(31); }),"M4031","备用")
            };

            output0 = new IOSignView[]
            {
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(0); }),"M4032","上料扫码OK"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(1); }),"M4033","上料扫码NG"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(2); }),"M4034","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(3); }),"M4035","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(4); }),"M4036","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(5); }),"M4037","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(6); }),"M4038","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(7); }),"M4039","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(8); }),"M4040","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(9); }),"M4041","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(10); }),"M4042","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(11); }),"M4043","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(12); }),"M4044","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(13); }),"M4045","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(14); }),"M4046","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(15); }),"M4047","备用"),
            };

            output1 = new IOSignView[]
{
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(16); }),"M4048","上位机与机器人通讯失败"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(17); }),"M4049","上位机与AGV通讯失败"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(18); }),"M4050","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(19); }),"M4051","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(20); }),"M4052","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(21); }),"M4053","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(22); }),"M4054","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(23); }),"M4055","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(24); }),"M4056","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(25); }),"M4057","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(26); }),"M4058","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(27); }),"M4059","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(28); }),"M4060","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(29); }),"M4061","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(30); }),"M4062","备用"),
                new IOSignView(new Func<bool>(()=>{ return main.StationOne.resultBuffer.GetBit(31); }),"M4063","心跳信号"),
};

            gbxChainInput0.Controls.AddRange(input0);
            gbxChainInput1.Controls.AddRange(input1);
            gbxChainOutput0.Controls.AddRange(output0);
            gbxChainOutput1.Controls.AddRange(output1);
        }

        private void FrmIOmonitor_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            enableRunning = true;
        }

        private void FrmIOMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            enableRunning = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    Array.ForEach(input0, s => s.RefreshState());
                    Array.ForEach(input1, s => s.RefreshState());
                    break;
                case 1:
                    Array.ForEach(output0, s => s.RefreshState());
                    Array.ForEach(output1, s => s.RefreshState());
                    break;
                default: break;
            }

            if (enableRunning)
            {
                timer1.Enabled = true;
            }
        }
    }
}
