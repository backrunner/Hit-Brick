using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalBrickController : brickController {

    public override void destroyBrick()
    {
        statController.normalBrickCount++;
        base.destroyBrick();
    }
}
