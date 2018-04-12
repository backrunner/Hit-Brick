using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickController : MonoBehaviour {
    //定义破裂的砖块
    public GameObject brokenBrick;
    //球
    public GameObject ball;

    //砖块的碰撞类型
    //default - none
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
            //如果球打到了砖块则不认为球卡住
            ball_ctrl.updateRecordPosition();
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
            //如果球打到了砖块则不认为球卡住
            ball_ctrl.updateRecordPosition();
            ballHit_trigger();
        }
    }

    public virtual void ballHit_trigger()
    {
        throwOutBrokenParts();
    }

    public virtual void ballHit_collision()
    {
        throwOutBrokenParts();
    }

    public virtual void throwOutBrokenParts()
    {
        GameObject broken = Instantiate(brokenBrick, gameObject.transform.position, gameObject.transform.rotation);
        broken.transform.localScale = transform.localScale;
        brickBrokenController ctrl = broken.GetComponent<brickBrokenController>();
        ctrl.throwout(ball.transform.position);
        Destroy(gameObject);
    }
}
