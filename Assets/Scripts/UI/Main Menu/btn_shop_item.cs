using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_shop_item : MonoBehaviour {

    private Button btn;

    public GameObject panel_shop_dialog;
    private GameObject dialogInScene;

    private GameObject canvas;

    public int index;   //商品编号

    private void Start()
    {
        //init
        canvas = gameController.canvas;
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        ShopItem item = (ShopItem)shopController.shopItems[index];  //get desc
        if (!gameController.isShopDialogSpawned && !item.soldout)
        {
            dialogInScene = Instantiate(panel_shop_dialog, canvas.transform);
            gameController.isShopDialogSpawned = true;
            //anim
            Animation anim = dialogInScene.GetComponent<Animation>();
            anim.Play("anim_panel_shop_dialog");
            //ui
            Text txt = dialogInScene.transform.Find("dialog").Find("txt_item").gameObject.GetComponent<Text>();            
            txt.text = item.desc;
            GameObject btn_confirm = dialogInScene.transform.Find("dialog").Find("btn_confirm").gameObject;
            btn_shop_dialog_confirm ctrl = btn_confirm.GetComponent<btn_shop_dialog_confirm>();
            ctrl.index = index; //传递index给确认按钮
        }
    }
}
