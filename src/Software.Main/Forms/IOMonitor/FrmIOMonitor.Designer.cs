namespace Software.Main
{
    partial class FrmIOMonitor
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxChainInput0 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbxChainInput2 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbxChainInput1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxChainOutput0 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbxChainOutput2 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbxChainOutput1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.tabInput.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabInput);
            this.tabControl1.Controls.Add(this.tabOutput);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ItemSize = new System.Drawing.Size(150, 40);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(866, 562);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 2;
            // 
            // tabInput
            // 
            this.tabInput.Controls.Add(this.tableLayoutPanel1);
            this.tabInput.Location = new System.Drawing.Point(4, 44);
            this.tabInput.Name = "tabInput";
            this.tabInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabInput.Size = new System.Drawing.Size(858, 514);
            this.tabInput.TabIndex = 2;
            this.tabInput.Text = "输入信号";
            this.tabInput.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.gbxChainInput0, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbxChainInput2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbxChainInput1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("宋体", 9F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 507F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 507F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 507F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(852, 508);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // gbxChainInput0
            // 
            this.gbxChainInput0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxChainInput0.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxChainInput0.Location = new System.Drawing.Point(4, 4);
            this.gbxChainInput0.Name = "gbxChainInput0";
            this.gbxChainInput0.Size = new System.Drawing.Size(276, 500);
            this.gbxChainInput0.TabIndex = 83;
            // 
            // gbxChainInput2
            // 
            this.gbxChainInput2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxChainInput2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxChainInput2.Location = new System.Drawing.Point(570, 4);
            this.gbxChainInput2.Name = "gbxChainInput2";
            this.gbxChainInput2.Size = new System.Drawing.Size(278, 500);
            this.gbxChainInput2.TabIndex = 85;
            // 
            // gbxChainInput1
            // 
            this.gbxChainInput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxChainInput1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxChainInput1.Location = new System.Drawing.Point(287, 4);
            this.gbxChainInput1.Name = "gbxChainInput1";
            this.gbxChainInput1.Size = new System.Drawing.Size(276, 500);
            this.gbxChainInput1.TabIndex = 84;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.tableLayoutPanel3);
            this.tabOutput.Location = new System.Drawing.Point(4, 44);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(858, 514);
            this.tabOutput.TabIndex = 3;
            this.tabOutput.Text = "输出信号";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.gbxChainOutput0, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.gbxChainOutput2, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.gbxChainOutput1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Font = new System.Drawing.Font("宋体", 9F);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 365F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 365F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 365F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(852, 508);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // gbxChainOutput0
            // 
            this.gbxChainOutput0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxChainOutput0.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxChainOutput0.Location = new System.Drawing.Point(4, 4);
            this.gbxChainOutput0.Name = "gbxChainOutput0";
            this.gbxChainOutput0.Size = new System.Drawing.Size(276, 500);
            this.gbxChainOutput0.TabIndex = 83;
            // 
            // gbxChainOutput2
            // 
            this.gbxChainOutput2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxChainOutput2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxChainOutput2.Location = new System.Drawing.Point(570, 4);
            this.gbxChainOutput2.Name = "gbxChainOutput2";
            this.gbxChainOutput2.Size = new System.Drawing.Size(278, 500);
            this.gbxChainOutput2.TabIndex = 85;
            // 
            // gbxChainOutput1
            // 
            this.gbxChainOutput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxChainOutput1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxChainOutput1.Location = new System.Drawing.Point(287, 4);
            this.gbxChainOutput1.Name = "gbxChainOutput1";
            this.gbxChainOutput1.Size = new System.Drawing.Size(276, 500);
            this.gbxChainOutput1.TabIndex = 84;
            // 
            // FrmIOMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 562);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmIOMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IO监控表";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmIOMonitor_FormClosed);
            this.Load += new System.EventHandler(this.FrmIOmonitor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabInput.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInput;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel gbxChainInput0;
        private System.Windows.Forms.FlowLayoutPanel gbxChainInput2;
        private System.Windows.Forms.FlowLayoutPanel gbxChainInput1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel gbxChainOutput0;
        private System.Windows.Forms.FlowLayoutPanel gbxChainOutput2;
        private System.Windows.Forms.FlowLayoutPanel gbxChainOutput1;
    }
}