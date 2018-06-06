using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_settings : MonoBehaviour {

    void animFinished()
    {
        Destroy(gameController.panel_settings_inscene);
        gameController.isSettingsPanelSpawned = false;
    }
}
