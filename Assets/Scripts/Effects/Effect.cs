using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

    public float liveTime;
    public new Camera camera;

    private void Start()
    {
        //设置特效的相机
        SetCamera();
    }
    
    public virtual void SetCamera()
    {
        camera = Camera.main;
    }
}
