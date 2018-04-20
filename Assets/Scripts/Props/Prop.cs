using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour {

    public float moveSpeed;
    //移动方向
    private Vector3 moveToward;

    //pad
    public GameObject pad;

    private void Awake()
    {
        //初始化
        if (moveSpeed <= 0)
        {
            moveSpeed = 2;
        }
    }

    public virtual void Start()
    {
        //沿y轴负方向运动
        moveToward.y = -1;
    }
    public virtual void Update()
    {
        move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PadController ctrl = collision.gameObject.GetComponent<PadController>();
        if (ctrl != null){
            pad = collision.gameObject;
            padGot();
        }
    }

    public virtual void move()
    {
        transform.Translate(moveToward * moveSpeed * Time.deltaTime);
    }

    public virtual void padGot()
    {

    }
}
