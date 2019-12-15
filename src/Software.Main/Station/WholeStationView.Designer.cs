namespace Software.Main
{
    partial class WholeStationView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudEnableConstLength = new System.Windows.Forms.NumericUpDown();
            this.chkEnableConstLength = new System.Windows.Forms.CheckBox();
            this.nudLengthMatch = new System.Windows.Forms.NumericUpDown();
            this.chkLengthMatch = new System.Windows.Forms.CheckBox();
            this.chkHeadMatch = new System.Windows.Forms.CheckBox();
            this.txtHeadMatch = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbMESShieldResult = new System.Windows.Forms.ComboBox();
            this.chkEnableMES = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnableConstLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLengthMatch)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudEnableConstLength);
            this.groupBox2.Controls.Add(this.chkEnableConstLength);
            this.groupBox2.Controls.Add(this.nudLengthMatch);
            this.groupBox2.Controls.Add(this.chkLengthMatch);
            this.groupBox2.Controls.Add(this.chkHeadMatch);
            this.groupBox2.Controls.Add(this.txtHeadMatch);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 124);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "条码规则";
            // 
            // nudEnableConstLength
            // 
            this.nudEnableConstLength.Location = new System.Drawing.Point(138, 91);
            this.nudEnableConstLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEnableConstLength.Name = "nudEnableConstLength";
            this.nudEnableConstLength.Size = new System.Drawing.Size(66, 21);
            this.nudEnableConstLength.TabIndex = 6;
            this.nudEnableConstLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudEnableConstLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkEnableConstLength
            // 
            this.chkEnableConstLength.AutoSize = true;
            this.chkEnableConstLength.Location = new System.Drawing.Point(12, 93);
            this.chkEnableConstLength.Name = "chkEnableConstLength";
            this.chkEnableConstLength.Size = new System.Drawing.Size(120, 16);
            this.chkEnableConstLength.TabIndex = 5;
            this.chkEnableConstLength.Text = "生成固定长度条码";
            this.chkEnableConstLength.UseVisualStyleBackColor = true;
            // 
            // nudLengthMatch
            // 
            this.nudLengthMatch.Location = new System.Drawing.Point(138, 58);
            this.nudLengthMatch.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLengthMatch.Name = "nudLengthMatch";
            this.nudLengthMatch.Size = new System.Drawing.Size(66, 21);
            this.nudLengthMatch.TabIndex = 4;
            this.nudLengthMatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLengthMatch.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkLengthMatch
            // 
            this.chkLengthMatch.AutoSize = true;
            this.chkLengthMatch.Location = new System.Drawing.Point(12, 60);
            this.chkLengthMatch.Name = "chkLengthMatch";
            this.chkLengthMatch.Size = new System.Drawing.Size(120, 16);
            this.chkLengthMatch.TabIndex = 3;
            this.chkLengthMatch.Text = "读取条码限制长度";
            this.chkLengthMatch.UseVisualStyleBackColor = true;
            // 
            // chkHeadMatch
            // 
            this.chkHeadMatch.AutoSize = true;
            this.chkHeadMatch.Location = new System.Drawing.Point(12, 27);
            this.chkHeadMatch.Name = "chkHeadMatch";
            this.chkHeadMatch.Size = new System.Drawing.Size(72, 16);
            this.chkHeadMatch.TabIndex = 2;
            this.chkHeadMatch.Text = "前端匹配";
            this.chkHeadMatch.UseVisualStyleBackColor = true;
            // 
            // txtHeadMatch
            // 
            this.txtHeadMatch.Location = new System.Drawing.Point(96, 25);
            this.txtHeadMatch.Name = "txtHeadMatch";
            this.txtHeadMatch.Size = new System.Drawing.Size(212, 21);
            this.txtHeadMatch.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbMESShieldResult);
            this.groupBox1.Controls.Add(this.chkEnableMES);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 108);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "功能选择";
            // 
            // cmbMESShieldResult
            // 
            this.cmbMESShieldResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMESShieldResult.FormattingEnabled = true;
            this.cmbMESShieldResult.Items.AddRange(new object[] {
            "屏蔽后，结果设为OK",
            "屏蔽后，结果设为NG"});
            this.cmbMESShieldResult.Location = new System.Drawing.Point(84, 18);
            this.cmbMESShieldResult.Name = "cmbMESShieldResult";
            this.cmbMESShieldResult.Size = new System.Drawing.Size(169, 20);
            this.cmbMESShieldResult.TabIndex = 8;
            // 
            // chkEnableMES
            // 
            this.chkEnableMES.AutoSize = true;
            this.chkEnableMES.Location = new System.Drawing.Point(12, 20);
            this.chkEnableMES.Name = "chkEnableMES";
            this.chkEnableMES.Size = new System.Drawing.Size(66, 16);
            this.chkEnableMES.TabIndex = 7;
            this.chkEnableMES.Text = "MES校验";
            this.chkEnableMES.UseVisualStyleBackColor = true;
            this.chkEnableMES.CheckedChanged += new System.EventHandler(this.ChkEnableMES_CheckedChanged);
            // 
            // WholeStationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WholeStationView";
            this.Size = new System.Drawing.Size(426, 321);
            this.Load += new System.EventHandler(this.WholeStationView_Load);
            this.Leave += new System.EventHandler(this.WholeStationView_Leave);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnableConstLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLengthMatch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudEnableConstLength;
        private System.Windows.Forms.CheckBox chkEnableConstLength;
        private System.Windows.Forms.NumericUpDown nudLengthMatch;
        private System.Windows.Forms.CheckBox chkLengthMatch;
        private System.Windows.Forms.CheckBox chkHeadMatch;
        private System.Windows.Forms.TextBox txtHeadMatch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkEnableMES;
        private System.Windows.Forms.ComboBox cmbMESShieldResult;
    }
}
