using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ballSize : Anim {

    private float deltaScale;
    private float deltaScale_reverse;
    private GameObject ball;
    private ballController ctrl;

    private void Awake()
    {
        ball = transform.parent.gameObject;
    }

    void Start () {
        ctrl = ball.GetComponent<ballController>();
        //计算每帧变化量
        if (Time.deltaTime > 0)
        {
            deltaScale = ctrl.deltaScale / (liveTime / Time.deltaTime);
            deltaScale_reverse = ctrl.deltaScale_reverse / (liveTime / Time.deltaTime);
        }
        else
        {
            deltaScale = ctrl.deltaScale / (liveTime / 0.0167f);
            deltaScale_reverse = ctrl.deltaScale_reverse / (liveTime / 0.0167f);
        }
    }
	
	void Update () {
        play();
	}

    void play()
    {
        float scale = ball.transform.localScale.x;
        if (scale < ctrl.targetScale)
        {
            //根据两个状态的scale做不同的缩放
            if (scale > ctrl.originScale)
            {
                scale += deltaScale;
            }
            else
            {
                scale += deltaScale_reverse;
            }
            //上限判断
            if (scale >= ctrl.targetScale)
            {
                scale = ctrl.targetScale;
                ball.transform.localScale = new Vector3(scale, scale , 1);
                Destroy(gameObject);
                return;
            }
            ball.transform.localScale = new Vector3(scale, scale , 1);
        }
        else
        {
            //根据两个状态的scale做不同的缩放
            if (scale > ctrl.originScale)
            {
                scale -= deltaScale;
            } else
            {
                scale -= deltaScale_reverse;
            }
            //下限判断
            if (scale <= ctrl.targetScale)
            {
                scale = ctrl.targetScale;
                ball.transform.localScale = new Vector3(scale, scale, 1);
                Destroy(gameObject);
                return;
            }
            ball.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
