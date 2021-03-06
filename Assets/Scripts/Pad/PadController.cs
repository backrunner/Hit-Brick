﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadController : MonoBehaviour
{

    //当前目标长度缩放比例
    public float targetScale;
    //限定最大的scale
    public float maxScale;
    private float minScale; //2-maxScale
    //scale乘数
    public float maxScaleMulti;
    public float minScaleMulti;
    //每次改变的scale 最多改变2次
    public float deltaScale;

    //运动方向
    public float moveToward;

    //待发射的球的列表
    public ArrayList ballLaunchList;

    //UI
    private GameObject canvas;
    public GameObject text_waitforLaunch; //prefab
    private GameObject text_waitforLaunch_inscene; //场景内

    //Anim
    public GameObject anim_padlength; //板子长度变化动画

    //Laser
    public GameObject laser;

    void Awake()
    {
        //初始化列表
        ballLaunchList = new ArrayList();
        //初始化变量
        //板子长度变化量计算
        targetScale = transform.localScale.x;
        minScale = 2 * transform.localScale.x - maxScale;
        deltaScale = (maxScale - transform.localScale.x) / 2;
        //初始化运动方向为正向
        moveToward = 1;
    }

    void Start()
    {
        //初始化canvas
        canvas = levelController.canvas;
    }

    // Update is called once per frame
    void Update()
    {
        //检查launchBall的操作
        ballLaunchCheck();
        //游戏未结束且没有暂停的情况下允许移动
        if (!levelController.isGameOver && !levelController.isLevelPaused)
        {
            moveWithMouse();
        }
    }

    void moveWithMouse()
    {
        Vector3 padPos = new Vector3(0f, this.transform.position.y, 0);
        //获取鼠标坐标映射到游戏场景
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x * moveToward;
        //计算pad坐标，保证pad不出屏幕
        //pad长度为1.2u
        padPos.x = Mathf.Clamp(mousePos, (-7.62f - (1f - transform.localScale.x)), (7.62f + (1f - transform.localScale.x)));
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
                if (!levelController.isLevelPaused && !levelController.isGameOver)
                {
                    ball_ctrl.launchBall();
                    ballLaunchList.Remove(ball);
                    //如果发射后球数小于等于1则删除text
                    if (ballLaunchList.Count <= 1)
                    {
                        Destroy(text_waitforLaunch_inscene);
                    }
                    else
                    {
                        Text text = text_waitforLaunch_inscene.GetComponent<Text>();
                        text.text = "+" + (ballLaunchList.Count - 1);
                    }
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
            }
            else
            {
                while (canvas == null)
                {
                    canvas = levelController.canvas;
                }
                Transform t = canvas.transform.Find("Text_waitforLaunch");
                if (t != null)
                {
                    text_waitforLaunch = t.gameObject;
                    Text text = text_waitforLaunch_inscene.GetComponent<Text>();
                    text.text = "+" + (ballLaunchList.Count - 1);
                }
                else
                {
                    text_waitforLaunch_inscene = Instantiate(text_waitforLaunch, canvas.transform);
                    Text text = text_waitforLaunch_inscene.GetComponent<Text>();
                    text.text = "+" + (ballLaunchList.Count - 1);
                }
            }
        }
    }

    //扩展板子长度
    public void extendPad()
    {
        if (targetScale < maxScale)
        {
            //Debug.Log("Change");
            if (targetScale >= 1)
            {
                targetScale += deltaScale * maxScaleMulti;
            }
            else
            {
                targetScale += deltaScale * minScaleMulti;
            }
            Transform anim_trans = transform.Find("Anim_padLength(Clone)");
            if (anim_trans == null)
            {
                Instantiate(anim_padlength, transform);
            }
        }
    }

    //压缩板子长度
    public void compactPad()
    {
        if (targetScale > minScale)
        {
            //Debug.Log("Change");
            if (targetScale <= 1)
            {
                targetScale -= deltaScale * minScaleMulti;
            }
            else
            {
                targetScale -= deltaScale * maxScaleMulti;
            }
            Transform anim_trans = transform.Find("Anim_padLength(Clone)");
            if (anim_trans == null)
            {
                Instantiate(anim_padlength, transform);
            }
        }
    }

    //反转方向
    public void reverse()
    {
        moveToward = -1;
    }
    public void removeReverse()
    {
        moveToward = 1;
    }

    //发射激光
    public void launchLaser()
    {
        Instantiate(laser, new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z),new Quaternion(0,0,0,0));
    }
}
