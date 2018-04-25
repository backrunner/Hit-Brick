using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_laser_particle : MonoBehaviour {

    ParticleSystem particle;

    //发射激光的延迟时间
    public float delayTime;

    //发射开关
    private bool islaunched = false;

    public GameObject pad;

	void Start () {
        particle = GetComponent<ParticleSystem>();	
	}

	void Update () {
		if (delayTime>0)
        {
            delayTime -= Time.deltaTime;
        }
        else
        {
            //发射激光
            if (!islaunched)
            {
                PadController ctrl = pad.GetComponent<PadController>();
                ctrl.launchLaser();
                islaunched = true;
            }
            if (particle.isStopped)
            {
                //播放停止后销毁
                Destroy(gameObject);
            }
        }
	}
}
