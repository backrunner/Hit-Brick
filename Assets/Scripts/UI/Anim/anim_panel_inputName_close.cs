using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_panel_inputName_close : UIAnim {

    //每次的变化量
    private float deltaScale;
    private float currentScale;

    void Start()
    {
        //预定义liveTime
        liveTime = 0.25f;
        //初始化
        currentScale = 1;
        deltaScale = Time.deltaTime / liveTime;
    }

    void Update()
    {
        if (liveTime > 0 || currentScale > 0)
        {
            liveTime -= Time.deltaTime;
            currentScale -= deltaScale;
            if (currentScale < 0)
            {
                currentScale = 0;
            }
            gameObject.transform.localScale = new Vector3(currentScale, 1, 1);
        }
        else
        {
            Destroy(gameObject);
            Messenger.Broadcast("input name panel closed");
        }
    }
}
