using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_settings_dialog_confirm : MonoBehaviour {

    private GameObject panel;

    private Button btn;

    void Start()
    {
        panel = transform.parent.parent.gameObject;
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        Animation anim = panel.GetComponent<Animation>();
        anim.Play("anim_panel_settings_dialog_out");
        anim_ctrl_settings_dialog ctrl = panel.GetComponent<anim_ctrl_settings_dialog>();
        ctrl.isClear = true;
    }
}
