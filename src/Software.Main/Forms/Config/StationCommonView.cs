using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Software.Contract;

namespace Software.Main
{
    public partial class StationCommonView : UserControl
    {

        private PropertyInfo[] stationProperties;
        private object _station;
        private DeviceNodeAttribute _viewAttribute;

        #region 构造函数

        private StationCommonView()
        {
            InitializeComponent();
        }

        public StationCommonView(object station,DeviceNodeAttribute supportAttribute)
        {
            _station = station;
            _viewAttribute = supportAttribute;
            InitializeComponent();
        }

        #endregion

        private void StationPartCommonView_Load(object sender, EventArgs e)
        {
            stationProperties = _station.GetType().GetProperties()
                .Where(s => s.GetCustomAttribute(typeof(DeviceNodeAttribute)) != null).ToArray();

            //部件列表加载
            lstStationPart.BeginUpdate();
            lstStationPart.Items.Add("整体");
            foreach (PropertyInfo property in stationProperties)
            {
                IEntity part = property.GetValue(_station) as IEntity;
                lstStationPart.Items.Add(part.EntityName);
            }
            lstStationPart.EndUpdate();

            //显示区加载
            lstStationPart.SelectedIndex = 0;
        }

        private void LstStationPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEntity _stationPart = null;
            DeviceNodeAttribute _DeviceNodeAttribute = null;
            UserControl _userControl = null;

            if (lstStationPart.SelectedIndex != 0)
            {
                //获取当前选择的工位部件实例及对应自定义特性
                string targetEntityName = lstStationPart.SelectedItem.ToString();
                foreach (PropertyInfo property in stationProperties)
                {
                    _stationPart = property.GetValue(_station) as IEntity;
                    if (_stationPart == null) continue;
                    if (_stationPart.EntityName == targetEntityName)
                    {
                        _DeviceNodeAttribute = (DeviceNodeAttribute)property.GetCustomAttribute(typeof(DeviceNodeAttribute));
                        break;
                    }
                }

                //获取需显示的用户控件               
                if (_DeviceNodeAttribute is null)
                    _userControl = UserControlHelper.GetUserControl(null, null);
                else
                    _userControl = UserControlHelper.GetUserControl(_DeviceNodeAttribute.ViewType, new object[] { _stationPart });
            }
            else
            {
                _userControl = UserControlHelper.GetUserControl(_viewAttribute.ViewType, new object[] { _station });
            }

            pnlStationPart.Controls.Clear();
            _userControl.Dock = DockStyle.Fill;
            pnlStationPart.Controls.Add(_userControl);

        }

    }
}
