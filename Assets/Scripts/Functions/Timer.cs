using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool startNow = false;
    public float liveTime = 0;

    public delegate void TimeoutHandler();
    public event TimeoutHandler Timeout;

    private void Update()
    {
        if (startNow) {
            if (liveTime > 0)
            {
                liveTime -= Time.deltaTime;
            } else
            {
                if (Timeout != null)
                {
                    startNow = false;                    
                    Timeout.Invoke();
                    Stop();
                }
            }
        }
    }

    public void Start()
    {
        startNow = true;
    }

    public void Stop()
    {
        startNow = false;
        Timeout = null;
        Destroy(this);
    }
}

