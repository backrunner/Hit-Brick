using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ballColor : Anim {

    //元素
    private GameObject ball;
    private SpriteRenderer render;
    private TrailRenderer render_trial;

    //颜色
    private Color originColor = Color.white;
    private Color targetColor = new Color(1, 110f / 255f, 45f / 255f, 1);

    //数值
    private float current;
    private float delta; //每秒钟的变化量（百分比）
    public bool toward;

    private void Awake()
    {
        //计算delta
        delta = Time.deltaTime / liveTime;
        //初始化变量
        toward = true;
    }

    void Start () {
        //初始化变量
        ball = transform.parent.gameObject;
        render = ball.GetComponent<SpriteRenderer>();
        render_trial = ball.GetComponent<TrailRenderer>();     
        if (!toward)
        {
            current = 1;
        }
	}
	
	void Update () {
        processColor();
	}

    //处理颜色
    void processColor()
    {
        Color t;

        if (toward)
        {
            if (Time.deltaTime > 0)
            {
                current += delta;
            }            
            t.r = Mathf.Lerp(originColor.r, targetColor.r, current);
            t.g = Mathf.Lerp(originColor.g, targetColor.g, current);
            t.b = Mathf.Lerp(originColor.b, targetColor.b, current);
            t.a = 1;
        } else
        {
            if (Time.deltaTime > 0)
            {
                current -= delta;
            }
            t.r = Mathf.Lerp(targetColor.r, originColor.r, 1-current);
            t.g = Mathf.Lerp(targetColor.g, originColor.g, 1-current);
            t.b = Mathf.Lerp(targetColor.b, originColor.b, 1-current);
            t.a = 1;
        }
        //超过上限，摧毁物体
        if (current >= 1 || current <= 0)
        {
            Destroy(gameObject);
        }

        render.color = t;
        render_trial.startColor = t;
        render_trial.endColor = new Color(t.r, t.g*0.65f, t.b);
    }
}
