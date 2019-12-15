using System;
using System.Windows.Forms;
using log4net;
using CQInterFace;
namespace Software.Main
{
    static class Program
    {
        static System.Threading.Mutex mutex;
        static ILog logger = LogManager.GetLogger(typeof(Program));
        static FrmMain frmMain;
        static FrmLogin frmLogin;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isRunning;
            mutex = new System.Threading.Mutex(true, "RunOneInstanceOnly", out isRunning);

            if (isRunning)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                //处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(UI_ThreadException);

                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                try
                {
                    string result = CQUpdate.UpdateDLL("BZ");
                    logger.Debug($" UpdateDLL 返回数据：{result}");

                    if (result != "Success")
                    {
                        throw new Exception(result);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error($"检查MES版本失败：{ex.Message}");
                    MessageBox.Show($"检查MES版本失败：{ex.Message}\n请尝试联系IT部门解决该问题！", "提示");
                    return;
                }

                try
                {
                    frmMain = new FrmMain();
                    frmMain.LoadParam();

                    frmLogin = new FrmLogin();
                    if (frmLogin.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(frmMain);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error($"参数加载失败：{ex.Message}");
                    MessageBox.Show($"参数加载失败：{ex.Message}", "提示");
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("程序已经启动！", "提示");
                Application.Exit();
            }
        }

        static void UI_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            GlobalParam.Instance.IsErrorExit = true;

            try
            {
                frmMain.SaveParam();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show($"参数保存失败：{ex.Message}", "提示");
            }

            logger.Fatal(e.Exception.Message);
            MessageBox.Show("UI_ThreadException Catched.\nMessage:" + e.Exception.ToString(), "提示");
            Application.Exit();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            GlobalParam.Instance.IsErrorExit = true;

            try
            {
                frmMain.SaveParam();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show($"参数保存失败：{ex.Message}", "提示");
            }

            logger.Fatal(e.ExceptionObject.ToString());
            MessageBox.Show("CurrentDomain_UnhandledException Catched.\nMessage:" + e.ExceptionObject.ToString(), "提示");
            Application.Exit();
        }
    }
}
