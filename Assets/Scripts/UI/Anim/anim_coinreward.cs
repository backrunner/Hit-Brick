using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anim_coinreward : MonoBehaviour
{

    private Text text;

    private long current;   //当前
    private long target;    //目标

    private long delta;

    private bool trigger;   //开关

    private void Awake()
    {
        //init
        trigger = false;
        delta = 17;
    }

    private void Start()
    {
        text = GetComponent<Text>();
        if (text != null)
        {
            current = playerController.coin;
            text.text = playerController.coin.ToString();
        }
        else
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
        if (Mathf.Abs(target - current) <= 10)
        {
            delta = 1;
        }
        else
        {
            if (Mathf.Abs(target - current) <= 50)
            {
                delta = 3;
            }
            else
            {
                if (Mathf.Abs(target - current) <= 200)
                {
                    delta = 7;
                }
                else
                {
                    if (Mathf.Abs(target - current) <= 2000)
                    {
                        delta = 17;
                    }
                    else
                    {
                        delta = 73;
                    }
                }
            }
        }
        if (target - current < 0)
        {
            delta = -delta;
        }
    }

    //加数
    private void forward()
    {
        if (trigger && delta > 0)
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
        if (trigger && delta < 0)
        {
            if (current > target)
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
