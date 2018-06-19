using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_rotation : MonoBehaviour {

    private float targetAngle;
    //旋转速度（一圈多少秒）
    public float rotateSpeed;
    public bool isClockwise;
    private float deltaAngle;
    
    private void Awake()
    {
        //初始化
        targetAngle = 0;
        deltaAngle = 360 / (rotateSpeed / Time.deltaTime);
    }
	
	void Update () {
        if (!levelController.isLevelPaused)
        {
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
            if (isClockwise)
            {
                targetAngle -= deltaAngle;
            }
            else
            {
                targetAngle += deltaAngle;
            }
        }
	}
}
