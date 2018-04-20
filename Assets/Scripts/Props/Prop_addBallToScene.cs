using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_addBallToScene : Prop {

    //新球个数
    public int count;

    public override void padGot()
    {
        for (int i = 0; i < count; i++)
        {
            levelController.newBall();
        }
        Destroy(gameObject);
    }

}
