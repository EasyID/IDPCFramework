using System;
using System.Text.RegularExpressions;

namespace Software.Device
{
    public class SimensBuffer
    {
        #region 字段

        public byte[] data;

        private readonly object writeLocker = new object();

        #endregion

        #region 属性

        public int Length { get { return data.Length; } }

        public int DeviceArea { get; private set; }

        public int DeviceNumber { get; private set; }

        public int DeviceAddress { get; private set; }

        public string DeviceName { get; private set; }

        #endregion

        #region 索引器

        public byte this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化 SimensBuffer 类
        /// </summary>
        /// <param name="devicename">指定设备名称（如MB4000、DBD3000）</param>
        /// <param name="devicesize">指定该缓冲区的大小</param>
        public SimensBuffer(string devicename, int devicesize)
        {
            GetDeviceInfo(devicename);
            data = new byte[devicesize];
        }

        private void GetDeviceInfo(string devicename)
        {
            DeviceName = devicename.ToUpper();
            string[] deviceNames = DeviceName.Split('.');
            if (deviceNames.Length == 1)
            {
                if (Regex.IsMatch(DeviceName, "^[MF]"))
                {
                    DeviceArea = libnodave.daveFlags;
                    DeviceNumber = 0;
                    DeviceAddress = int.Parse(devicename.Substring(2));
                }
                else if (Regex.IsMatch(DeviceName, "^[EI]"))
                {
                    DeviceArea = libnodave.daveInputs;
                    DeviceNumber = 0;
                    DeviceAddress = int.Parse(devicename.Substring(2));
                }
                else if (Regex.IsMatch(DeviceName, "^[AQ]"))
                {
                    DeviceArea = libnodave.daveOutputs;
                    DeviceNumber = 0;
                    DeviceAddress = int.Parse(devicename.Substring(2));
                }
                else if (Regex.IsMatch(DeviceName, "^T"))
                {
                    DeviceArea = libnodave.daveTimer;
                    DeviceNumber = 0;
                    DeviceAddress = int.Parse(devicename.Substring(1));
                }
                else if (Regex.IsMatch(DeviceName, "^[ZC]"))
                {
                    DeviceArea = libnodave.daveCounter;
                    DeviceNumber = 0;
                    DeviceAddress = int.Parse(devicename.Substring(1));
                }
                else if (Regex.IsMatch(DeviceName, "^P[EI]"))
                {
                    DeviceArea = libnodave.daveP;
                    DeviceNumber = 0;
                    DeviceAddress = int.Parse(devicename.Substring(3));
                }
                else
                {
                    throw new ArgumentException(string.Format("devicename {0} is not supported.", devicename));
                }

            }
            else if (deviceNames.Length == 2 && Regex.IsMatch(DeviceName, "^DB"))
            {
                DeviceArea = libnodave.daveDB;
                DeviceNumber = int.Parse(deviceNames[0].Substring(2));
                DeviceAddress = int.Parse(deviceNames[1].Substring(3));
            }
            else
            {
                throw new ArgumentException(string.Format("devicename {0} is not supported.", devicename));
            }

        }

        #endregion

        #region 数据处理

        /// <summary>
        /// 重置数组
        /// </summary>
        /// <param name="index">起始索引</param>
        /// <param name="length">重置长度</param>
        public void Clear(int index, int length)
        {
            Array.Clear(data, index, length);
        }

        #region 写入

        /// <summary>
        /// 设置bool数据
        /// </summary>
        /// <param name="address">起始位置</param>
        /// <param name="value">数据</param>
        public void SetBit(int address, bool value)
        {
            lock (writeLocker)
            {
                int index = address % 8;
                int cache = data[address / 8];
                int mask = 0;
                if (value)
                {
                    mask = 1 << index;
                    cache = cache | mask;
                }
                else
                {
                    mask = 0xff ^ 1 << index;
                    cache = cache & mask;
                }
                data[address / 8] = (byte)cache;
            }
        }

        /// <summary>
        /// 设置short数据
        /// </summary>
        /// <param name="index">起始索引</param>
        public void SetInt16(int index, short value)
        {
            byte[] tempBytes = BitConverter.GetBytes(value);
            Array.Reverse(tempBytes);
            Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
        }

