using System;
using System.Reflection;
using System.Windows.Forms;
namespace Software.Main
{
    public class UserControlHelper
    {
        public static UserControl GetUserControl(Type controltype, object[] controlargs)
        {
            UserControl _control;

            if (controltype == null)
            {
                _control = typeof(DefaultView).Assembly.CreateInstance(typeof(DefaultView).FullName, false, BindingFlags.CreateInstance, null, controlargs, null, null) as UserControl;
            }
            else
            {
                object control = controltype.Assembly.CreateInstance(controltype.FullName, false, BindingFlags.CreateInstance, null, controlargs, null, null);          
                if(control is UserControl)
                {
                    _control = (UserControl)control;
                }
                else
                {
                    _control = typeof(DefaultView).Assembly.CreateInstance(typeof(DefaultView).FullName, false, BindingFlags.CreateInstance, null, controlargs, null, null) as UserControl;
                }
            }
            return _control;
        }
    }
}
