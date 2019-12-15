using System;

namespace Software.Common
{
    /// <summary>
    /// 用于标记能被设备配置窗体识别的部件
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DeviceNodeAttribute : Attribute
    {
        /// <summary>
        /// 部件视图
        /// </summary>
        public Type ViewType { private set; get; }

        /// <param name="viewtype">部件视图</param>
        public DeviceNodeAttribute(Type viewtype = null)
        {
            ViewType = viewtype;
        }
    }
}
