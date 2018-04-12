using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particles_triangle_collision : MonoBehaviour {

    //用于散射的GameObject
    public GameObject particle_launcher;

    //碰撞检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballController ball = collision.gameObject.GetComponent<ballController>();
        //检测碰撞的物体是否为ball
        if (ball != null)
        {
            GameObject particle = Instantiate(particle_launcher, collision.gameObject.transform.position, new Quaternion(0,0,0,0));
        }
    }

}
