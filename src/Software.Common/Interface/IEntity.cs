namespace Software.Common
{
    public interface IEntity
    {
        /// <summary>
        ///     名称
        /// </summary>
        string EntityName { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        string Description { get; set; }
    }
}
