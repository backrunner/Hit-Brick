using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour {

    public bool isAttracted;

    //GameObjects
    public GameObject pad;

	// Use this for initialization
	void Start () {
        //游戏未开始ball附在pad上
		if (!levelController.isLevelStarted)
        {
            isAttracted = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttracted)
        {
            attachToPad();
            launchBall();
        }
	}

    //绑定ball与pad的运动
    void attachToPad()
    {
        Vector3 position = pad.transform.position;
        position.y += 0.25f;
        this.gameObject.transform.position = position;
    }

    void launchBall()
    {
        if (this.isAttracted)
        {
            //判断自定义按键launchBall
            if (Input.GetButton("launchBall"))
            {
                this.isAttracted = false;
                //向ball施加自定义力
                Vector2 force = new Vector2(0f,250f);
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                //开启ball的trail render
                this.gameObject.GetComponent<TrailRenderer>().enabled = true;
            }
        }
    }
}
