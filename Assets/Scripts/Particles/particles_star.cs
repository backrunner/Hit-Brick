﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particles_star : MonoBehaviour
{

    //移动速度
    public float moveSpeed;
    //粒子存活时间
    public float liveTime;
    private float deltaScale;

    // Use this for initialization
    void Start()
    {
        //随机粒子的大小
        float randomScale = Random.Range(0.35f, 0.62f);
        transform.localScale = new Vector3(randomScale, randomScale, 1);
        //计算每次缩放的差值
        deltaScale = transform.localScale.x / (liveTime * Random.Range(0.6f, 1.3f));
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        this.transform.transform.Translate(new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime);
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