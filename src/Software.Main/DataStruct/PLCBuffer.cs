using System;

namespace Software.Main
{
    public class PLCBuffer
    {
        // -----------------------------------------------------------------
        //
        // Last update time : 2019/07/19 
        // By Sitruuna
        // 
        // -----------------------------------------------------------------

        #region 字段

        public byte[] data;

        private readonly object writeLocker = new object();

        #endregion

        #region 属性

        public int Length { get { return data.Length; } }

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
        /// 初始化 PLCBuffer 类
        /// </summary>
        /// <param name="deviceAddress">指定设备地址</param>
        /// <param name="devicesize">指定该缓冲区的大小</param>
        public PLCBuffer(string deviceAddress, int devicesize)
        {
            DeviceName = deviceAddress;
            data = new byte[devicesize];
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
                    cache |= mask;
                }
                else
                {
                    mask = 0xff ^ 1 << index;
                    cache &= mask;
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
            lock (writeLocker)
            {
                byte[] tempBytes = BitConverter.GetBytes(value);
                Array.Reverse(tempBytes);
                Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
            }
        }

        /// <summary>
        /// 设置ushort数据
        /// </summary>
        /// <param name="index">索引位置</param>
        public void SetUInt16(int index, ushort value)
        {
            lock (writeLocker)
            {
                byte[] tempBytes = BitConverter.GetBytes(value);
                Array.Reverse(tempBytes);
                Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
            }
        }

        /// <summary>
        /// 设置int数据
        /// </summary>
        /// <param name="index">索引位置</param>
        public void SetInt32(int index, int value)
        {
            lock (writeLocker)
            {
                byte[] tempBytes = BitConverter.GetBytes(value);
                Array.Reverse(tempBytes);
                Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
            }
        }

        /// <summary>
        /// 设置uint数据
        /// </summary>
        /// <param name="index">索引位置</param>
        public void SetUInt32(int index, uint value)
        {
            lock (writeLocker)
            {
                byte[] tempBytes = BitConverter.GetBytes(value);
                Array.Reverse(tempBytes);
                Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
            }
        }

        /// <summary>
        /// 设置float数据
        /// </summary>
        /// <param name="index">索引位置</param>
        public void SetFloat(int index, float value)
        {
            lock (writeLocker)
            {
                byte[] tempBytes = BitConverter.GetBytes(value);
                Array.Reverse(tempBytes);
                Array.Copy(tempBytes, 0, data, index, tempBytes.Length);
            }
        }

        /// <summary>
        /// 设置double数据
        /// </summary>
        /// <param name="index">索引位置</param>
        public void SetDouble(int index, double value)
        {
            lock (writeLocker)
            {
                byte[] tempBytes = BitConverter.GetBytes(value);
                Array.Reverse(tempBytes);
                Array.Copy(tempBytes, 0, data, index, tempBytes.Length);

            }
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
