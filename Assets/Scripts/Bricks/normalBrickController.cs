using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalBrickController : brickController {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void ballHit_collision()
    {
        Destroy(gameObject);
    }
}
