using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blankmenuController : MonoBehaviour {

	void Start () {
        //调整camera
        Canvas canvas_ctrl = gameController.canvas.GetComponent<Canvas>();
        canvas_ctrl.worldCamera = Camera.main;
        //调整菜单位置
        gameController.panel_mainMenu_inscene.transform.position = new Vector3(0, 0, 0);        
	}

}
