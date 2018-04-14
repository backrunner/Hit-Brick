using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadZoneController : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
    {
        ballController ctrl = collision.gameObject.GetComponent<ballController>();
        if (ctrl != null)
        {
            //球进入死亡区域，减少球数            
            if (levelController.leftBall > 0)
            {               
                levelController.newBall();                
            }
            levelController.leftBall = levelController.leftBall - 1;
            Destroy(collision.gameObject);
        }
    }
}
