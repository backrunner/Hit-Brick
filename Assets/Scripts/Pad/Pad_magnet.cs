using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad_magnet : MonoBehaviour {

    private PadController pad_ctrl;

    private void Start()
    {
        pad_ctrl = GetComponent<PadController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballController ctrl = collision.gameObject.GetComponent<ballController>();
        if (ctrl != null)
        {
            ctrl.isAttracted = true;
            pad_ctrl.addBallToLaunchList(collision.gameObject);
            //关闭尾迹渲染
            ctrl.gameObject.GetComponent<TrailRenderer>().enabled = false;
            //清除力
            ctrl.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }

}
