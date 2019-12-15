using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Software.Common;

namespace Software.Main
{

    public partial class FrmConfig : Form
    {
        #region field

        Dictionary<string, ViewCreatArgs> viewDic = new Dictionary<string, ViewCreatArgs>();
        object machine;

        #endregion

        #region constructor

        private FrmConfig()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public FrmConfig(object machine) : this()
        {
            this.machine = machine;
        }

        #endregion

        private void frmConfig_Load(object sender, EventArgs e)
        {
            tvwStationList.BeginUpdate();

            tvwStationList.Nodes.Clear();
            viewDic.Clear();

            AddNodeIFNeed(tvwStationList.Nodes, machine);

            tvwStationList.EndUpdate();

            tvwStationList.ExpandAll();
        }

        private void tvwStationList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UserControl _userControl = null;
            ViewCreatArgs info = null;

            pnlContent.Controls.Clear();

            if (viewDic.ContainsKey(tvwStationList.SelectedNode.FullPath))
            {
                info = viewDic[tvwStationList.SelectedNode.FullPath];
            }

            DeviceNodeAttribute _viewSupportAttribute = (DeviceNodeAttribute)info.property.GetCustomAttribute(typeof(DeviceNodeAttribute));
            if (info != null && _viewSupportAttribute != null && _viewSupportAttribute.ViewType != null)
            {
                _userControl = GetUserControl(_viewSupportAttribute.ViewType, new object[] { info.property.GetValue(info.instance) });
            }
            else
            {
                _userControl = GetUserControl(null, null);
            }

            _userControl.Parent = pnlContent;
            _userControl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(_userControl);
        }

        private void AddNodeIFNeed(TreeNodeCollection nodes, object instance)
        {
            PropertyInfo[] instanceProperties = instance.GetType().GetProperties()
                .Where(s => s.GetCustomAttribute(typeof(DeviceNodeAttribute)) != null).ToArray();

            if (instanceProperties.Length > 0)
            {
                for (int i = 0; i < instanceProperties.Length; i++)
                {
                    PropertyInfo property = instanceProperties[i];
                    object part = property.GetValue(instance);

                    nodes.Add((part as IEntity).EntityName);
                    viewDic.Add(nodes[i].FullPath, new ViewCreatArgs() { property = property, instance = instance });

                    AddNodeIFNeed(nodes[i].Nodes, part);
                }
            }
        }

        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            pnlContent.Controls.Clear();
        }

        private UserControl GetUserControl(Type controltype, object[] controlargs)
        {
            UserControl _control;

            if (controltype == null)
            {
                _control = typeof(DefaultView).Assembly.CreateInstance(typeof(DefaultView).FullName, false, BindingFlags.CreateInstance, null, controlargs, null, null) as UserControl;
            }
            else
            {
                object control = controltype.Assembly.CreateInstance(controltype.FullName, false, BindingFlags.CreateInstance, null, controlargs, null, null);
                if (control is UserControl)
                {
                    _control = (UserControl)control;
                }
                else
                {
                    _control = typeof(DefaultView).Assembly.CreateInstance(typeof(DefaultView).FullName, false, BindingFlags.CreateInstance, null, controlargs, null, null) as UserControl;
                }
            }
            return _control;
        }
    }
}
