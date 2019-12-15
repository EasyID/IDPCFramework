using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Software.Toolkit;
using System.IO;
namespace Software.Main
{
    public partial class FrmDatabase : Form
    {
        #region field

        DataTable requireData = new DataTable();

        int pageSize = 30;
        int currentPage = 0;
        int maxPage = 0;

        #endregion

        #region constructor

        public FrmDatabase()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #endregion

        #region loading

        private void Form_History_Load(object sender, EventArgs e)
        {
            prgAction.Value = 0;

            chkDate.Checked = true;
            dtpStart.Value = DateTime.Now.AddDays(-6);
            dtpEnd.Value = DateTime.Now;

        }

        #endregion

        #region close



        #endregion

        private void BtnRequire_Click(object sender, EventArgs e)
        {
            requireData.Clear();

            //分割查询
            try
            {
                List<DateTime> splits = DateTimeSplit.SplitByMonth(dtpStart.Value, dtpEnd.Value);
                for (int i = 0; i < splits.Count - 1; i += 2)
                {
                    RequireData(splits[i], splits[i + 1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            //设置分页信息
            maxPage = (int)Math.Ceiling(requireData.Rows.Count / (double)pageSize);

            //刷新视图
            BtnFirst_Click(this, EventArgs.Empty);
        }

        private void RequireData(DateTime startTime, DateTime endTime)
        {
            string filePath = PathDefine.DataBaseDirectoryPath + $"{startTime.ToString("yyyyMM")}.db3";
            if (File.Exists(filePath))
            {
                using (SQLiteConnection conn = new SQLiteConnection($"data source={filePath}"))
                {
                    conn.Open();
                    SQLiteCommand comm = conn.CreateCommand();
                    comm.CommandText = CreatSelectCommand(NameDefine.SQLTableName, startTime, endTime);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(comm);
                    adapter.Fill(requireData);
                }
            }
        }

        private string CreatSelectCommand(string tableName, DateTime startTime, DateTime endTime)
        {
            List<string> cmdTexts = new List<string>();
            string cmdStr = "";

            if (chkDate.Checked)
                cmdTexts.Add($"{NameDefine.OperateDate} between '{startTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}' and '{endTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}'");

            if (chkSN.Checked)
                cmdTexts.Add($"{NameDefine.SN} like '%{txtSN.Text}%'");

            if (cmdTexts.Count == 0)
            {
                cmdStr = $"select * from [{tableName}]";
            }
            else
            {
                for (int i = 0; i < cmdTexts.Count; i++)
                {
                    if (i == 0)
                        cmdStr = $"select * from [{tableName}] where {cmdTexts[0]}";
                    if (i > 0)
                        cmdStr += $" and {cmdTexts[i]}";
                }
            }

            return cmdStr;
        }

        private void BtnFirst_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            RefreshView();
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage -= 1;
                RefreshView();
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < maxPage)
            {
                currentPage += 1;
                RefreshView();
            }
        }

        private void BtnLast_Click(object sender, EventArgs e)
        {
            currentPage = maxPage;
            RefreshView();
        }

        private void RefreshView()
        {
            dgvData.DataSource = requireData.AsEnumerable().Skip((currentPage - 1) * pageSize).Take(pageSize).CopyToDataTable();
            lblPageInfo.Text = $"{currentPage} / {maxPage}";
        }

        private void BtnExportData_Click(object sender, EventArgs e)
        {
            btnExportData.Enabled = false;
            new Action(ExportToCSV).BeginInvoke(null, null);
        }

        private void ExportToCSV()
        {
            try
            {
                if (requireData.Rows.Count == 0) throw new Exception("没有需要导出的数据！");

                string header = "";
                string path = PathDefine.ExportCSVFilePath;
                for (int i = 0; i < dgvData.ColumnCount; i++)
                {
                    header += dgvData.Columns[i].HeaderText + ",";
                }

                Invoke(new Action(() =>
                {
                    prgAction.Maximum = requireData.Rows.Count - 1;
                }));

                for (int i = 0; i < requireData.Rows.Count; i++)
                {
                    CSVTool.WriteCSV(path, header, requireData.Rows[i]);
                    Invoke(new Action(() =>
                    {
                        prgAction.Value = i;
                    }));
                }

                MessageBox.Show("导出成功！", "提示");

                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("导出文件异常：{0}", ex.Message), "提示");
            }
            finally
            {
                Invoke(new Action(() =>
                {
                    btnExportData.Enabled = true;
                }));
            }
        }

    }
}
