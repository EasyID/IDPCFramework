using System;

namespace Software.Device
{
    public class MitsubishiBuffer
    {
        #region 字段

        public int[] data;

        private readonly object writeLocker = new object();

        #endregion

        #region 属性

        public int Length { get { return data.Length; } }

        public int DeviceAddress { get; private set; }

        public string DeviceName { get; private set; }

        public string DeviceType { get; private set; }

        #endregion

        #region 索引器

        public int this[int index]
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
        ///     初始化 MitsubishiBuffer 类
        /// </summary>
        /// <param name="devicename">指定设备名称（如M4000、D3000）</param>
        /// <param name="devicesize">指定该缓冲区的大小</param>
        public MitsubishiBuffer(string devicename, int devicesize)
        {
            DeviceName = devicename.ToUpper();

            DeviceType = devicename.Substring(0, 1);

            DeviceAddress = int.Parse(devicename.Substring(1));

            if (0 != DeviceAddress % 16)
                throw new ArgumentException(string.Format("Address {0} invalid.", DeviceAddress));

            data = new int[devicesize];
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

        /// <summary>
        /// 获取该缓冲区某一位的bool值
        /// </summary>
        /// <param name="address">从起始地址开始的偏移值</param>
        public bool GetBit(int address)
        {
            int index = address % 16;
            int cache = data[address / 16];
            int mask = 1 << index;
            return (cache & mask) > 0;
        }

        /// <summary>
        /// 设置该缓冲区某一位的bool值
        /// </summary>
        /// <param name="address">从起始地址开始的偏移值</param>
        /// <param name="value">需要设定的bool值</param>
        public void SetBit(int address, bool value)
        {
            lock (writeLocker)
            {
                int index = address % 16;
                int cache = data[address / 16];
                int mask = 0;
                if (value)
                {
                    mask = 1 << index;
                    cache = cache | mask;
                }
                else
                {
                    mask = 0xffff ^ 1 << index;
                    cache = cache & mask;
                }
                data[address / 16] = cache;
            }
        }

        #endregion

    }
}
