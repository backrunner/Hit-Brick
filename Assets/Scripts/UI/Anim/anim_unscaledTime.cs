using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_unscaledTime : MonoBehaviour {

    Animation anim;

    public float progress;

    public bool isReverse;

    public string animName;

    private void Awake()
    {
        //初始化
        progress = 0;
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        if (isReverse)
        {
            progress -= Time.unscaledDeltaTime*1.3f;
            if (progress < 0)
            {
                Messenger.Broadcast(animName + " rewinded");
                Destroy(gameObject);
            }
        }
        else
        {
            progress += Time.unscaledDeltaTime;
        }
        anim[animName].normalizedTime = progress / anim[animName].length;
        anim.Sample();
    }

}
