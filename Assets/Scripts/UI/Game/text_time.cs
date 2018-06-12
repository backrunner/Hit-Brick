using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text_time : MonoBehaviour {

    private Text txt;

	void Start () {
        txt = GetComponent<Text>();
	}
	
	void Update () {
        int min = Mathf.FloorToInt(levelController.currentTime / 60);
        int second = Mathf.FloorToInt(levelController.currentTime - min * 60);
        txt.text = min.ToString()+":"+second.ToString("D2");
	}
}
