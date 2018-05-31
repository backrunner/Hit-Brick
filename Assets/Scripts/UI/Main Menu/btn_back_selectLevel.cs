using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_back_selectLevel : btn_back {

    private GameObject panel;

    public override void Start()
    {
        base.Start();
        //初始化
        if (gameController.panel_selectLevel_inscene != null)
        {
            panel = gameController.panel_selectLevel_inscene;
        }
    }

    public override void Clicked()
    {
        if (!gameController.isStatPanelSpawned)
        {
            Animation anim_mainmenu = mainmenu.GetComponent<Animation>();
            anim_mainmenu.Play("anim_panel_mainmenu_in");
            Animation anim_panel = panel.GetComponent<Animation>();
            anim_panel.Play("anim_panel_selectLevel_out");
            base.Clicked();
        }
    }

}
