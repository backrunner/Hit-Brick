using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_mp_room : MonoBehaviour {

    public void animFinished()
    {
        Destroy(gameController.panel_multiplay_room_inscene);
        gameController.isMultiPlayRoomSpawned = false;
    }
}
