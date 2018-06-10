﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_settings_dialog_cancel : MonoBehaviour {

    private GameObject panel;

    private Button btn;

	void Start () {
        panel = transform.parent.parent.gameObject;
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
	}
	
	public void onClick()
    {
        Animation anim = panel.GetComponent<Animation>();
        anim.Play("anim_panel_settings_dialog_out");
    }
}
