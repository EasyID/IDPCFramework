namespace Software.Main
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvRunningData = new System.Windows.Forms.DataGridView();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.btnDevice = new System.Windows.Forms.ToolStripButton();
            this.btnRecipe = new System.Windows.Forms.ToolStripButton();
            this.btnTray = new System.Windows.Forms.ToolStripButton();
            this.btnHistory = new System.Windows.Forms.ToolStripButton();
            this.btnIOMonitor = new System.Windows.Forms.ToolStripButton();
            this.btnUser = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtWholeStation = new System.Windows.Forms.TextBox();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbLine = new System.Windows.Forms.ComboBox();
            this.cmbFloor = new System.Windows.Forms.ComboBox();
            this.lblMachineNo = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbProjectCode = new System.Windows.Forms.ComboBox();
            this.lblSNPercent = new System.Windows.Forms.Label();
            this.lblSNTotal = new System.Windows.Forms.Label();
            this.lblSNOK = new System.Windows.Forms.Label();
            this.lblCodereader = new System.Windows.Forms.Label();
            this.lblMES = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStatisticClear = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblUserid = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunningData)).BeginInit();
            this.toolStripMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tabControl1, 1, 1);
            this.tlpMain.Controls.Add(this.toolStripMenu, 0, 0);
            this.tlpMain.Controls.Add(this.groupBox1, 1, 2);
            this.tlpMain.Controls.Add(this.pnlLeft, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpMain.Size = new System.Drawing.Size(939, 581);
            this.tlpMain.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(253, 103);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(683, 325);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvRunningData);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(675, 299);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "历史数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvRunningData
            // 
            this.dgvRunningData.AllowUserToAddRows = false;
            this.dgvRunningData.AllowUserToDeleteRows = false;
            this.dgvRunningData.AllowUserToResizeRows = false;
            this.dgvRunningData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRunningData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRunningData.Location = new System.Drawing.Point(3, 3);
            this.dgvRunningData.Name = "dgvRunningData";
            this.dgvRunningData.ReadOnly = true;
            this.dgvRunningData.RowTemplate.Height = 23;
            this.dgvRunningData.Size = new System.Drawing.Size(669, 293);
            this.dgvRunningData.TabIndex = 1;
            this.dgvRunningData.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DgvRunningData_RowsAdded);
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.AutoSize = false;
            this.toolStripMenu.BackColor = System.Drawing.Color.Transparent;
            this.toolStripMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStripMenu.BackgroundImage")));
            this.tlpMain.SetColumnSpan(this.toolStripMenu, 2);
            this.toolStripMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDevice,
            this.btnRecipe,
            this.btnTray,
            this.btnHistory,
            this.btnIOMonitor,
            this.btnUser,
            this.btnExit});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenu.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripMenu.Size = new System.Drawing.Size(939, 100);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // btnDevice
            // 
            this.btnDevice.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnDevice.Image = ((System.Drawing.Image)(resources.GetObject("btnDevice.Image")));
            this.btnDevice.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDevice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDevice.Margin = new System.Windows.Forms.Padding(30, 5, 2, 5);
            this.btnDevice.Name = "btnDevice";
            this.btnDevice.Size = new System.Drawing.Size(69, 90);
            this.btnDevice.Text = "设备配置";
            this.btnDevice.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDevice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDevice.Click += new System.EventHandler(this.btnDevice_Click);
            // 
            // btnRecipe
            // 
            this.btnRecipe.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnRecipe.Image = ((System.Drawing.Image)(resources.GetObject("btnRecipe.Image")));
            this.btnRecipe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRecipe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecipe.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.btnRecipe.Name = "btnRecipe";
            this.btnRecipe.Size = new System.Drawing.Size(69, 90);
            this.btnRecipe.Text = "配方设置";
            this.btnRecipe.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRecipe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRecipe.Visible = false;
            this.btnRecipe.Click += new System.EventHandler(this.btnRecipe_Click);
            // 
            // btnTray
            // 
            this.btnTray.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnTray.Image = ((System.Drawing.Image)(resources.GetObject("btnTray.Image")));
            this.btnTray.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnTray.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTray.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.btnTray.Name = "btnTray";
            this.btnTray.Size = new System.Drawing.Size(69, 90);
            this.btnTray.Text = "托盘设置";
            this.btnTray.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTray.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTray.Visible = false;
            this.btnTray.Click += new System.EventHandler(this.BtnTray_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnHistory.Image = ((System.Drawing.Image)(resources.GetObject("btnHistory.Image")));
            this.btnHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHistory.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(69, 90);
            this.btnHistory.Text = "历史数据";
            this.btnHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnIOMonitor
            // 
            this.btnIOMonitor.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnIOMonitor.Image = ((System.Drawing.Image)(resources.GetObject("btnIOMonitor.Image")));
            this.btnIOMonitor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnIOMonitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIOMonitor.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.btnIOMonitor.Name = "btnIOMonitor";
            this.btnIOMonitor.Size = new System.Drawing.Size(68, 90);
            this.btnIOMonitor.Text = "IO监控";
            this.btnIOMonitor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnIOMonitor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnIOMonitor.Click += new System.EventHandler(this.btnIOMonitor_Click);
            // 
            // btnUser
            // 
            this.btnUser.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnUser.Image = ((System.Drawing.Image)(resources.GetObject("btnUser.Image")));
            this.btnUser.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUser.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(69, 90);
            this.btnUser.Text = "用户登录";
            this.btnUser.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(68, 90);
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtWholeStation);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(253, 434);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(683, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运行日志";
            // 
            // txtWholeStation
            // 
            this.txtWholeStation.BackColor = System.Drawing.SystemColors.Window;
            this.txtWholeStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWholeStation.Location = new System.Drawing.Point(3, 17);
            this.txtWholeStation.Multiline = true;
            this.txtWholeStation.Name = "txtWholeStation";
            this.txtWholeStation.ReadOnly = true;
            this.txtWholeStation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWholeStation.Size = new System.Drawing.Size(677, 124);
            this.txtWholeStation.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.groupBox3);
            this.pnlLeft.Controls.Add(this.groupBox2);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 100);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLeft.Name = "pnlLeft";
            this.tlpMain.SetRowSpan(this.pnlLeft, 2);
            this.pnlLeft.Size = new System.Drawing.Size(250, 481);
            this.pnlLeft.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblUserid);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cmbLine);
            this.groupBox3.Controls.Add(this.cmbFloor);
            this.groupBox3.Controls.Add(this.lblMachineNo);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.cmbProjectCode);
            this.groupBox3.Controls.Add(this.lblSNPercent);
            this.groupBox3.Controls.Add(this.lblSNTotal);
            this.groupBox3.Controls.Add(this.lblSNOK);
            this.groupBox3.Controls.Add(this.lblCodereader);
            this.groupBox3.Controls.Add(this.lblMES);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 110);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 371);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "信息显示";
            // 
            // cmbLine
            // 
            this.cmbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLine.FormattingEnabled = true;
            this.cmbLine.Location = new System.Drawing.Point(89, 102);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(145, 20);
            this.cmbLine.TabIndex = 19;
            this.cmbLine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CmbLine_MouseDown);
            // 
            // cmbFloor
            // 
            this.cmbFloor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFloor.FormattingEnabled = true;
            this.cmbFloor.Location = new System.Drawing.Point(89, 68);
            this.cmbFloor.Name = "cmbFloor";
            this.cmbFloor.Size = new System.Drawing.Size(145, 20);
            this.cmbFloor.TabIndex = 18;
            this.cmbFloor.DropDown += new System.EventHandler(this.CmbFloor_DropDown);
            // 
            // lblMachineNo
            // 
            this.lblMachineNo.AutoSize = true;
            this.lblMachineNo.Location = new System.Drawing.Point(89, 37);
            this.lblMachineNo.Name = "lblMachineNo";
            this.lblMachineNo.Size = new System.Drawing.Size(0, 12);
            this.lblMachineNo.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "楼层号";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "设备编号";
            // 
            // cmbProjectCode
            // 
            this.cmbProjectCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjectCode.FormattingEnabled = true;
            this.cmbProjectCode.Location = new System.Drawing.Point(89, 137);
            this.cmbProjectCode.Name = "cmbProjectCode";
            this.cmbProjectCode.Size = new System.Drawing.Size(145, 20);
            this.cmbProjectCode.TabIndex = 14;
            this.cmbProjectCode.DropDown += new System.EventHandler(this.CmbProjectCode_DropDown);
            // 
            // lblSNPercent
            // 
            this.lblSNPercent.AutoSize = true;
            this.lblSNPercent.Location = new System.Drawing.Point(89, 316);
            this.lblSNPercent.Name = "lblSNPercent";
            this.lblSNPercent.Size = new System.Drawing.Size(0, 12);
            this.lblSNPercent.TabIndex = 13;
            // 
            // lblSNTotal
            // 
            this.lblSNTotal.AutoSize = true;
            this.lblSNTotal.Location = new System.Drawing.Point(89, 281);
            this.lblSNTotal.Name = "lblSNTotal";
            this.lblSNTotal.Size = new System.Drawing.Size(0, 12);
            this.lblSNTotal.TabIndex = 12;
            // 
            // lblSNOK
            // 
            this.lblSNOK.AutoSize = true;
            this.lblSNOK.Location = new System.Drawing.Point(89, 246);
            this.lblSNOK.Name = "lblSNOK";
            this.lblSNOK.Size = new System.Drawing.Size(0, 12);
            this.lblSNOK.TabIndex = 11;
            // 
            // lblCodereader
            // 
            this.lblCodereader.AutoSize = true;
            this.lblCodereader.Location = new System.Drawing.Point(89, 211);
            this.lblCodereader.Name = "lblCodereader";
            this.lblCodereader.Size = new System.Drawing.Size(0, 12);
            this.lblCodereader.TabIndex = 10;
            // 
            // lblMES
            // 
            this.lblMES.AutoSize = true;
            this.lblMES.Location = new System.Drawing.Point(89, 176);
            this.lblMES.Name = "lblMES";
            this.lblMES.Size = new System.Drawing.Size(0, 12);
            this.lblMES.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 316);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "扫码成功率";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "扫码总数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "扫码OK";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "扫码功能";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "MES功能";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "产品型号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "线体名称";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStatisticClear);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 110);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "控制区域";
            // 
            // btnStatisticClear
            // 
            this.btnStatisticClear.Location = new System.Drawing.Point(32, 71);
            this.btnStatisticClear.Name = "btnStatisticClear";
            this.btnStatisticClear.Size = new System.Drawing.Size(75, 23);
            this.btnStatisticClear.TabIndex = 20;
            this.btnStatisticClear.Text = "统计清零";
            this.btnStatisticClear.UseVisualStyleBackColor = true;
            this.btnStatisticClear.Click += new System.EventHandler(this.BtnStatisticClear_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(134, 28);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(32, 28);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblUserid
            // 
            this.lblUserid.AutoSize = true;
            this.lblUserid.Location = new System.Drawing.Point(89, 345);
            this.lblUserid.Name = "lblUserid";
            this.lblUserid.Size = new System.Drawing.Size(0, 12);
            this.lblUserid.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(26, 345);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "员工工号";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 581);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.tlpMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunningData)).EndInit();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton btnDevice;
        private System.Windows.Forms.ToolStripButton btnRecipe;
        private System.Windows.Forms.ToolStripButton btnHistory;
        private System.Windows.Forms.ToolStripButton btnIOMonitor;
        private System.Windows.Forms.ToolStripButton btnUser;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtWholeStation;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolStripButton btnTray;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSNPercent;
        private System.Windows.Forms.Label lblSNTotal;
        private System.Windows.Forms.Label lblSNOK;
        private System.Windows.Forms.Label lblCodereader;
        private System.Windows.Forms.Label lblMES;
        private System.Windows.Forms.ComboBox cmbProjectCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMachineNo;
        private System.Windows.Forms.ComboBox cmbLine;
        private System.Windows.Forms.ComboBox cmbFloor;
        private System.Windows.Forms.Button btnStatisticClear;
        public System.Windows.Forms.DataGridView dgvRunningData;
        private System.Windows.Forms.Label lblUserid;
        private System.Windows.Forms.Label label11;
    }
}