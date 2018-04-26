using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_addSpeed : Prop
{

    public override void padGot()
    {

        foreach (GameObject obj in levelController.ballList)
        {
            ballController ctrl = obj.GetComponent<ballController>();
            ctrl.addSpeed();
        }
        Destroy(gameObject);
        base.padGot();
    }

}
