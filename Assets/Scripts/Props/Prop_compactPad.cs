using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_compactPad : Prop {

    public override void padGot()
    {
        if (pad != null)
        {
            PadController ctrl = pad.GetComponent<PadController>();
            ctrl.compactPad();                   
        }
        Destroy(gameObject);
        base.padGot();
    }
}
