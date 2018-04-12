using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour {

    public bool isAttracted;

    //GameObjects
    public GameObject pad;

    //初始球速
    public float initMoveSpeed;

    //位置卡死检测的时间线
    //卡住判定时间
    public float time_check_deadline;
    //卡死判定阀值
    public float check_limit;

    //位置记录和持续时间
    private Vector3 recordedPosition;
    private float position_deltaTime;

    //记录位置是否卡住
    private bool isStack_x;
    private bool isStack_y;

	// Use this for initialization
	void Start () {
        //游戏未开始ball附在pad上
		if (!levelController.isLevelStarted)
        {
            isAttracted = true;
        }
        //如果pad没有指定则查找
        if (pad == null)
        {
            pad = GameObject.Find("Pad");
        }
        //初始化变量
        recordedPosition = transform.position;
        position_deltaTime = 0;
        isStack_x = false;
        isStack_y = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttracted)
        {
            attachToPad();
            launchBall();
        } else
        {
            //如果球未吸附在pad上，则检测球的位置是否出现卡死
            checkPosition();
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
                Vector2 force = new Vector2(0f,initMoveSpeed);
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                //开启ball的trail render
                this.gameObject.GetComponent<TrailRenderer>().enabled = true;
            }
        }
    }

    //位置检查
    void checkPosition()
    {
        //球没有卡在y轴
        if (!isStack_y)
        {
            if (Mathf.Abs(recordedPosition.x - transform.position.x) <= check_limit)
            {
                isStack_x = true;              
                position_deltaTime += Time.deltaTime;
                //当卡住的时间超过限定时间
                if (position_deltaTime > time_check_deadline)
                {
                    Debug.Log("Ball Stacked");
                    //分左右两侧给球施加一个反向外力解决卡位
                    if (transform.position.x <= 0)
                    {
                        Vector2 force = new Vector2(10f, 0f);
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                        updateRecordPosition();
                    }
                    else
                    {
                        Vector2 force = new Vector2(-10f, 0);
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                        updateRecordPosition();
                    }
                }
            }
            else
            {
                if (isStack_x)
                {
                    position_deltaTime = 0;
                }
                isStack_x = false;
            }
        }

        //球没有卡在x轴
        if (!isStack_x)
        {
            if (Mathf.Abs(recordedPosition.y - transform.position.y) <= check_limit)
            {
                isStack_y = true;
                position_deltaTime += Time.deltaTime;
                //当卡住的时间超过限定时间
                if (position_deltaTime > time_check_deadline)
                {
                    Debug.Log("Ball Stacked");
                    //分上下两侧给球施加一个反向外力解决卡位
                    if (transform.position.y <= 0)
                    {
                        Vector2 force = new Vector2(0, 10f);
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                        updateRecordPosition();
                    } else
                    {
                        Vector2 force = new Vector2(0, -10f);
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                        updateRecordPosition();
                    }
                }
            }
            else
            {
                if (isStack_y)
                {
                    position_deltaTime = 0;
                }
                isStack_y = false;
            }
        }
        //记录新的位置
        if (!isStack_x && !isStack_y)
        {
            recordedPosition = transform.position;
        }
    }

    //更新记录的位置
    public void updateRecordPosition()
    {
        recordedPosition = transform.position;
        position_deltaTime = 0;
        isStack_x = false;
        isStack_y = false;
    }
}
