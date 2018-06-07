using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_shop : MonoBehaviour {

    public void animFinished()
    {
        Destroy(gameController.panel_shop_inscene);
        gameController.isShopPanelSpawned = false;
    }

}
