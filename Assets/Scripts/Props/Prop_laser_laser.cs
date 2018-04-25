using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_laser_laser : MonoBehaviour {

    //每次的长度变化量
    public float deltaHeight;
    //加速量
    public float addSpeed;

    //renderer
    private SpriteRenderer render;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }
	
	void Update () {
		
	}
}
