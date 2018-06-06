using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anim_coinreward : MonoBehaviour {

    private Text text;

    private long current;   //当前
    private long target;    //目标

    public long delta;

    private bool trigger;   //开关

    private void Awake()
    {
        //init
        trigger = false;
        if (delta <= 0)
        {
            delta = 17;
        }
    }

    private void Start()
    {
        text = GetComponent<Text>();
        if (text != null)
        {
            current = long.Parse(text.text);
        } else
        {
            current = 0;
        }
    }

    private void Update()
    {
        forward();
    }

    public void forwardNumber(long number)
    {
        target = number;
        trigger = true;
    }

    //加数
    private void forward()
    {
        if (trigger)
        {
            if (current < target)
            {
                current += delta;
            }
            else
            {
                current = target;
                trigger = false;
            }
            text.text = current.ToString();
        }
    }
}
