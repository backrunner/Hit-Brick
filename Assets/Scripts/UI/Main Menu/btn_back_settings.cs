using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_back_settings : btn_back {

    private GameObject panel;

    public override void Start()
    {
        base.Start();
        //初始化
        if (gameController.panel_settings_inscene != null)
        {
            panel = gameController.panel_settings_inscene;
        }
    }

    public override void Clicked()
    {
        Animation anim_mainmenu = mainmenu.GetComponent<Animation>();
        anim_mainmenu.Play("anim_panel_mainmenu_in");
        Animation anim_panel = panel.GetComponent<Animation>();
        anim_panel.Play("anim_panel_settings_out");
        base.Clicked();
    }

}
