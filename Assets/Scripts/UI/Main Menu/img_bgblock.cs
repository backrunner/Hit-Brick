using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class img_bgblock : MonoBehaviour {

    GameObject gameCtrl_obj;
    gameController gameCtrl;

    Color currentColor;

    Image thisImg;

	void Start () {
        //init
        gameCtrl_obj = gameController.thisgameObj;
        gameCtrl = gameCtrl_obj.GetComponent<gameController>();

        thisImg = GetComponent<Image>();

        currentColor = thisImg.color;
	}
	
	void Update () {
        currentColor.a = gameCtrl.bgblock_opacity;
        thisImg.color = currentColor;
	}
}
