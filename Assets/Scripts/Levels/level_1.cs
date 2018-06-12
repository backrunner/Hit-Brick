using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_1 : Level {
      
    private float targetAngle = 0;

	void Start () {
        while (targetAngle < 360) {
            GameObject obj = new GameObject();
            GameObject brick = Instantiate(normalBrick, obj.transform);
            levelController.bricks.Add(brick);
            brick.transform.localPosition = new Vector3(0, 2.5f, 0);
            obj.transform.localRotation = Quaternion.Euler(0, 0, targetAngle);
            targetAngle += 30f;
        }
        Destroy(this);
	}

}
