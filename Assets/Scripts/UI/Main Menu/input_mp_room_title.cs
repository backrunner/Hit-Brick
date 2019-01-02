using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class input_mp_room_title : MonoBehaviour {

    InputField input;

    string nowName = "";

    bool isFocus = false;

	void Start () {
        input = this.GetComponent<InputField>();
        input.onValueChanged.AddListener(OnValueChanged);
	}

    void Update()
    {
        //监听Submit输入
        if (Input.GetButtonDown("Submit") && isFocus)
        {
            SubmitNameChange();
        }
    }

    private void OnValueChanged(string value)
    {
        nowName = value;
        isFocus = true;
    }

    private void SubmitNameChange()
    {
        isFocus = false;
        gameController.multiplayController.RoomNameChanged(nowName);
    }
}
