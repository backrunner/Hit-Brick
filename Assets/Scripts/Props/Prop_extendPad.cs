using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prop_extendPad : Prop {

    public override void padGot()
    {
        if (levelController.pad != null)
        {
            PadController ctrl = pad.GetComponent<PadController>();
            ctrl.extendPad();
            Destroy(gameObject);
        }
    }
}
