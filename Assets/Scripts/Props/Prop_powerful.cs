using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_powerful : Prop {

    public override void padGot()
    {
        for (int i = 0; i < levelController.ballList.Count; i++)
        {
            GameObject ball = (GameObject)levelController.ballList[i];
            ballController ctrl = ball.GetComponent<ballController>();
            ctrl.enablePowerful();
        }
        Destroy(gameObject);
        base.padGot();
    }
}
