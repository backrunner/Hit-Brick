using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_panel_inputName : UIAnim {

    //每次的变化量
    private float deltaScale;
    private float currentScale;

    void Start () {
        currentScale = 0;
        deltaScale = Time.deltaTime / liveTime;
	}
	
	void Update () {
        if (liveTime > 0 || currentScale < 1)
        {
            liveTime -= Time.deltaTime;
            currentScale += deltaScale;
            if (currentScale > 1)
            {
                currentScale = 1;
            }
            gameObject.transform.localScale = new Vector3(currentScale, 1, 1);
        } else
        {
            Destroy(this);
        }
	}
}
