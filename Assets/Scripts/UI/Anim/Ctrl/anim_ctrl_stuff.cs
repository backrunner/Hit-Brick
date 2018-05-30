using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_stuff : MonoBehaviour {

	void outAnimFinished()
    {
        Destroy(gameController.panel_stuff_inscene);
        gameController.isStuffPanelSpawned = false;
    }
}
