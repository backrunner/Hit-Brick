using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_shoot_bullet : MonoBehaviour {

    //运动方向
    private Vector3 moveToward;
    public float moveSpeed;
    public float addSpeed;

    private void Awake()
    {
        //初始化变量
        moveToward = new Vector3(0, 1, 0);
    }
	
	// Update is called once per frame
	void Update () {
        move();
	}

    void move()
    {
        //移动
        transform.Translate(moveToward * moveSpeed * Time.deltaTime);
        //处理加速度
        if (addSpeed != 0)
        {
            moveSpeed += addSpeed;
        }
        //清除屏幕外的obj
        if (transform.position.y >= 5.5f)
        {
            Destroy(gameObject);
        }
    }

}
