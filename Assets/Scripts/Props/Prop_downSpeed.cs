using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_downSpeed : Prop {

    public override void padGot()
    {
        foreach (GameObject obj in levelController.ballList)
        {
            ballController ctrl = obj.GetComponent<ballController>();
            ctrl.downSpeed();
        }
        Destroy(gameObject);
        base.padGot();
    }
}
