using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_ray_launcher : MonoBehaviour {

    public GameObject particle_ray;

    //粒子数量
    private int particle_count;

    //角度
    private float targetAngle;
    private float deltaAngle;

	void Start () {
        //初始化
        particle_count = Random.Range(5, 9);
        targetAngle = Random.Range(0f, 360f);
        deltaAngle = 360f / particle_count;
        //定义存放
        GameObject rayParticleGroup = new GameObject();
        rayParticleGroup.transform.position = transform.position;
        //附加代码
        rayParticleGroup.AddComponent<particle_ray_group>();
        //发射粒子
        for (int i = 0; i < particle_count; i++)
        {
            Vector3 positon = transform.position;
            positon.y += 0.1f;
            GameObject particle = Instantiate(particle_ray, positon, Quaternion.Euler(0, 0, targetAngle));
            //设定父物体
            particle.transform.parent = rayParticleGroup.transform;
            //改变角度
            targetAngle += deltaAngle;
        }
	}
}