        /// <summary>
        /// 设置ushort数据
        /// </summary>
        /// <param name="index">索引位置</param>
        public void SetUInt16(int index, ushort value)
        {
            byte[] tempBytes = BitConverter.GetBytes(value);
            Array.Reverse(tempBytes);
            Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
        }

        /// <summary>
        /// 设置int数据
        /// </summary>
        /// <param name="index">索引位置</param>
        public void SetInt32(int index, int value)
        {
            byte[] tempBytes = BitConverter.GetBytes(value);
            Array.Reverse(tempBytes);
            Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
        }

        /// <summary>
        /// 设置uint数据
        /// </summary>
        /// <param name="index">索引位置</param>
        public void SetUInt32(int index, uint value)
        {
            byte[] tempBytes = BitConverter.GetBytes(value);
            Array.Reverse(tempBytes);
            Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
        }

        /// <summary>
        /// 设置float数据
        /// </summary>
        /// <param name="index">索引位置</param>
        public void SetFloat(int index, float value)
        {
            byte[] tempBytes = BitConverter.GetBytes(value);
            Array.Reverse(tempBytes);
            Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
        }

        /// <summary>
        /// 设置double数据
        /// </summary>
        /// <param name="index">索引位置</param>
        public void SetDouble(int index, double value)
        {
            byte[] tempBytes = BitConverter.GetBytes(value);
            Array.Reverse(tempBytes);
            Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
        }

        #endregion

        #region 获取

        /// <summary>
        /// 获取bool结果
        /// </summary>
        /// <param name="index">起始位置</param>
        public bool GetBit(int address)
        {
            int index = address % 8;
            int cache = data[address / 8];
            int mask = 1 << index;
            return (cache & mask) > 0;
        }

        /// <summary>
        /// 获取short结果
        /// </summary>
        /// <param name="index">起始索引</param>
        public short GetInt16(int index)
        {
            byte[] tmp = new byte[2];
            tmp[0] = data[1 + index];
            tmp[1] = data[0 + index];
            return BitConverter.ToInt16(tmp, 0);
        }

        /// <summary>
        /// 获取ushort结果
        /// </summary>
        /// <param name="index">索引位置</param>
        public ushort GetUInt16(int index)
        {
            byte[] tmp = new byte[2];
            tmp[0] = data[1 + index];
            tmp[1] = data[0 + index];
            return BitConverter.ToUInt16(tmp, 0);
        }

        /// <summary>
        /// 获取int结果
        /// </summary>
        /// <param name="index">索引位置</param>
        public int GetInt32(int index)
        {
            byte[] tmp = new byte[4];
            tmp[0] = data[3 + index];
            tmp[1] = data[2 + index];
            tmp[2] = data[1 + index];
            tmp[3] = data[0 + index];
            return BitConverter.ToInt32(tmp, 0);
        }

        /// <summary>
        /// 获取uint结果
        /// </summary>
        /// <param name="index">索引位置</param>
        public uint GetUInt32(int index)
        {
            byte[] tmp = new byte[4];
            tmp[0] = data[3 + index];
            tmp[1] = data[2 + index];
            tmp[2] = data[1 + index];
            tmp[3] = data[0 + index];
            return BitConverter.ToUInt32(tmp, 0);
        }

        /// <summary>
        /// 获取float结果
        /// </summary>
        /// <param name="index">索引位置</param>
        public float GetFloat(int index)
        {
            byte[] tmp = new byte[4];
            tmp[0] = data[3 + index];
            tmp[1] = data[2 + index];
            tmp[2] = data[1 + index];
            tmp[3] = data[0 + index];
            return BitConverter.ToSingle(tmp, 0);
        }

        /// <summary>
        /// 获取double结果
        /// </summary>
        /// <param name="index">索引位置</param>
        public double GetDouble(int index)
        {
            byte[] tmp = new byte[8];
            tmp[0] = data[7 + index];
            tmp[1] = data[6 + index];
            tmp[2] = data[5 + index];
            tmp[3] = data[4 + index];
            tmp[4] = data[3 + index];
            tmp[5] = data[2 + index];
            tmp[6] = data[1 + index];
            tmp[7] = data[0 + index];
            return BitConverter.ToDouble(tmp, 0);
        }

        #endregion

        #endregion
    }
}
