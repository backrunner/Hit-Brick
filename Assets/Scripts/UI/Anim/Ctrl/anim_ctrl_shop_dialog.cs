using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_shop_dialog : MonoBehaviour {

    public int index;
    public bool soldout;

    private GameObject canvas;

    public GameObject panel_fail;

    private void Awake()
    {
        soldout = false;
    }

    private void Start()
    {
        canvas = gameController.canvas;
    }

    public void animFinished()
    {
        gameController.isShopDialogSpawned = false;
        if (soldout)
        {
            ShopItem item = (ShopItem)shopController.shopItems[index];
            if (item.price <= playerController.coin)
            {
                item.sold();
                GameObject panel_shop = canvas.transform.Find("Panel_shop(Clone)").gameObject;
                GameObject txt_coin = panel_shop.transform.Find("txt_coin").gameObject;
                anim_coinreward ctrl = txt_coin.GetComponent<anim_coinreward>();
                ctrl.forwardNumber(playerController.coin - item.price);
                //扣除用户coin
                playerController.coin -= item.price;
                playerController.saveData();
                //保存商品状态
                shopController.saveShopItemStatus();
            } else
            {
                levelController.panel_fail_inscene = Instantiate(panel_fail, canvas.transform);
                Animation anim = levelController.panel_fail_inscene.GetComponent<Animation>();
                anim.Play();
            }
        }
        Destroy(gameObject);
    }
}
