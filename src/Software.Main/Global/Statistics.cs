namespace Software.Main
{
    public class Statistics
    {
        #region 单例

        public static Statistics Instance = new Statistics();

        private Statistics() { }

        #endregion

        /// <summary>
        /// 读码成功
        /// </summary>
        public int SNOK = 0;

        /// <summary>
        /// 读码失败
        /// </summary>
        public int SNNG = 0;

        /// <summary>
        /// 读码总数
        /// </summary>
        public double SNTotal
        {
            get { return SNOK + SNNG; }
        }

        /// <summary>
        /// 读码成功率
        /// </summary>
        public double SNOKPercent
        {
            get
            {
                if (SNTotal == 0)
                {
                    return 1;
                }
                else
                {
                    return SNOK / SNTotal;
                }
            }
        }

        /// <summary>
        /// 良品数量
        /// </summary>
        public int ProductOkTotal = 0;

        /// <summary>
        /// 不良品数量
        /// </summary>
        public int ProductNgTotal = 0;

        /// <summary>
        /// 总产品数量
        /// </summary>
        public int ProductTotal
        {
            get
            {
                return ProductOkTotal + ProductNgTotal;
            }
        }

    }
}
