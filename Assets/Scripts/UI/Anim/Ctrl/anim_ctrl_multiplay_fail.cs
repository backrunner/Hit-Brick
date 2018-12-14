using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_multiplay_fail : anim_ctrl_panel_fail {

    public string failure;

	public override void animFinished()
    {
        switch (failure) 
        {
            case "connect":
                Animation anim = gameController.panel_multiplay_inscene.GetComponent<Animation>();
                Animation anim_parent = gameController.panel_selectLevel_inscene.GetComponent<Animation>();
                anim.Play("anim_panel_multiplay_out");
                anim_parent.Play("anim_panel_selectLevel_in_up");
                gameController.multiplayController = null;
                base.animFinished();
                break;
            case "create_room":
                gameController.multiplayController.isCreateRoomFailed = false;
                base.animFinished();
                break;
            default:
                base.animFinished();
                break;
        } 
    }
}
