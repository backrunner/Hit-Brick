using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickController : MonoBehaviour {
    //定义破裂的砖块
    public GameObject brokenBrick;
    //球
    public GameObject ball;

    //Particles
    public GameObject particle_destory;

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
            //指定球物体
            ball = collision.gameObject;
            if (!ball_ctrl.isPowerful)
            {                
                //如果球打到了砖块则不认为球卡住
                ball_ctrl.updateRecordPosition();
                ballHit_collision();
            } else
            {
                //如果球打到了砖块则不认为球卡住
                ball_ctrl.updateRecordPosition();
                //Powerful状态下直接摧毁砖块
                destroyBrick();
            }
        } else {
            //检测到bullet
            Prop_shoot_bullet bullet_ctrl = collision.gameObject.GetComponent<Prop_shoot_bullet>();
            if (bullet_ctrl != null && collision_type == 1) 
            {
                //将bullet视作ball
                ball = collision.gameObject;
                ballHit_collision();
                Destroy(collision.gameObject);
            }
        }
    }

    //Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballController ball_ctrl = collision.gameObject.GetComponent<ballController>();
        //检测碰撞的物体是否为ball
        if (ball_ctrl != null)
        {
            if (ball_ctrl.isPowerful || collision_type == 2)
            {
                ball = collision.gameObject;
                if (!ball_ctrl.isPowerful)
                {
                    //如果球打到了砖块则不认为球卡住
                    ball_ctrl.updateRecordPosition();
                    ballHit_trigger();
                }
                else
                {
                    //如果球打到了砖块则不认为球卡住
                    ball_ctrl.updateRecordPosition();
                    //Powerful状态下直接摧毁砖块
                    destroyBrick();
                }
            }
        } else
        {
            Prop_shoot_bullet bullet_ctrl = collision.gameObject.GetComponent<Prop_shoot_bullet>();
            if (bullet_ctrl != null && collision_type == 2)
            {
                ball = collision.gameObject;
                ballHit_trigger();
                Destroy(collision.gameObject);
            }
        }
    }

    public virtual void ballHit_trigger()
    {
        destroyBrick();
    }

    public virtual void ballHit_collision()
    {
        destroyBrick();
    }

    //销毁砖块的公共方法
    public virtual void destroyBrick()
    {
        playDestoryParticle();
        throwOutBrokenParts();
        destoryObj();
    }

    public virtual void playDestoryParticle()
    {
        Instantiate(particle_destory, ball.transform.position, new Quaternion(0, 0, 0, 0));
    }

    public virtual void throwOutBrokenParts()
    {
        //刷新破碎部件
        GameObject broken = Instantiate(brokenBrick, gameObject.transform.position, gameObject.transform.rotation);
        broken.transform.localScale = transform.localScale;
        brickBrokenController ctrl = broken.GetComponent<brickBrokenController>();
        ctrl.throwout(ball.transform.position);        
    }

    //销毁砖块obj
    public virtual void destoryObj()
    {
        //销毁物体并减少计数
        levelController.bricks.Remove(gameObject);
        Destroy(gameObject);        
    }
}
