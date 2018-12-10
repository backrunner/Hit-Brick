using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_multiplay : MonoBehaviour {

    public void animFinished()
    {
        Destroy(gameController.panel_multiplay_inscene);
        gameController.isMultiPlaySpawned = false;        
    }

}
