using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_Prop : Anim {
    
    private float deltaScale;
    private float currentScale;

    void Start () {
        //计算每次变化量
        deltaScale = Time.deltaTime / liveTime;
        Debug.Log(deltaScale);
        //初始化
        currentScale = 0f;
        liveTime = 0.5f;
	}
	
	void Update () {
		if (liveTime > 0)
        {
            liveTime -= Time.deltaTime;
            processScale();
        } else
        {
            Destroy(this);
        }
	}

    //处理缩放
    void processScale()
    {
        currentScale += deltaScale;
        gameObject.transform.localScale = new Vector3(currentScale, currentScale, 1);
    }
}
