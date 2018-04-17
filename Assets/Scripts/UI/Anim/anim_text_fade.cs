using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_text_fade : MonoBehaviour {

    //动画的类型
    public string type;
    //持续时间
    public float liveTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void play()
    {
        switch (type)
        {
            case "fadein":
                break;
            case "fadeout":
                break;
        }
    }
}
