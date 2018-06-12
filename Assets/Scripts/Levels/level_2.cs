using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_2 : Level {

    private float targetAngle1 = 0;
    private float targetAngle2 = 0;

	void Start () {
        GameObject group1 = new GameObject();
        while (targetAngle1 >= -180)
        {
            GameObject obj = new GameObject();
            GameObject brick = Instantiate(normalBrick, obj.transform);
            levelController.bricks.Add(brick);
            brick.transform.localPosition = new Vector3(0, 2.2f, 0);
            obj.transform.localRotation = Quaternion.Euler(0, 0, targetAngle1);
            obj.transform.parent = group1.transform;
            targetAngle1 -= 30;
        }
        group1.transform.position = new Vector3(5.2f, 0, 0);
        GameObject group2 = new GameObject();
        while (targetAngle2 <= 180)
        {
            GameObject obj = new GameObject();
            GameObject brick = Instantiate(normalBrick, obj.transform);
            levelController.bricks.Add(brick);
            brick.transform.localPosition = new Vector3(0, 2.2f, 0);
            obj.transform.localRotation = Quaternion.Euler(0, 0, targetAngle2);
            obj.transform.parent = group2.transform;
            targetAngle2 += 30;
        }
        group2.transform.position = new Vector3(-5.2f, 0, 0);
    }
}
