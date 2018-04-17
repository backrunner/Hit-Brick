﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadController : MonoBehaviour {

    //待发射的球的列表
    public ArrayList ballLaunchList;

    //UI
    public GameObject canvas;
    public GameObject text_waitforLaunch;
    private GameObject text_waitforLaunch_inscene;

	// Use this for initialization
	void Awake () {
        ballLaunchList = new ArrayList();
	}

    void Start()
    {
        //初始化canvas
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas");
        }
    }

    // Update is called once per frame
    void Update () {
        //检查launchBall的操作
        ballLaunchCheck();
        //游戏未结束的情况下允许移动
        if (!levelController.isGameOver)
        {
            moveWithMouse();
        }
	}

    void moveWithMouse()
    {
        Vector3 padPos = new Vector3(0f, this.transform.position.y, 0);
        //获取鼠标坐标映射到游戏场景
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        //计算pad坐标，保证pad不出屏幕
        //pad长度为1.2u
        padPos.x = Mathf.Clamp(mousePos, (-7.62f-(1f-transform.localScale.x)*0.6f),(7.62f+(1f-transform.localScale.x)*0.6f));
        this.transform.position = padPos;
    }

    //检查用户的操作
    void ballLaunchCheck()
    {
        //判断自定义按键launchBall
        if (Input.GetButtonDown("launchBall"))
        {
            if (ballLaunchList.Count > 0)
            {
                GameObject ball = (GameObject)ballLaunchList[0];
                ballController ball_ctrl = ball.GetComponent<ballController>();
                ball_ctrl.launchBall();
                ballLaunchList.Remove(ball);
                //如果发射后球数小于等于1则删除text
                if (ballLaunchList.Count <= 1)
                {
                    Destroy(text_waitforLaunch_inscene);
                } else
                {
                    Text text = text_waitforLaunch_inscene.GetComponent<Text>();
                    text.text = "+" + (ballLaunchList.Count - 1);
                }
            }
        }
    }

    //添加球到待发射列表
    public void addBallToLaunchList(GameObject obj)
    {
        ballLaunchList.Add(obj);
        //如果待发射球数在一个以上，则显示+X的UI
        if (ballLaunchList.Count > 1)
        {            
            if (text_waitforLaunch_inscene != null)
            {
                Text text = text_waitforLaunch_inscene.GetComponent<Text>();
                text.text = "+" + (ballLaunchList.Count - 1);
            } else
            {
                Transform t = canvas.transform.Find("Text_waitforLaunch");
                if (t != null)
                {
                    text_waitforLaunch = t.gameObject;
                    Text text = text_waitforLaunch_inscene.GetComponent<Text>();
                    text.text = "+" + (ballLaunchList.Count - 1);
                } else
                {
                    text_waitforLaunch_inscene = Instantiate(text_waitforLaunch, canvas.transform);
                    Text text = text_waitforLaunch_inscene.GetComponent<Text>();
                    text.text = "+" + (ballLaunchList.Count - 1);
                }
            }
        }
    }
}
