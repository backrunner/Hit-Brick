using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_ballSizeDown : Prop {

    public override void padGot()
    {
        for (int i = 0; i < levelController.ballList.Count; i++)
        {
            GameObject obj = (GameObject)levelController.ballList[i];
            ballController ctrl = obj.GetComponent<ballController>();
            ctrl.downBallSize();
        }
        Destroy(gameObject);
    }

}
