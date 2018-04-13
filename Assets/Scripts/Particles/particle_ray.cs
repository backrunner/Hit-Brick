using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_ray : MonoBehaviour {

    //移动速度
    public float moveSpeed;
    //粒子存活时间
    public float liveTime;

    //Render
    private SpriteRenderer render;

    //Alpha变化量
    private float deltaAlpha;

    void Start () {
        //初始化render
        render = GetComponent<SpriteRenderer>();
        deltaAlpha = 1f / (liveTime * 60);
        //随机长度
        Vector3 scale = new Vector3(Random.Range(0.85f, 1f), Random.Range(0.8f, 1.2f), 1);
        transform.localScale = scale;
    }
	
	void Update () {
        move();
        processAlpha();
	}

    //移动
    void move()
    {
        this.transform.transform.Translate(new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime);
    }

    //处理Alpha
    void processAlpha()
    {
        Color t = render.color;
        //当物体因为透明度改变不可见时删除该物体
        if (t.a - deltaAlpha <= 0)
        {
            Destroy(gameObject);
        }
        t = new Color(t.r, t.g, t.b, t.a - deltaAlpha);
        render.color = t;
    }
}
