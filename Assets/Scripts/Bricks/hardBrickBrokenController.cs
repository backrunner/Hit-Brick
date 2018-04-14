using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hardBrickBrokenController : brickBrokenController {

    public Color brickColor;

	public override void throwout(Vector3 ballPosition)
    {
        //获取左右两部分执行throwout
        GameObject left = transform.Find(name_left).gameObject;
        GameObject right = transform.Find(name_right).gameObject;
        brickBrokenPartController ctrl_left = left.GetComponent<brickBrokenPartController>();
        brickBrokenPartController ctrl_right = right.GetComponent<brickBrokenPartController>();
        //获取Render改变颜色
        SpriteRenderer renderer_left = left.GetComponent<SpriteRenderer>();
        SpriteRenderer renderer_right = right.GetComponent<SpriteRenderer>();
        renderer_left.color = brickColor;
        renderer_right.color = brickColor;
        //扔出部件
        ctrl_left.throwout(ballPosition);
        ctrl_right.throwout(ballPosition);
    }
}
