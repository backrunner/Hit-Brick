using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particles_triangle : MonoBehaviour {

    //移动速度
    public float moveSpeed;
    //粒子存活时间
    public float liveTime;
    private float deltaScale;

	// Use this for initialization
	void Start () {
        //计算每次缩放的差值
        float randomScale = Random.Range(0.75f, 1.1f);
        transform.localScale = new Vector3(randomScale, randomScale, 1);
        deltaScale = transform.localScale.x / (liveTime * Random.Range(0.7f,1.5f));
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}

    void move()
    {
        this.transform.transform.Translate(new Vector3(0,1,0) * moveSpeed * Time.deltaTime);
        //缩放
        Vector3 scale = transform.localScale;
        scale.x -= Time.deltaTime * deltaScale;
        //如果粒子缩放到不可见则销毁
        if (scale.x <= 0)
        {
            Destroy(gameObject);
        }
        scale.y -= Time.deltaTime * deltaScale;
        transform.localScale = scale;
    }
}
