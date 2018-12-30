using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class MultiplayController
{

    public UdpClient client = null;

    public IPEndPoint remoteEndPoint = null;
    public IPAddress remoteIP = null;
    public int remotePort = 0;

    private IPEndPoint remoteSenderEP = null;
    private IPEndPoint localEP = null;
    //ui
    private GameObject txt_connect;

    //timer
    private Timer connectTimer;

    //flags
    private bool isConnectFailed = false;
    public bool isCreateRoomFailed = false;

    //room
    private long currentRoomId = -1;

    public MultiplayController(string ipAddress, int port)
    {
        remoteIP = IPAddress.Parse(ipAddress);
        remoteEndPoint = new IPEndPoint(remoteIP, port);
        //ui init
        txt_connect = gameController.panel_multiplay_inscene.transform.Find("txt_connect").gameObject;
    }

    public void Shutdown()
    {
        isConnectFailed = true;
        if (connectTimer != null)
        {
            connectTimer.Stop();
        }
        client.Close();
    }

    private void GeneralRecvCallback(IAsyncResult iar)
    {
        if (iar.IsCompleted)
        {
            string message = null;
            try
            {
                byte[] recvBytes = client.EndReceive(iar, ref remoteSenderEP);
                message = Encoding.UTF8.GetString(recvBytes);
            }
            catch
            {
                //do nothing
            }

            if (message != null)
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(message);

                string msgType = jObject["type"].ToString();
                string content = jObject["content"].ToString();

                switch (msgType)
                {
                    case "room_list":                      
                        if (!content.Contains("error"))
                        {
                            GetRoomListSuccess(content);
                        }
                        else
                        {
                            GetRoomListFailed();
                            return;
                        }
                        break;
                    case "create_room":
                        if (content.Contains("success"))
                        {
                            CreateRoomSuccess(content);
                        }
                        else
                        {
                            CreateRoomFailed();
                            return;
                        }
                        break;
                }
            }
            client.BeginReceive(new AsyncCallback(GeneralRecvCallback), null);
        }
    }

    #region == 连接 ==

    public void TryConnect()
    {
        client = new UdpClient();
        if (remoteEndPoint != null)
        {
            //连接的同时提交玩家的名称和guid
            MpTextMessage message = new MpTextMessage("connect", gameController.player_name + "," + gameController.player_guid);
            if (gameController.panel_multiplay_inscene != null)
            {
                txt_connect = gameController.panel_multiplay_inscene.transform.Find("txt_connect").gameObject;
                if (txt_connect != null)
                {
                    txt_connect.GetComponent<Text>().text = "连接中......";
                }
            }

            try
            {
                client.Connect(remoteEndPoint);
                localEP = (IPEndPoint)client.Client.LocalEndPoint;
            }
            catch
            {
                ConnectFailed();
                return;
            }
            byte[] sendBuffer = message.GetBytes();
            client.BeginSend(sendBuffer, sendBuffer.Length, new AsyncCallback(ConnectSendCallback), null);
            //add a timer
            connectTimer = gameController.thisgameObj.AddComponent<Timer>();
            connectTimer.liveTime = 5;
            connectTimer.Timeout += ConnectFailed;
            connectTimer.Start();
        }
    }

    private void ConnectSendCallback(IAsyncResult iar)
    {
        if (iar.IsCompleted)
        {
            int sent = client.EndSend(iar);
            if (sent == 0)
            {
                ConnectFailed();
                return;
            }
            else
            {
                //接受服务器回复
                client.BeginReceive(new AsyncCallback(ConnectRecvCallback), null);
            }
        }
    }

    private void ConnectRecvCallback(IAsyncResult iar)
    {
        if (iar.IsCompleted)
        {
            byte[] receiveBytes = new byte[0];
            try
            {
                receiveBytes = client.EndReceive(iar, ref remoteSenderEP);
            }
            catch
            {
                ConnectFailed();
                return;
            }
            if (receiveBytes.Length > 0)
            {
                string s = Encoding.UTF8.GetString(receiveBytes);
                MpTextMessage message = JsonUtility.FromJson<MpTextMessage>(s);
                if (message.content == "success")
                {
                    ConnectSuccess();
                }
                else
                {
                    ConnectFailed();
                    return;
                }
            }
            else
            {
                ConnectFailed();
                return;
            }
        }
    }

    private void ConnectSuccess()
    {
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() =>
        {
            if (txt_connect != null)
            {
                txt_connect.GetComponent<Text>().text = "";
            }
            connectTimer.Stop();

            //自动获取房间列表
            client.Close();
            client = new UdpClient(localEP);
            GetRoomList();
        });
    }

    private void ConnectFailed()
    {
        if (!isConnectFailed)
        {
            isConnectFailed = true;
            DoOnMainThread.ExecuteOnMainThread.Enqueue(() =>
            {
                if (txt_connect != null)
                {
                    txt_connect.GetComponent<Text>().text = "";
                }

                GameObject failedDialog = UnityEngine.MonoBehaviour.Instantiate(gameController.dialog_multiplay, gameController.canvas.transform);
                anim_ctrl_multiplay_fail failedDialog_ctrl = failedDialog.GetComponent<anim_ctrl_multiplay_fail>();
                failedDialog_ctrl.failure = "connect";

                if (connectTimer != null)
                {
                    connectTimer.Stop();
                }
                this.Shutdown();
            });
        }
    }
    #endregion

    #region == 获取房间列表 ==
    public void GetRoomList()
    {
        client.Close();
        client = new UdpClient(localEP);
        if (remoteEndPoint != null)
        {
            MpTextMessage message = new MpTextMessage("room_list");

            try
            {
                client.Connect(remoteEndPoint);
            }
            catch
            {
                GetRoomListFailed();
                return;
            }
            byte[] sendBuffer = message.GetBytes();
            client.BeginSend(sendBuffer, sendBuffer.Length, new AsyncCallback(GetRoomListSendCallabck), null);
            //add a timer
            connectTimer = gameController.thisgameObj.AddComponent<Timer>();
            connectTimer.liveTime = 5;
            connectTimer.Timeout += GetRoomListFailed;
            connectTimer.Start();
        }
    }

    private void GetRoomListSendCallabck(IAsyncResult ar)
    {
        if (ar.IsCompleted)
        {
            int sent = client.EndSend(ar);
            if (sent == 0)
            {
                GetRoomListFailed();
                return;
            }
            else
            {
                //接受服务器回复
                client.BeginReceive(new AsyncCallback(GeneralRecvCallback), null);
            }
        }
    }

    private void GetRoomListSuccess(string message)
    {
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() =>
        {
            Debug.Log(message);
        });
    }

    private void GetRoomListFailed()
    {

        DoOnMainThread.ExecuteOnMainThread.Enqueue(() =>
        {
            if (connectTimer != null)
            {
                connectTimer.Stop();
            }
            this.Shutdown();
        });
    }
    #endregion

    #region == 创建房间 ==
    public void CreateRoom()
    {
        client.Close();
        client = new UdpClient(localEP);
        isCreateRoomFailed = false;
        if (remoteEndPoint != null)
        {
            MpTextMessage message = new MpTextMessage("create_room", gameController.player_name);

            if (txt_connect != null)
            {
                txt_connect.GetComponent<Text>().text = "连接中...";
            }

            try
            {
                client.Connect(remoteEndPoint);
            }
            catch
            {
                CreateRoomFailed();
                return;
            }
            byte[] sendBuffer = message.GetBytes();
            client.BeginSend(sendBuffer, sendBuffer.Length, new AsyncCallback(CreateRoomSendCallback), null);
            //add a timer
            connectTimer = gameController.thisgameObj.AddComponent<Timer>();
            connectTimer.liveTime = 5;
            connectTimer.Timeout += CreateRoomFailed;
            connectTimer.Start();
        }
    }

    private void CreateRoomSendCallback(IAsyncResult ar)
    {
        if (ar.IsCompleted)
        {
            int sent = client.EndSend(ar);
            if (sent == 0)
            {
                CreateRoomFailed();
                return;
            }
            else
            {
                //接受服务器回复
                client.BeginReceive(new AsyncCallback(GeneralRecvCallback), null);
            }
        }
    }

    private void CreateRoomSuccess(string s)
    {
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() =>
        {
            string[] temp = s.Split(',');

            currentRoomId = int.Parse(temp[1]);

            if (txt_connect != null)
            {
                txt_connect.GetComponent<Text>().text = "";
            }

            if (connectTimer != null)
            {
                connectTimer.Stop();
            }

            gameController.panel_multiplay_room_inscene = UnityEngine.MonoBehaviour.Instantiate(gameController.panel_multiplay_room, gameController.canvas.transform);
            gameController.isMultiPlaySpawned = true;
            //anim
            Animation anim = gameController.panel_multiplay_room_inscene.GetComponent<Animation>();
            anim.Play("anim_panel_multiplay_room");
            Animation anim_parent = gameController.panel_multiplay_inscene.GetComponent<Animation>();
            anim_parent.Play("anim_panel_multiplay_out_left");

            //面板都设置成默认的样式

        });
    }

    private void CreateRoomFailed()
    {
        if (!isCreateRoomFailed)
        {
            isCreateRoomFailed = true;
            DoOnMainThread.ExecuteOnMainThread.Enqueue(() =>
            {
                if (txt_connect != null)
                {
                    txt_connect.GetComponent<Text>().text = "";
                }

                GameObject failedDialog = UnityEngine.MonoBehaviour.Instantiate(gameController.dialog_multiplay, gameController.canvas.transform);
                Text txt_msg = failedDialog.transform.Find("dialog").Find("txt_msg").GetComponent<Text>();
                txt_msg.text = "遇到错误，无法创建房间";

                //设置错误对话框
                anim_ctrl_multiplay_fail failedDialog_ctrl = failedDialog.GetComponent<anim_ctrl_multiplay_fail>();
                failedDialog_ctrl.failure = "create_room";

                if (connectTimer != null)
                {
                    connectTimer.Stop();
                }
                this.Shutdown();
            });
        }
    }
    #endregion
}
