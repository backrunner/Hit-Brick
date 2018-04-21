using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_padlength : Anim {

    private GameObject pad;
    private PadController pad_ctrl;
    private float deltaScale;

    void Start () {
        //初始化变量
        pad = transform.parent.gameObject;
        pad_ctrl = pad.GetComponent<PadController>();
        //计算每帧变化量
        if (Time.deltaTime > 0)
        {
            deltaScale = pad_ctrl.deltaScale / (liveTime / Time.deltaTime);
        }
        else
        {
            deltaScale = pad_ctrl.deltaScale / (liveTime / 0.0167f);
        }
	}
	
	void Update () {
        play();
	}

    //动画播放
    void play()
    {
        float scale = pad.transform.localScale.x;
        if (scale < pad_ctrl.targetScale)
        {
            scale += deltaScale;
            if (scale >= pad_ctrl.targetScale)
            {
                scale = pad_ctrl.targetScale;
                pad.transform.localScale = new Vector3(scale, 1, 1);
                Destroy(gameObject);
                return;
            }
            pad.transform.localScale = new Vector3(scale, 1, 1);
        } else
        {
            scale -= deltaScale;
            if (scale <= pad_ctrl.targetScale)
            {
                scale = pad_ctrl.targetScale;
                pad.transform.localScale = new Vector3(scale, 1, 1);
                Destroy(gameObject);
                return;
            }
            pad.transform.localScale = new Vector3(scale, 1, 1);
        }
    }
}
