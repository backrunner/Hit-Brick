using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_clear : Effect {

    private float deltaTimeScale;
    private float timeScale;

    void Start()
    {
        timeScale = 1;
        deltaTimeScale = Time.fixedDeltaTime / liveTime;
    }

    private void FixedUpdate()
    {
        if (timeScale > 0)
        {
            timeScale -= deltaTimeScale;
            if (timeScale < 0)
            {
                timeScale = 0;
            }
        }
        Time.timeScale = timeScale;
    }

}
