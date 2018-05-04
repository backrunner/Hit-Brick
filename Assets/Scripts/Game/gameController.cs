using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {

    //游戏初始化
    public static bool isInited;

    private void Awake()
    {
        isInited = PlayerPrefs.HasKey("player_name");
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
