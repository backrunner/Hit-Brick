using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_stat : MonoBehaviour {

	public void animFinished()
    {
        Destroy(gameController.panel_stat_inscene);
        gameController.isStatPanelSpawned = false;
    }
}
