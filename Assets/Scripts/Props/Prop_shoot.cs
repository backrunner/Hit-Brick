using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_shoot : Prop {

    //pad
    public GameObject shoot_launcher;

    public override void padGot()
    {
        Transform launcher = pad.transform.Find("Prop_shoot_launcher(Clone)");
        if (launcher == null)
        {
            Instantiate(shoot_launcher, pad.transform);
        } else
        {
            //如果之前捡到过则重置livetime
            Prop_shoot_launcher ctrl = launcher.gameObject.GetComponent<Prop_shoot_launcher>();
            ctrl.resetliveTime();
        }
        Destroy(gameObject);
    }
}
