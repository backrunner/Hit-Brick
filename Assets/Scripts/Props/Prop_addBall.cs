using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_addBall : Prop {

    public override void padGot()
    {
        levelController.addLeftBall();
        Destroy(gameObject);
    }
}
