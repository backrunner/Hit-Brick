﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_compactPad : Prop {

    public override void padGot()
    {
        if (levelController.pad != null)
        {
            PadController ctrl = levelController.pad.GetComponent<PadController>();
            ctrl.compactPad();
            Destroy(gameObject);
        }
    }
}
