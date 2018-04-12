using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hollowBrickController : brickController {

	void Start () {
		
	}
	
	void Update () {
		
	}

    public override void ballHit_trigger()
    {
        GameObject broken = Instantiate(brokenBrick, gameObject.transform.position, gameObject.transform.rotation);
        brickBrokenController ctrl = broken.GetComponent<brickBrokenController>();
        ctrl.throwout(ball.transform.position);
        Destroy(gameObject);
    }
}
