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

        //events
        public delegate void LogHandler(string log);
        public event LogHandler LogEvent;

        //lists
        public List<Player> playerList;
        public List<Room> roomList;

        //values
        public long currentRoomId = 0;

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
                        //初始化数据
                        roomList = new List<Room>();
                        playerList = new List<Player>();

                        currentRoomId = 0;

                        //启动服务器
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
                        Log("收到消息: " + message);
                        JObject jObject = (JObject)JsonConvert.DeserializeObject(message);                        
                        string msgType = jObject["type"].ToString();
                        switch (msgType)
                        {
                            //连接
                            case "connect":
                                string[] contents = jObject["content"].ToString().Split(',');
                                string playerName = "";
                                string playerGuid = "";
                                try
                                {
                                    playerName = contents[0].Trim();
                                    playerGuid = contents[1].Trim();
                                } catch(Exception ex)
                                {
                                    Log("解析消息出现问题: " + ex.Message);
                                }
                                Log("新玩家加入大厅：" + playerName + "("+ playerGuid+ "):" + recvState.remoteEndPoint.ToString());

                                //加入到玩家列表                                
                                Player player = new Player(playerGuid, playerName, recvState.remoteEndPoint);
                                playerList.Add(player);

                                SendResponse("connect", "success",recvState);
                                break;
                            //断开连接
                            case "disconnect":
                                //传入内容为playerGuid
                                playerGuid = jObject["content"].ToString();
                                foreach (Player p in playerList)
                                {
                                    if (p.GUID == playerGuid)
                                    {
                                        playerList.Remove(p);
                                        break;
                                    }
                                }
                                break;
                            //房间列表
                            case "room_list":
                                List<RoomInfo> avaliableRooms = new List<RoomInfo>();
                                foreach (Room r in roomList)
                                {
                                    if (r.status == 0)
                                    {
                                        //新建roominfo
                                        RoomInfo info = new RoomInfo();
                                        info.id = r.id;
                                        info.name = r.name;
                                        info.playerCount = r.playerCount;
                                        info.capacity = r.capacity;

                                        avaliableRooms.Add(info);
                                    }
                                }
                                SendResponse("room_list", JsonConvert.SerializeObject(avaliableRooms), recvState.remoteEndPoint);
                                break;
                            //创建房间
                            case "create_room":
                                playerGuid = jObject["content"].ToString();

                                //新建房间
                                Room room = new Room("游戏房间", 4);    //默认设置
                                room.level = "l_1";

                                //获取玩家
                                player = playerList.Find(p => p.GUID.Equals(playerGuid));

                                if (player == null)
                                {
                                    //没有找到Guid匹配的玩家
                                    SendResponse("create_room", "failed", recvState);
                                    Log("创建房间失败: "+playerList.ToString());
                                } else
                                {
                                    //使用递增的id
                                    room.id = currentRoomId;
                                    currentRoomId++;

                                    //设置玩家
                                    room.players[0] = player;
                                    room.holder = 0;
                                    room.playerCount++;
                                    player.isInLobby = true;

                                    //加入到列表
                                    roomList.Add(room);

                                    //回传数据，格式为 success,roomId
                                    SendResponse("create_room", "success," + room.id.ToString(), recvState);                                    
                                }                                
                                break;
                            //房间容量调整
                            case "room_changeCapacity":
                                //传入内容为roomId,newCapacity
                                contents = jObject["content"].ToString().Split(',');
                                long roomId = long.Parse(contents[0]);
                                int newCapacity = Convert.ToInt32(contents[1]);

                                //判断容量是否合法
                                if (newCapacity > 4 || newCapacity <= 1)
                                {
                                    SendResponse("room_changeCapacity", "failed", recvState);
                                    break;
                                }

                                room = roomList.Find(r => r.id.Equals(roomId));

                                if (room == null)
                                {
                                    SendResponse("room_changeCapacity", "failed", recvState);
                                    break;
                                }
                                else
                                {
                                    room.capacity = newCapacity;
                                    //给请求的玩家发送回复
                                    SendResponse("room_changeCapacity", "success", recvState);
                                    //给房间内的其他玩家发送更改消息
                                    foreach(Player p in room.players)
                                    {
                                        SendResponseWithoutRecv("room_changeCapacity", newCapacity.ToString(), p.EndPoint);
                                    }                                    
                                }
                                break;                            
                            //房间关卡调整
                            case "room_changeLevel":                                    
                                
                                break;
                            //房间名称调整
                            case "room_changeName":
                                //传入内容为roomId,newTitle
                                contents = jObject["content"].ToString().Split(',');
                                roomId = long.Parse(contents[0]);
                                string newTitle = contents[1];

                                room = roomList.Find(r => r.id.Equals(roomId));

                                if (room == null)
                                {                                    
                                    break;
                                }
                                else
                                {
                                    room.name = newTitle;
                                    //给房间内的其他玩家发送更改消息
                                    for(int i=0;i<room.players.Length;i++)
                                    {
                                        if (room.players[i] != null && i != room.holder) {
                                            SendResponseWithoutRecv("room_changeName", roomId.ToString()+","+newTitle, room.players[i].EndPoint);
                                        }
                                    }
                                }
                                StartRecv();
                                break;
                            //加入房间
                            case "join_room":
                                
                                break;
                            //离开房间
                            case "leave_room":
                                //传入内容为roomId,newLevel
                                contents = jObject["content"].ToString().Split(',');
                                roomId = long.Parse(contents[0]);
                                playerGuid = contents[1];

                                Log("玩家[" + playerGuid + "]离开了房间[" + roomId.ToString() +  "]");

                                //获取玩家
                                player = playerList.Find(p => p.GUID.Equals(playerGuid));
                                room = roomList.Find(r => r.id.Equals(roomId));

                                if (player != null && room != null)
                                {
                                    room.playerCount--;
                                    if (room.playerCount > 0)
                                    {
                                        for (int i = 0; i < room.players.Length; i++)
                                        {
                                            if (room.players[i].GUID.Equals(playerGuid))
                                            {
                                                //判断玩家是否为房主，若是，则寻找新的房主
                                                if (room.holder == i)
                                                {
                                                    //设置新房主
                                                    for (int j=0;j<room.players.Length; j++)
                                                    {
                                                        if (room.players[j] != null && j!=i)
                                                        {
                                                            room.holder = j;
                                                        }
                                                    }
                                                }
                                                room.players[i] = null;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        roomList.Remove(room);
                                    }
                                }
                                StartRecv();
                                break;
                            //准备
                            case "game_ready":
                                break;
                            //开始
                            case "game_start":
                                break;
                            //读取完成
                            case "load_completed":
                                break;
                            default:
                                Log("消息类别不明: " + message);
                                StartRecv();
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

        private void StartRecv()
        {
            server.Close();
            server = new UdpClient(localEndPoint);
            UdpState recvState = new UdpState(server);
            server.BeginReceive(new AsyncCallback(recvCallBack), recvState);
        }

        private void SendResponse(string type, string content, UdpState recvState)
        {
            MpTextMessage response = new MpTextMessage(type, content);
            byte[] sendBuffer = response.GetBytes();
            //发送回复
            try
            {
                server.Connect(recvState.remoteEndPoint);
            }
            catch
            {
                Log("无法建立到客户端: " + recvState.remoteEndPoint.ToString() + " 的连接。");
            }
            server.BeginSend(sendBuffer, sendBuffer.Length, sendCallBack, new UdpSendState(server, response.ToString()));
        }

        private void SendResponse(string type, string content, IPEndPoint endPoint)
        {
            MpTextMessage response = new MpTextMessage(type, content);
            byte[] sendBuffer = response.GetBytes();
            //发送回复
            try
            {
                server.Connect(endPoint);
            }
            catch
            {
                Log("无法建立到客户端: " + endPoint.ToString() + " 的连接。");
            }
            server.BeginSend(sendBuffer, sendBuffer.Length, sendCallBack, new UdpSendState(server, response.ToString()));
        }

        private void SendResponseWithoutRecv(string type, string content, UdpState recvState)
        {
            MpTextMessage response = new MpTextMessage(type, content);
            byte[] sendBuffer = response.GetBytes();
            //发送回复
            try
            {
                server.Connect(recvState.remoteEndPoint);
            }
            catch
            {
                Log("无法建立到客户端: " + recvState.remoteEndPoint.ToString() + " 的连接。");
            }
            server.BeginSend(sendBuffer, sendBuffer.Length, sendCallBackWithoutRecv, new UdpSendState(server, response.ToString()));
        }

        private void SendResponseWithoutRecv(string type, string content, IPEndPoint endPoint)
        {
            MpTextMessage response = new MpTextMessage(type, content);
            byte[] sendBuffer = response.GetBytes();
            //发送回复
            try
            {
                server.Connect(endPoint);
            }
            catch
            {
                Log("无法建立到客户端: " + endPoint.ToString() + " 的连接。");
            }
            server.BeginSend(sendBuffer, sendBuffer.Length, sendCallBackWithoutRecv, new UdpSendState(server, response.ToString()));
        }

        //不启动接收的回调函数
        private void sendCallBackWithoutRecv(IAsyncResult res)
        {
            UdpSendState sendState = res.AsyncState as UdpSendState;
            UdpClient senderClient = sendState.client;
            if (res.IsCompleted)
            {
                int sent = senderClient.EndSend(res);
                if (sent <= 0)
                {
                    Log("消息发送失败: " + sendState.message);
                }
                else
                {
                    Log("消息已经发送至客户端: " + senderClient.Client.RemoteEndPoint.ToString());
                }
            }
            senderClient.Close();
        }

        private void sendCallBack(IAsyncResult res)
        {
            sendCallBackWithoutRecv(res);

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
