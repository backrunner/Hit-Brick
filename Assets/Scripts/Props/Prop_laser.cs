using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_laser : Prop {

    //particle
    public GameObject particle;

    public override void padGot()
    {
        Vector3 position = new Vector3(pad.transform.position.x, pad.transform.position.y+0.25f, pad.transform.position.z);
        GameObject p = Instantiate(particle, position, new Quaternion(0, 0, 0, 0));
        //绑定粒子和pad
        p.transform.parent = pad.transform;
        //传递pad
        Prop_laser_particle ctrl = p.GetComponent<Prop_laser_particle>();
        ctrl.pad = pad;
        Destroy(gameObject);
        base.padGot();
    }
}
