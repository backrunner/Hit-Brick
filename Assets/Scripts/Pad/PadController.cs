using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moveWithMouse();
	}

    void moveWithMouse()
    {
        Vector3 padPos = new Vector3(0f, this.transform.position.y, 0);
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        padPos.x = Mathf.Clamp(mousePos, -8f, 8f);
        this.transform.position = padPos;
    }
}
