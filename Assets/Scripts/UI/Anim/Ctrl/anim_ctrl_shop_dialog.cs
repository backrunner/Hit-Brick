using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_shop_dialog : MonoBehaviour {

    public int index;
    public bool soldout;

    private void Awake()
    {
        soldout = false;
    }

    public void animFinished()
    {
        gameController.isShopDialogSpawned = false;
        if (soldout)
        {
            ShopItem item = (ShopItem)shopController.shopItems[index];
            item.sold();
        }
        Destroy(gameObject);
    }
}
