using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_mainmenu_initname : MonoBehaviour {

    public GameObject nameInputPanel;
    public GameObject canvas;

    void Start () {
        //如果游戏没有初始化
		if (!gameController.isInited)
        {
            if (canvas == null)
            {
                canvas = GameObject.Find("Canvas");
            }
            Instantiate(nameInputPanel, canvas.transform);
        } else
        {
            Destroy(this);
        }
	}

}
