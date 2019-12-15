using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using log4net;
using Software.Common;

namespace Software.Device
{
    public class NetworkDevice : Terminal
    {

        #region field

        private static ILog logger = LogManager.GetLogger(typeof(NetworkDevice));

        private Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

        private Thread DataListenThread;
        private ManualResetEventSlim threadStatus = new ManualResetEventSlim();

        private bool IsClosing;
        byte[] receiveBuffer = new byte[256];

        #endregion

        #region constructor

        public NetworkDevice(TerminalMode mode = TerminalMode.Sync)
        {
            Mode = mode;
        }

        #endregion

        public override void Start()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //分配参数
            string[] param = Param.Split(',');
            socket.ReceiveTimeout = int.Parse(param[2]);
            socket.SendTimeout = int.Parse(param[3]);
            TryTimes = int.Parse(Param.Split(',')[4]);

            //启动操作
            socket.Connect(IPAddress.Parse(param[0]), int.Parse(param[1]));
            IsStart = true;

            //根据模式设置行为
            switch (Mode)
            {
                case TerminalMode.Async:
                    DataListenThread = new Thread(ReceiveBehavior);
                    DataListenThread.IsBackground = true;
                    DataListenThread.Start();
                    break;
                default:
                    break;
            }

        }

        public override void End()
        {
            IsClosing = true;

            if (DataListenThread != null && !threadStatus.Wait(5000))
            {
                DataListenThread.Abort();
            }
            threadStatus.Reset();

            socket?.Disconnect(false);

            IsStart = false;
            IsClosing = false;
        }

        [ExecuteInfo("TRG", "开始读码", "TRG,message")]
        public string Trigger(object param = null)
        {
            string receiveString = "";

            switch (Mode)
            {
                case TerminalMode.Async:
                    {
                        try
                        {
                            socket.Send(Encoding.Default.GetBytes("Run,1\n"));
                        }
                        catch (Exception ex)
                        {
                            logger.Debug(string.Format("Trigger Error：{0}", ex.Message));
                            receiveString = ResultFlags.Error + ex.Message;
                        }
                    }
                    break;
                default:
                    {
                        for (int i = 0; i < TryTimes; i++)
                        {
                            try
                            {
                                Array.Clear(receiveBuffer, 0, receiveBuffer.Length);

                                socket.Send(Encoding.Default.GetBytes("Run,1\n"));
                                socket.Receive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None);
                                receiveString = Encoding.Default.GetString(receiveBuffer);

                                if (string.IsNullOrEmpty(receiveString))
                                    throw new Exception("图像解析失败");

                                break;
                            }
                            catch (Exception ex)
                            {
                                logger.Debug(string.Format("Trigger Error：{0}", ex.Message));
                                receiveString = ResultFlags.Error + ex.Message;
                            }
                        }
                    }
                    break;
            }

            return receiveString;
        }

        private void ReceiveBehavior()
        {
            string result = "";
            while (true)
            {
                try
                {
                    if (IsClosing)
                    {
                        break;
                    }

                    Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
                    socket.Receive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None);
                    result = Encoding.Default.GetString(receiveBuffer);
                    RaiseDataReceivedEvent(result);
                }
                catch (Exception ex)
                {
                    if (ex.GetType() != typeof(TimeoutException))
                    {
                        logger.Debug(ex);
                        RaiseDataReceivedEvent(ResultFlags.Error + ex.Message);
                        break;
                    }
                }
            }
            threadStatus.Set();
        }

        public override string Execute(string content)
        {
            string result = "";
            content = content.Trim().ToUpper();

            if (Regex.IsMatch(content, "^HELP"))
            {
                Attribute[] attributes = GetType().GetMethods()
                    .Select(s => Attribute.GetCustomAttribute(s, typeof(ExecuteInfoAttribute)))
                    .Where(s => s != null).ToArray();

                result = Environment.NewLine + "-----------------------------------" + Environment.NewLine;
                result += Environment.NewLine + "该设备支持以下指令：" + Environment.NewLine;
                foreach (Attribute attribute in attributes)
                {
                    ExecuteInfoAttribute executeInfo = (ExecuteInfoAttribute)attribute;
                    result += Environment.NewLine + executeInfo.Command + " - " + executeInfo.Description + Environment.NewLine
                        + "示例：" + executeInfo.Example + Environment.NewLine;
                }
                result += Environment.NewLine + "-----------------------------------" + Environment.NewLine;
            }
            else if (Regex.IsMatch(content, "^TRG"))
            {
                if (content.Length > 3)
                {
                    string cmd = content.Substring(3);
                    result = Trigger(cmd);
                }
                else
                {
                    result = "文本长度不正确，请重新输入！";
                }
            }
            else
            {
                result = "不支持指令" + content;
            }
            return result;
        }
    }
}
