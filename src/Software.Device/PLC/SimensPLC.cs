using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
namespace Software.Device
{
    public class SimensPLC
    {
        // -----------------------------------------------------------------
        // This project using libnodave.net.dll & libnodave.dll
        //
        // Last Updatetime 2019/4/30 By Sitruuna
        // -----------------------------------------------------------------

        #region 变量

        libnodave.daveOSserialType fds;
        libnodave.daveInterface di;
        libnodave.daveConnection dc;
        int _rack, _slot, _port;
        string _address;

        #endregion

        #region 属性

        public string EntityName { get; set; }

        public string Description { get; set; }

        public bool IsConnected { get; set; }

        public string Param
        {
            get
            {
                return _address + "," + _port.ToString() + "," + _rack.ToString() + "," + _slot.ToString();
            }
            set
            {
                string[] str = value.Split(',');
                _address = str[0];
                _port = int.Parse(str[1]);
                _rack = int.Parse(str[2]);
                _slot = int.Parse(str[3]);
            }
        }

        #endregion

        #region 构造函数

        public SimensPLC() { }

        #endregion

        public void Connect()
        {
            fds.rfd = libnodave.openSocket(_port, _address);
            fds.wfd = fds.rfd;
            if (fds.rfd > 0)
            {
                di = new libnodave.daveInterface(fds, "IF1", 0, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
                di.setTimeout(20000);
                dc = new libnodave.daveConnection(di, 0, _rack, _slot);
                if (0 == dc.connectPLC()) IsConnected = true;
            }
            else
            {
                IsConnected = false;
                throw new Exception("PLC返回值不正确");
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                di.disconnectAdapter();
                dc.disconnectPLC();
                libnodave.closeSocket(_port);
                Thread.Sleep(500);
            }

        }

        public void ReadDeviceBlock(ref SimensBuffer buffer)
        {
            if (0 != dc.readBytes(buffer.DeviceArea, buffer.DeviceNumber, buffer.DeviceAddress, buffer.Length, buffer.data))
            {
                throw new Exception(string.Format("返回值不为0"));
            }
        }

        public void WriteDeviceBlock(ref SimensBuffer buffer)
        {
            if (0 != dc.writeBytes(buffer.DeviceArea, buffer.DeviceNumber, buffer.DeviceAddress, buffer.Length, buffer.data))
            {
                throw new Exception(string.Format("返回值不为0"));
            }
        }

    }
}
