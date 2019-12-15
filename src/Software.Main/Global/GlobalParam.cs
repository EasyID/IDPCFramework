using Software.Toolkit;
using System;
using System.IO;

namespace Software.Main
{
    public class GlobalParam
    {
        #region 单例

        public static GlobalParam Instance { get; set; } = new GlobalParam();

        private GlobalParam() { }

        #endregion

        #region 参数

        public string CodeReaderConnectionParam { get; set; } = "COM5,115200,None,8,One,1500,1500,3";

        public string CardReaderConnectionParam { get; set; } = "COM7,115200,None,8,One";

        public string RobotConnectionParam { get; set; } = "192.168.0.100,1000,3000,3000,1";

        public string PLCConnectionParam { get; set; } = "0,";

        #endregion

        #region 用户相关

        public string OperatorPassword { get; set; } = MD5.Encryption("123");

        public string AdminPassword { get; set; } = MD5.Encryption("333");

        #endregion

        #region 屏蔽

        /// <summary>
        /// 启用MES校验
        /// </summary>
        public bool EnableMES { get; set; }

        /// <summary>
        /// 禁用MES校验时返回结果设定
        /// </summary>
        public bool MESShieldResult { get; set; }

        /// <summary>
        /// 启用条码前位匹配
        /// </summary>
        public bool EnableSNHeadControl { get; set; }

        /// <summary>
        /// 启用条码长度匹配
        /// </summary>
        public bool EnableSNLengthControl { get; set; }

        /// <summary>
        /// 启用固定长度规则
        /// </summary>
        public bool EnableSNConstLengthControl { get; set; }

        #endregion

        #region 配方

        public string CurrentProductType { get; set; } = "Default";

        #endregion

        /// <summary>
        /// 指示程序是否异常退出
        /// </summary>
        public bool IsErrorExit { get; set; }

        /// <summary>
        /// 条码前位匹配规则
        /// </summary>
        public string SNHeadMatchRule { get; set; }

        /// <summary>
        /// 条码长度匹配规则
        /// </summary>
        public int SNLengthMatchRule { get; set; } = 5;

        /// <summary>
        /// 条码固定长度规则
        /// </summary>
        public int SNConstLengthRule { get; set; } = 30;

        /// <summary>
        /// 设备编号
        /// </summary>
        public string MachineNo { get; set; } = "4F-10L-SLJ";
    }

}
