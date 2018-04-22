using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_powerful_child : MonoBehaviour {

    //持续时间
    public float liveTime;
    private float originLiveTime;
    //ball
    private GameObject ball;
    private ballController ctrl;

    private void Awake()
    {
        //初始化变量
        originLiveTime = liveTime;
    }

    void Start () {
        //初始化ball
        ball = transform.parent.gameObject;
        ctrl = ball.GetComponent<ballController>();
        ctrl.isPowerful = true;
        //修改砖块的碰撞参数
        for (int i = 0; i < levelController.bricks.Count; i++) {
            GameObject obj = (GameObject)levelController.bricks[i];
            obj.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
	}
	
	void Update () {
		if (liveTime > 0)
        {
            liveTime -= Time.deltaTime;
        } else
        {
            ctrl.isPowerful = false;
            //还原砖块的碰撞参数
            for (int i = 0; i < levelController.bricks.Count; i++)
            {
                GameObject obj = (GameObject)levelController.bricks[i];
                if (obj.GetComponent<brickController>().collision_type == 1)
                {
                    obj.GetComponent<PolygonCollider2D>().isTrigger = false;
                }
            }
            Destroy(gameObject);
        }
	}

    //重置持续时间
    public void resetLiveTime()
    {
        liveTime = originLiveTime;
    }

}
