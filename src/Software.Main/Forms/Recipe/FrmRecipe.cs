using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Software.Toolkit;

namespace Software.Main
{
    public partial class FrmRecipe : Form
    {
        public FrmRecipe()
        {
            InitializeComponent();
        }

        private void FrmRecipe_Load(object sender, EventArgs e)
        {
            RefreshFileList();
            RefreshInfo();
        }

        private void RefreshFileList()
        {
            lstProductType.Items.Clear();
            DirectoryInfo[] directories = new DirectoryInfo(PathDefine.RecipeDirectoryPath).GetDirectories();
            foreach (DirectoryInfo directory in directories)
            {
                lstProductType.Items.Add(directory.Name);
            }
        }

        private void RefreshInfo()
        {
            txtCurrentType.Text = GlobalParam.Instance.CurrentProductType;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string newType = txtTargetType.Text.Trim();

            if (string.IsNullOrEmpty(newType))
            {
                MessageBox.Show("目标型号不能为空！", "提示");
                return;
            }

            if (lstProductType.Items.Contains(newType))
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

            string oldType = GlobalParam.Instance.CurrentProductType;

            GlobalParam.Instance.CurrentProductType = newType;
            if (!Directory.Exists(PathDefine.CurrentRecipeDirectoryPath))
            {
                Directory.CreateDirectory(PathDefine.CurrentRecipeDirectoryPath);
            }

            XMLSerializeTool.Save(PathDefine.CurrentRecipeFilePath, Recipe.Instance);

            GlobalParam.Instance.CurrentProductType = oldType;
            RefreshFileList();
            RefreshInfo();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string deleteType = txtTargetType.Text.Trim();

            if (string.IsNullOrEmpty(deleteType))
            {
                MessageBox.Show("目标型号不能为空！", "提示");
                return;
            }

            if (deleteType == txtCurrentType.Text)
            {
                MessageBox.Show("目标型号正在使用，不能删除！", "提示");
                return;
            }

            if (!lstProductType.Items.Contains(deleteType))
            {
                MessageBox.Show("列表中未找到目标，无法删除！", "提示");
                return;
            }

            if (MessageBox.Show(string.Format("是否删除型号：{0}", deleteType), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DirectoryInfo[] directories = new DirectoryInfo(PathDefine.RecipeDirectoryPath).GetDirectories();
                foreach (DirectoryInfo directory in directories)
                {
                    if (directory.Name == deleteType)
                    {
                        directory.Delete(true);
                        break;
                    }
                }
                RefreshFileList();
                RefreshInfo();
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            string switchType = txtTargetType.Text.Trim();

            if (string.IsNullOrEmpty(switchType))
            {
                MessageBox.Show("目标型号值为空，请确认是否选中对应型号！", "提示");
                return;
            }

            if (switchType == txtCurrentType.Text)
            {
                MessageBox.Show("目标型号与正在使用的型号一致，无需切换！", "提示");
                return;
            }

            //if (MessageBox.Show($"是否保存型号 {GetCurrentType()} 的数据？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    btnSave_Click(this, EventArgs.Empty);
            //}

            if (!lstProductType.Items.Contains(switchType))
            {
                MessageBox.Show("目标型号不在列表中，请确认后重试！", "提示");
                return;
            }

            GlobalParam.Instance.CurrentProductType = switchType;
            Recipe.Instance = XMLSerializeTool.Load<Recipe>(PathDefine.CurrentRecipeFilePath);
            RefreshInfo();
        }

        private void lstProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTargetType.Text = lstProductType.SelectedItem.ToString();
        }

    }
}
