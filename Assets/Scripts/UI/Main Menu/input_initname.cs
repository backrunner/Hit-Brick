using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class input_initname : MonoBehaviour {

    private btn_submitName btn_submit;

    private void Start()
    {
        btn_submit = transform.parent.Find("btn_submitname").gameObject.GetComponent<btn_submitName>();
    }

    void Update () {
        //监听Submit输入
		if (Input.GetButtonDown("Submit"))
        {
            btn_submit.onClick();
        }
	}

}
