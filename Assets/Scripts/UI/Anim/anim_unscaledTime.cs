using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_unscaledTime : MonoBehaviour {

    Animation anim;

    float progress;

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
        progress += Time.unscaledDeltaTime;
        anim[animName].normalizedTime = progress / anim[animName].length;
        anim.Sample();
    }

}
