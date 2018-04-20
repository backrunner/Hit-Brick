using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_reverse : Prop {

    //子物体obj
    public GameObject child;

    public override void padGot()
    {
        //验证重复
        Transform child_trans = pad.transform.Find("Prop_reverse_child(Clone)");
        if (child_trans == null)
        {
            Instantiate(child, pad.transform);
        } else
        {
            //重置时间
            Prop_reverse_child ctrl = child_trans.gameObject.GetComponent<Prop_reverse_child>();
            ctrl.resetliveTime();
        }
        Destroy(gameObject);
    }

}
