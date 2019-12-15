namespace Software.Main
{
    public static class NameDefine
    {
        #region 部件名称

        public static string StationOne { get; } = "上料设备";

        public static string WholeCodeReader { get; } = "读码器";

        public static string PLC { get; } = "PLC";

        public static string Robot { get; } = "机器人";

        public static string AGV { get; } = "AGV小车";

        #endregion

        #region 窗体名称

        public static string ConfigFormName { get; } = "设备配置";

        public static string RecipeFormName { get; } = "配方选择";

        public static string SettingFormName { get; } = "系统设置";

        public static string DatabaseFormName { get; } = "历史数据";

        public static string UserLoginFormName { get; } = "用户登录";

        public static string IOMonitorFormName { get; } = "信号监控";

        public static string TrayFormName { get; } = "托盘设置"; 

        #endregion

        #region 数据表列名

        public static string SQLTableName { get; } = "Data";

        public static string SN { get; } = "条码";

        public static string Result { get; } = "判断结果";

        public static string OperateDate { get; } = "操作时间";

        public static string Remark { get; } = "备注";

        #endregion

        #region 产品相关

        public static string ProductOK { get; } = "Pass";

        public static string ProductNG { get; } = "Fail";

        #endregion
    }
}
