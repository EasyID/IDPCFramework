using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Software.Toolkit;
namespace Software.Main
{
    public partial class TestTray : Form
    {
        #region field

        private static ILog logger = LogManager.GetLogger(typeof(TestTray));

        Tray tray = new Tray();
        uTrayPanel tp;

        #endregion

        #region constructor

        public TestTray()
        {
            InitializeComponent();
        }

        #endregion

        public void TestTray_Load(object sender, EventArgs e)
        {
            //初始化各控件默认值
            cmbStartPosition.SelectedIndex = 0;
            cmbDirection.SelectedIndex = 0;
            cmbSwitchMethod.SelectedIndex = 0;

            //初始化托盘视图
            tp = new uTrayPanel();
            tp.Dock = DockStyle.Fill;
            panel1.Controls.Add(tp);

            //刷新文件
            if (Directory.GetFiles(PathDefine.TrayDirectoryPath).Count() == 0)
            {
                tray.Load(PathDefine.TrayDirectoryPath);
                tray.Save(PathDefine.TrayDirectoryPath);
            }
            else
            {
                tray.Load(PathDefine.TrayDirectoryPath);
            }
            RefreshFileList();
            RefreshViewInfo(tray);
        }

        private void RefreshFileList()
        {
            cmbTargetType.Items.Clear();
            FileInfo[] files = new DirectoryInfo(PathDefine.TrayDirectoryPath).GetFiles();
            foreach (FileInfo file in files)
            {
                cmbTargetType.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            }
        }

        public void RefreshViewInfo(Tray tray)
        {
            txtCurrentType.Text = tray.Name;
            nudRow.Value = tray.RowCount;
            nudCol.Value = tray.ColumnCount;
            cmbStartPosition.Text = tray.StartPosition.ToString();
            cmbDirection.Text = tray.Direction.ToString();
            cmbSwitchMethod.Text = tray.SwitchMethod.ToString();
            tp.SetTrayObj(tray, Color.Gray);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string newType = cmbTargetType.Text.Trim();

            if (string.IsNullOrEmpty(newType))
            {
                MessageBox.Show("目标型号不能为空！", "提示");
                return;
            }

            if (cmbTargetType.Items.Contains(newType))
            {
                MessageBox.Show("列表中已有相同型号，不能再次新增！", "提示");
                return;
            }

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (newType.Contains(c))
                {
                    MessageBox.Show($"名称中不能包含特殊字符 '{c}' 请重新命名！", "提示");
                    return;
                }
            }

            tray.Name = newType;
            tray.Save(PathDefine.TrayDirectoryPath);
            RefreshFileList();
            RefreshViewInfo(tray);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string deleteType = cmbTargetType.Text.Trim();

            if (string.IsNullOrEmpty(deleteType))
            {
                MessageBox.Show("目标型号不能为空！", "提示");
                return;
            }

            if (deleteType == tray.Name)
            {
                MessageBox.Show("目标型号正在编辑，不能删除！", "提示");
                return;
            }

            if (deleteType == "Default")
            {
                MessageBox.Show("默认型号不能删除！", "提示");
                return;
            }

            if (!cmbTargetType.Items.Contains(deleteType))
            {
                MessageBox.Show("列表中未找到目标，无法删除！", "提示");
                return;
            }

            if (MessageBox.Show(string.Format("是否删除型号：{0}", deleteType), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FileInfo[] files = new DirectoryInfo(PathDefine.TrayDirectoryPath).GetFiles();
                foreach (FileInfo file in files)
                {
                    if (Path.GetFileNameWithoutExtension(file.Name) == deleteType)
                    {
                        file.Delete();
                        break;
                    }
                }
                RefreshFileList();
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            string switchType = cmbTargetType.Text.Trim();

            if (string.IsNullOrEmpty(switchType))
            {
                MessageBox.Show("目标型号值为空，请确认是否选中对应型号！", "提示");
                return;
            }

            if (switchType == tray.Name)
            {
                MessageBox.Show("目标型号与当前的型号一致，无需切换！", "提示");
                return;
            }

            tray.Name = switchType;
            tray.Load(PathDefine.TrayDirectoryPath);
            RefreshViewInfo(tray);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tray.StartPosition = (EStartPosition)Enum.Parse(typeof(EStartPosition), cmbStartPosition.SelectedItem.ToString());
            tray.Direction = (EDirection)Enum.Parse(typeof(EDirection), cmbDirection.SelectedItem.ToString());
            tray.SwitchMethod = (ESwitchMethod)Enum.Parse(typeof(ESwitchMethod), cmbSwitchMethod.SelectedItem.ToString());
            tray.RowCount = (int)nudRow.Value;
            tray.ColumnCount = (int)nudCol.Value;
            tp.SetTrayObj(tray, Color.Gray);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tray.Save(PathDefine.TrayDirectoryPath);
        }

    }
}
