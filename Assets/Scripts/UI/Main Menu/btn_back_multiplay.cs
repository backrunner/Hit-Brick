using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_back_multiplay : MonoBehaviour {

    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    void onClick()
    {
        Animation anim = gameController.panel_multiplay_inscene.GetComponent<Animation>();
        Animation anim_parent = gameController.panel_selectLevel_inscene.GetComponent<Animation>();
        anim.Play("anim_panel_multiplay_out");
        anim_parent.Play("anim_panel_selectLevel_in_up");
    }

}
