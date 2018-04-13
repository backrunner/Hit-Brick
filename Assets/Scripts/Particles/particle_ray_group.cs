using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_ray_group : MonoBehaviour {

    private GameObject pad;

    private void Start()
    {
        if (levelController.pad != null)
        {
            pad = levelController.pad;
        } else
        {
            pad = GameObject.Find("Pad");
            levelController.pad = pad;
        }
    }

    void Update () {
        attachToBall();
	}

    //和球绑定
    void attachToBall()
    {
        Vector3 position = pad.transform.position;
        position.y += 0.25f;
        transform.position = position;
    }
}
