using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text_waitingToLaunch : MonoBehaviour {

    private GameObject pad;

    void Start () {
        pad = levelController.pad;
	}
	
	// Update is called once per frame
	void Update () {
        trackPad();
	}

    //追踪板子
    void trackPad()
    {
        Vector3 positon = pad.transform.position;
        positon.x += 0.35f;
        positon.y += 0.18f;
        transform.position = positon;
    }
}
