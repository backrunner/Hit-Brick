using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_drop_launcher : MonoBehaviour
{

    //粒子
    public GameObject particle_drop;
    //散射的粒子数量
    public int particle_count;
    //散射角度
    private float targetAngle;
    //记录发射粒子的数量
    private int count;

    // Use this for initialization
    void Start()
    {
        //随机数量
        particle_count = Random.Range(7, particle_count+1);
        //随机一个targetAngle初值
        targetAngle = Random.Range(0f, 360f);
    }

    // Update is called once per frame
    void Update()
    {
        int t = Random.Range(1, 4);
        //计数
        count += t;
        for (int i = 0; i < t; i++)
        {
            //生成particle
            Instantiate(particle_drop, gameObject.transform.position, Quaternion.Euler(0, 0, targetAngle));
            targetAngle +=65f + Random.Range(-15f, 15f);
        }
        //发射完指定数目的粒子，销毁
        if (count >= particle_count)
        {
            Destroy(gameObject);
        }
    }
}
