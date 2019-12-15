using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
namespace Software.Main
{
    public class FormManager
    {
        #region Singleton

        public static FormManager Instance { get; set; } = new FormManager();

        private FormManager() { }

        #endregion

        #region field

        private Dictionary<string, object> formCache = new Dictionary<string, object>();

        #endregion

        /// <summary>
        ///     注册窗体
        /// </summary>
        /// <typeparam name="TForm">窗体类型</typeparam>
        /// <param name="key">窗体标识</param>
        /// <param name="param">构造函数所需的参数</param>
        public bool RegisterForm<TForm>(string key, object[] param = null) where TForm : Form
        {
            TForm form = null;

            if (formCache.ContainsKey(key))
            {
                return false;
            }
            else
            {
                Type formtype = typeof(TForm);
                form = formtype.Assembly.CreateInstance(formtype.FullName, false, BindingFlags.CreateInstance, null, param, null, null) as TForm;

                if (null != form)
                {
                    form.Text = key;
                    formCache.Add(key, form);
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }


        /// <summary>
        ///     获取与Key相对应的窗体
        /// </summary>
        /// <typeparam name="TForm">窗体类型</typeparam>
        /// <param name="key">窗体标识</param>
        public TForm GetForm<TForm>(string key) where TForm : Form
        {
            return formCache.ContainsKey(key) ? (TForm)formCache[key] : null;
        }

    }
}
