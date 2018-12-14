using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hit_Brick_Server
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private UdpClient server = null;
        private IPEndPoint localEndPoint = null;
        private ArrayList remoteIPPool = new ArrayList();

        //event
        public delegate void LogHandler(string log);
        public event LogHandler LogEvent;

        private void mainForm_Load(object sender, EventArgs e)
        {
            //禁止缩放
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            //事件绑定
            LogEvent += addLog;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (txt_port.Text.Length > 0)
            {
                int port = int.Parse(txt_port.Text);
                if (port <= 65535)
                {
                    if (server == null)
                    {                        
                        localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                        server = new UdpClient(localEndPoint);                        
                        UdpState recvState = new UdpState(server);
                        server.BeginReceive(new AsyncCallback(recvCallBack),recvState);
                        Log("服务器已启动，正在接收消息……");
                        //ui
                        this.btn_stop.Enabled = true;
                        this.btn_start.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("服务运行中。", "错误");
                    }
                } else
                {
                    MessageBox.Show("请输入合法端口号。","错误");
                }
            } else
            {
                MessageBox.Show("请输入端口号。","错误");
            }
        }

        private void recvCallBack(IAsyncResult res)
        {
            UdpState recvState = res.AsyncState as UdpState;
            if (res.IsCompleted)
            {
                string message = null;
                try
                {
                    byte[] recvBytes = recvState.client.EndReceive(res, ref recvState.remoteEndPoint);
                    message = Encoding.UTF8.GetString(recvBytes);
                }
                catch
                {
                    //do nothing
                }
                try
                {
                    if (message != null) {
                        JObject jObject = (JObject)JsonConvert.DeserializeObject(message);
                        string msgType = jObject["type"].ToString();
                        switch (msgType)
                        {
                            case "connect":
                                string playerName = jObject["content"].ToString();
                                Log("新玩家加入大厅：" + playerName + ":" + recvState.remoteEndPoint.ToString());
                                MpTextMessage response = new MpTextMessage("connect", "success");
                                byte[] sendBuffer = response.GetBytes();
                                //发送回复
                                try
                                {
                                    server.Connect(recvState.remoteEndPoint);
                                }
                                catch
                                {
                                    Log("无法建立到客户端 " + playerName + ":" + recvState.remoteEndPoint.ToString() +" 的连接。");
                                }                           
                                server.BeginSend(sendBuffer, sendBuffer.Length,sendCallBack, new UdpSendState(server, response.ToString()));
                                break;
                            default:
                                Log("消息类别不明: " + message);
                                server.Close();
                                server = new UdpClient(localEndPoint);
                                recvState = new UdpState(server);
                                server.BeginReceive(new AsyncCallback(recvCallBack), recvState);                                
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    if (message != null)
                    {
                        Log("无法解析收到的消息: " + e.Message + " @ " + message);
                    } else
                    {
                        Log("无法解析收到的消息: " + e.Message);
                    }
                }
            }
        }

        private void sendCallBack(IAsyncResult res)
        {
            UdpSendState sendState = res.AsyncState as UdpSendState;
            UdpClient senderClient = sendState.client;
            if (res.IsCompleted)
            {
                int sent = senderClient.EndSend(res);
                if (sent <= 0)
                {
                    Log("消息发送失败: "+sendState.message);
                } else
                {
                    Log("消息已经发送至客户端: " + senderClient.Client.RemoteEndPoint.ToString());
                }
            }
            senderClient.Close();
            server = new UdpClient(localEndPoint);
            UdpState recvState = new UdpState(server);
            server.BeginReceive(new AsyncCallback(recvCallBack), recvState);
        }

        private void txt_port_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Log(string content)
        {
            LogEvent?.Invoke(content);
        }

        private void addLog(string content)
        {
            var update = new Action(() => { rtb_log.Select(rtb_log.TextLength, 0); rtb_log.ScrollToCaret(); rtb_log.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + content + "\r\n"); });
            this.BeginInvoke(update);
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            try
            {
                server.Close();
                server = null;
                Log("服务已停止……");
                //ui
                btn_stop.Enabled = false;
                btn_start.Enabled = true;
            } catch (Exception ex)
            {
                Log("服务无法停止，出现错误：" + ex.Message);
            }            
        }

        private void btn_clearLog_Click(object sender, EventArgs e)
        {
            rtb_log.Text = "";
        }
    }
}
