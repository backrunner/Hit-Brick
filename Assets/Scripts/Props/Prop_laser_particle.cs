using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_laser_particle : MonoBehaviour {

    ParticleSystem particle;

    public GameObject pad;

	void Start () {
        particle = GetComponent<ParticleSystem>();	
	}

	void Update () {
		if (particle.isStopped)
        {
            //立即播放结束之后销毁粒子并且发射激光
            PadController ctrl = pad.GetComponent<PadController>();
            ctrl.launchLaser();
            Destroy(gameObject);
        }
	}


}
