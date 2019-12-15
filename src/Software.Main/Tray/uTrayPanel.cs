using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace Software.Main
{
    public partial class uTrayPanel : UserControl
    {
        int row = 3, column = 3;
        Color backColor = SystemColors.InactiveBorder;
        SynchronizationContext context = null;
        Tray t = null;

        public uTrayPanel()
        {
            InitializeComponent();
            context = SynchronizationContext.Current;
        }

        /// <summary>
        /// 移除屏蔽
        /// </summary>
        public bool BRemoveFlag { get; set; } = false;

        /// <summary>
        /// 添加屏蔽
        /// </summary>
        public bool BFlag { get; set; } = false;

        /// <summary>
        /// 显示模式
        /// </summary>
        public bool BShowModel { get; set; } = true;

        public void SetTrayObj(Tray tray, Color color, int size = 48)
        {
            t = tray;
            row = t.RowCount;
            column = t.ColumnCount;
            SetLayout(color, size);
        }

        public void SetLayout(Color color, int size)
        {
            t.SortTray(t.StartPosition, t.Direction, t.SwitchMethod);
            DynamicLayout(tableLayoutPanel1, color, size);
        }

        /// <summary>  
        /// 动态布局  
        /// </summary>  
        /// <param name="layoutPanel">布局面板</param>  
        /// <param name="Row">行</param>  
        /// <param name="Col">列</param>  
        private void DynamicLayout(TableLayoutPanel layoutPanel, Color bColor, int size)
        {
            layoutPanel.Controls.Clear();
            layoutPanel.RowStyles.Clear();
            layoutPanel.ColumnStyles.Clear();

            //设置分成几行 
            layoutPanel.RowCount = row + 1;
            float pRow = Convert.ToSingle((100 / row).ToString("0.00"));
            float pColumn = Convert.ToSingle((100 / column).ToString("0.00"));
            for (int i = 0; i < row; i++)
            {
                layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, size));
            }

            //设置分成几列  
            layoutPanel.ColumnCount = column + 1;
            for (int i = 0; i < column; i++)
            {
                layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, size));
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    uButton btn = new uButton();
                    btn.Anchor = AnchorStyles.None;
                    btn.Margin = new Padding(0, 0, 0, 0);
                    btn.Dock = DockStyle.Fill;
                    btn.BackColor = Color.Black;
                    btn.Font = new Font("宋体", 7.5F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
                    btn.Name = i.ToString() + "_" + j.ToString();
                    Index pos = new Index(i, j);
                    int num = t.FindPos(pos);
                    if (num > -1)
                    {
                        t.dic_Index[num].SetColor(bColor);//初始化颜色
                    }

                    if (BShowModel) //预览模式
                    {
                        if (num < 0) //屏蔽的点不显示名称
                        {
                            btn.Content = "";
                            SetBtnColor(btn, backColor);
                        }
                        else //不屏蔽的点显示顺序号
                        {
                            btn.Content = num.ToString();
                            SetBtnColor(btn, bColor);
                        }
                    }
                    else
                    {
                        if (num < 0)
                        {
                            SetBtnColor(btn, backColor);
                        }
                        else
                        {
                            SetBtnColor(btn, bColor);
                        }
                        btn.Content = i.ToString() + "," + j.ToString();
                    }

                    btn.Click += new EventHandler(btn_Click);
                    layoutPanel.Controls.Add(btn, j, i);
                }

            }
        }

        private void SetBtnColor(uButton btn, Color color)
        {
            btn.LanternBackground = color;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var btn = (uButton)sender;
            if (BFlag && !BShowModel)
            {
                SetBtnColor(btn, backColor);
                t.AddEmptyPos(btn.Name);
            }
            if (BRemoveFlag && !BShowModel)
            {
                SetBtnColor(btn, Color.Blue);
                t.RemoveEmptyPos(btn.Name);
            }
        }

        public void UpdateColor()
        {
            context.Post(UpdateUI, null);
        }

        private void UpdateUI(object obj)
        {
            foreach (KeyValuePair<int, Index> pair in t.dic_Index)
            {
                int col = pair.Value.Col;
                int row = pair.Value.Row;
                uButton btn = (uButton)tableLayoutPanel1.GetControlFromPosition(col, row);
                SetBtnColor(btn, pair.Value.color);
            }
        }
    }
}
