namespace Software.Main
{
    partial class StationCommonView
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
            this.tlpStationPart = new System.Windows.Forms.TableLayoutPanel();
            this.lstStationPart = new System.Windows.Forms.ListBox();
            this.pnlStationPart = new System.Windows.Forms.Panel();
            this.tlpStationPart.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpStationPart
            // 
            this.tlpStationPart.ColumnCount = 2;
            this.tlpStationPart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tlpStationPart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tlpStationPart.Controls.Add(this.lstStationPart, 0, 0);
            this.tlpStationPart.Controls.Add(this.pnlStationPart, 1, 0);
            this.tlpStationPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStationPart.Location = new System.Drawing.Point(0, 0);
            this.tlpStationPart.Margin = new System.Windows.Forms.Padding(0);
            this.tlpStationPart.Name = "tlpStationPart";
            this.tlpStationPart.RowCount = 1;
            this.tlpStationPart.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStationPart.Size = new System.Drawing.Size(612, 415);
            this.tlpStationPart.TabIndex = 1;
            // 
            // lstStationPart
            // 
            this.lstStationPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstStationPart.FormattingEnabled = true;
            this.lstStationPart.ItemHeight = 12;
            this.lstStationPart.Location = new System.Drawing.Point(0, 0);
            this.lstStationPart.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lstStationPart.Name = "lstStationPart";
            this.lstStationPart.Size = new System.Drawing.Size(70, 415);
            this.lstStationPart.TabIndex = 2;
            this.lstStationPart.SelectedIndexChanged += new System.EventHandler(this.LstStationPart_SelectedIndexChanged);
            // 
            // pnlStationPart
            // 
            this.pnlStationPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStationPart.Location = new System.Drawing.Point(76, 0);
            this.pnlStationPart.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.pnlStationPart.Name = "pnlStationPart";
            this.pnlStationPart.Size = new System.Drawing.Size(536, 415);
            this.pnlStationPart.TabIndex = 1;
            // 
            // StationCommonView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpStationPart);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "StationCommonView";
            this.Size = new System.Drawing.Size(612, 415);
            this.Load += new System.EventHandler(this.StationPartCommonView_Load);
            this.tlpStationPart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpStationPart;
        private System.Windows.Forms.Panel pnlStationPart;
        private System.Windows.Forms.ListBox lstStationPart;
    }
}
