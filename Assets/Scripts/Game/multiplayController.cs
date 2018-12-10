using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Threading;

public class MultiplayController
{

    public UdpClient client = null;

    public IPEndPoint remoteEndPoint = null;
    public IPAddress remoteIP = null;
    public int remotePort = 0;

    private IPEndPoint remoteSenderEP = null;

    //ui
    private GameObject txt_connect;

    //timer
    private Timer connectTimer;

    //flags
    private bool isConnectFailed = false;

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

    public void TryConnect()
    {
        client = new UdpClient();
        client.Client.Blocking = false;
        if (remoteEndPoint != null)
        {
            MpTextMessage message = new MpTextMessage("connect", gameController.player_name);
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
                Debug.Log(client.Client.RemoteEndPoint.ToString() + " " + client.Client.LocalEndPoint.ToString());
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
                UnityEngine.MonoBehaviour.Instantiate(gameController.dialog_multiplay, gameController.canvas.transform);
                if (connectTimer != null)
                {
                    connectTimer.Stop();
                }
                this.Shutdown();
            });
        }
    }
}
