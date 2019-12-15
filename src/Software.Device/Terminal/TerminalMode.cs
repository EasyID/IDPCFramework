namespace Software.Device
{
    public enum TerminalMode
    {
        /// <summary>
        /// 调用 Trigger() 方法获取结果，会阻塞线程直到接收到数据
        /// </summary>
        Sync,

        /// <summary>
        /// 调用 Trigger() 方法发送命令，结果由DataReceived事件通知订阅者，不阻塞线程
        /// </summary>
        Async
    }
}
