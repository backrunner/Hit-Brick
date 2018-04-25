using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_magnet : Prop {

    public GameObject child;

    public override void padGot()
    {
        Transform child_trans = pad.transform.Find("Prop_magnet_child(Clone)");
        if (child_trans == null)
        {
            Instantiate(child, pad.transform);
        } else
        {
            Prop_magnet_child ctrl = child_trans.gameObject.GetComponent<Prop_magnet_child>();
            ctrl.resetLiveTime(); 
        }
        Destroy(gameObject);
        base.padGot();
    }
}
