using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_reverse_child : MonoBehaviour {

    //持续时间
    public float liveTime;
    private float originliveTime;

    //pad
    private GameObject pad;
    private PadController ctrl;

    private void Awake()
    {
        originliveTime = liveTime;
    }

    private void Start()
    {
        //初始化pad
        if (levelController.pad != null)
        {
            pad = levelController.pad;
        } else
        {
            pad = GameObject.Find("Pad");
            if (pad != null)
            {
                levelController.pad = pad;
            }
        }
        ctrl = pad.GetComponent<PadController>();
        //反转
        ctrl.reverse();
    }

    private void Update()
    {
        if (liveTime > 0)
        {
            liveTime -= Time.deltaTime;
        } else
        {
            //解除反转
            ctrl.removeReverse();
            Destroy(gameObject);
        }
    }

    //reset
    public void resetliveTime()
    {
        liveTime = originliveTime;
    }
}
