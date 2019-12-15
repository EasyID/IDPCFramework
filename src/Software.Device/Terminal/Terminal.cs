using Software.Common;

namespace Software.Device
{

    public delegate void DataReceivedEventHandler(string message);

    public abstract class Terminal : IEntity
    {
        /// <summary>
        /// 当接收到数据时通知所有订阅者
        /// </summary>
        public event DataReceivedEventHandler DataReceived;

        /// <summary>
        /// 名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 连接参数
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// 是否启动
        /// </summary>
        public virtual bool IsStart { get; set; }

        /// <summary>
        /// 操作失败重试次数
        /// </summary>
        public int TryTimes { get; protected set; }

        /// <summary>
        /// 运行模式
        /// </summary>
        public TerminalMode Mode { get; protected set; }

        /// <summary>
        /// 触发DataReceived事件
        /// </summary>
        /// <param name="content">接收到的信息</param>
        protected virtual void RaiseDataReceivedEvent(string content)
        {
            DataReceived?.Invoke(content);
        }

        /// <summary>
        /// 启动
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// 终止
        /// </summary>
        public abstract void End();

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="cmd">命令字符串</param>
        public abstract string Execute(string cmd);

    }

}