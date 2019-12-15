using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActUtlTypeLib;
using System.Threading;

namespace Software.Device
{
    public class MitsubishiPLC
    {
        #region 变量

        private ActUtlTypeClass device = new ActUtlTypeClass();
        private int _actLogicalStationNumber;
        private string _actPassword;

        #endregion

        #region 属性

        public string EntityName { get; set; }

        public string Description { get; set; }

        public bool IsConnect { get; set; }

        public string Param
        {
            get
            {
                return _actLogicalStationNumber.ToString() + "," + _actPassword;
            }
            set
            {
                string[] str = value.Split(',');
                _actLogicalStationNumber = int.Parse(str[0]);
                _actPassword = str[1];
            }
        }

        #endregion

        #region 构造函数

        public MitsubishiPLC() { }

        #endregion

        public void Connect()
        {
            Disconnect();
            device.ActLogicalStationNumber = _actLogicalStationNumber;
            device.ActPassword = _actPassword;
            if (device.Open() == 0)
            {
                IsConnect = true;
            }
            else
            {
                IsConnect = false;
                throw new Exception("PLC返回值不正确\n");
            }
        }

        public void Disconnect()
        {
            if (IsConnect)
            {
                device.Close();
                IsConnect = false;
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 读取数据块
        /// </summary>
        public void ReadDeviceBlock(ref MitsubishiBuffer buffer)
        {
            if (0 != device.ReadDeviceBlock(buffer.DeviceName, buffer.Length, out buffer.data[0]))
            {
                throw new Exception(string.Format("返回值不为0"));
            }
        }

        /// <summary>
        /// 写入数据块
        /// </summary>
        public void WriteDeviceBlock(ref MitsubishiBuffer buffer)
        {
            if (0 != device.WriteDeviceBlock(buffer.DeviceName, buffer.Length, ref buffer.data[0]))
            {
                throw new Exception(string.Format("返回值不为0"));
            }
        }

    }
}
