namespace Software.Common
{
    public interface IStation : IEntity
    {
        /// <summary>
        ///     是否初始化完成
        /// </summary>
        bool IsInitialCompleted { get; set; }

        /// <summary>
        ///     工位初始化
        /// </summary>
        void Initial();

        /// <summary>
        ///     工位刷新
        /// </summary>
        void Update();

        /// <summary>
        ///     工位保存
        /// </summary>
        void Save();
    }
}
