using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hollowBrickController : brickController {

    public override void destroyBrick()
    {
        statController.hollowBrickCount++;
        base.destroyBrick();
    }
}
