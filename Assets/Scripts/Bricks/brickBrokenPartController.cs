using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickBrokenPartController : MonoBehaviour {

    //类型
    //1 - left
    //2 - right
    public short part_type;

    //效果持续时间
    public float liveTime;
    //透明度差值（每次）
    private float deltaAlpha;
    //Sprite Render
    private SpriteRenderer render;

    private void Start()
    {
        //调整重心
        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        Transform centerofMass = transform.Find("centerofMass");
        rigidbody.centerOfMass = centerofMass.position;

        //deltaAlpha初始化
        if (Time.deltaTime>0)
        {
            deltaAlpha = 1f / (liveTime / Time.deltaTime);
        } else
        {
            deltaAlpha = 1f / (liveTime / 0.167f);
        }

        //render初始化
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //透明度渐变
        processAlpha();
        //清理
        if (transform.position.y <= -5.6f)
        {
            Destroy(gameObject);
        }
    }

    //处理Alpha的变化
    void processAlpha()
    {
        Color t = render.color;
        //当物体因为透明度改变不可见时删除该物体
        if (t.a-deltaAlpha <= 0)
        {
            Destroy(gameObject);
        }
        t = new Color(t.r, t.g, t.b, t.a - deltaAlpha);
        render.color = t;
    }

    public void throwout(Vector3 ballPosition)
    {
        //定义刚体和力
        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2();

        //球从下面打
        if (ballPosition.y <= transform.position.y)
        {            
            //分情况向部件施加力
            switch (part_type)
            {
                case 1:
                    force = new Vector2(Random.Range(-175f,-65f),Random.Range(35f, 95f));
                    rigidbody.AddForceAtPosition(force, ballPosition);
                    break;
                case 2:
                    force = new Vector2(Random.Range(65f, 175f), Random.Range(35f, 95f));
                    rigidbody.AddForceAtPosition(force, ballPosition);
                    break;
            }
        } else
        {
            //球从上面打
            switch (part_type)
            {
                case 1:
                    force = new Vector2(Random.Range(-125f, -55f), Random.Range(-95f, -35f));
                    rigidbody.AddForceAtPosition(force, ballPosition);
                    break;
                case 2:
                    force = new Vector2(Random.Range(55f, 125f), Random.Range(-95f, -35f));
                    rigidbody.AddForceAtPosition(force, ballPosition);
                    break;
            }
        }
    }
}
