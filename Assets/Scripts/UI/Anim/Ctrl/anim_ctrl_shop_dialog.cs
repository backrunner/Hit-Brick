using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_shop_dialog : MonoBehaviour {

	public void animFinished()
    {
        gameController.isShopDialogSpawned = false;
        Destroy(gameObject);
    }
}
