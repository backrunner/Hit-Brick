using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadZoneController : MonoBehaviour {

    //UI
    public gameUIContorller uictrl;

    //物体进入（球）
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballController ctrl = collision.gameObject.GetComponent<ballController>();
        if (ctrl != null)
        {
            //球进入死亡区域
            GameObject[] ballList = GameObject.FindGameObjectsWithTag("Ball");
            //判断当前场景内的球数 为1则消耗一条命 否则不消耗
            if (ballList.Length == 1)
            {
                if (levelController.leftBall > 0)
                {
                    levelController.newBall();
                }
                levelController.leftBall = levelController.leftBall - 1;
                //更新UI
                gameUIContorller.updateLeftBallUI();
                //销毁游戏物体
                destoryBall(collision.gameObject);
            } else
            {
                destoryBall(collision.gameObject);
                //更改levelController的currentBall为场景内的球
                ballList = GameObject.FindGameObjectsWithTag("Ball");
                levelController.currentBall = ballList[0];
            }             
        }
    }

    void destoryBall(GameObject obj)
    {
        if (obj.transform.Find("Prop_powerful_child(Clone)") != null)
        {
            for (int i = 0; i < levelController.bricks.Count; i++)
            {
                GameObject brick = (GameObject)levelController.bricks[i];
                if (brick.GetComponent<brickController>().collision_type == 1)
                {
                    brick.GetComponent<PolygonCollider2D>().isTrigger = false;
                }
            }
        }
        //从列表里移除
        levelController.ballList.Remove(obj);
        Destroy(obj);
    }

    //物体离开（其他）
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
