using System;

namespace Software.Common
{
    /// <summary>
    /// 用于标记支持 Excute() 的方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ExecuteInfoAttribute : Attribute
    {
        /// <summary>
        /// 指令文本
        /// </summary>
        public string Command { private set; get; }

        /// <summary>
        /// 指令描述
        /// </summary>
        public string Description { private set; get; }

        /// <summary>
        /// 指令举例
        /// </summary>
        public string Example { private set; get; }

        /// <param name="command">指令文本</param>
        /// <param name="description">指令描述</param>
        /// <param name="example">指令举例</param>
        public ExecuteInfoAttribute(string command, string description, string example)
        {
            Command = command;
            Description = description;
            Example = example;
        }
    }
}
