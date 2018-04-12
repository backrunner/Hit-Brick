using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickController : MonoBehaviour {
    //定义破裂的砖块
    public GameObject brokenBrick;
    //球
    public GameObject ball;

    //砖块的碰撞类型
    //1 - collision
    //2 - trigger
    public short collision_type;      

    //Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballController ball_ctrl = collision.gameObject.GetComponent<ballController>();
        //检测碰撞的物体是否为ball
        if (ball_ctrl != null && collision_type == 1)
        {
            ball = collision.gameObject;
            ballHit_collision();
        }
    }

    //Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballController ball_ctrl = collision.gameObject.GetComponent<ballController>();
        //检测碰撞的物体是否为ball
        if (ball_ctrl != null && collision_type == 2)
        {
            ball = collision.gameObject;
            ballHit_trigger();
        }
    }

    public virtual void ballHit_trigger()
    {

    }

    public virtual void ballHit_collision()
    {

    }
}
