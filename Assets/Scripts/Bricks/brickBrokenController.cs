using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickBrokenController : MonoBehaviour {

    //左右部分的名称
    public string name_left;
    public string name_right;

    private void Update()
    {
        //没有子物体时执行清理
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    public void throwout(Vector3 ballPosition)
    {
        //获取左右两部分执行throwout
        GameObject left = transform.Find(name_left).gameObject;
        GameObject right = transform.Find(name_right).gameObject;
        brickBrokenPartController ctrl_left = left.GetComponent<brickBrokenPartController>();
        brickBrokenPartController ctrl_right = right.GetComponent<brickBrokenPartController>();
        ctrl_left.throwout(ballPosition);
        ctrl_right.throwout(ballPosition);
    }

}
