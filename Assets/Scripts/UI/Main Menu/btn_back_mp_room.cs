using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_back_mp_room : MonoBehaviour {

    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    void onClick()
    {
        Animation anim = gameController.panel_multiplay_room_inscene.GetComponent<Animation>();
        Animation anim_parent = gameController.panel_multiplay_inscene.GetComponent<Animation>();
        anim.Play("anim_panel_multiplay_room_out");
        anim_parent.Play("anim_panel_multiplay_in_right");
    }

}
