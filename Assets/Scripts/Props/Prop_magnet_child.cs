using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_magnet_child : MonoBehaviour {

    //持续时间
    public float liveTime;
    private float originLiveTime;
    //pad
    private GameObject pad;

    private void Awake()
    {
        //初始化变量
        originLiveTime = liveTime;
    }

    private void Start()
    {
        if (levelController.pad != null)
        {
            pad = levelController.pad;
            pad.AddComponent<Pad_magnet>();
        } else
        {
            pad = GameObject.Find("Pad");
            if (pad != null)
            {
                //给Pad添加新的代码
                pad.AddComponent<Pad_magnet>();
            }
        }
        
    }

    private void Update()
    {
        if (liveTime > 0)
        {
            liveTime -= Time.deltaTime;
        } else
        {
            //移除magnet代码
            Destroy(pad.GetComponent<Pad_magnet>());
            Destroy(gameObject);
        }
    }

    public void resetLiveTime()
    {
        liveTime = originLiveTime;
    }
}
