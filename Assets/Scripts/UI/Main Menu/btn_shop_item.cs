using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_shop_item : MonoBehaviour {

    private Button btn;

    public GameObject panel_shop_dialog;
    private GameObject dialogInScene;

    private GameObject canvas;

    public string itemName;
    public long itemPrice;

    private void Start()
    {
        canvas = gameController.canvas;
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        if (!gameController.isShopDialogSpawned)
        {
            dialogInScene = Instantiate(panel_shop_dialog, canvas.transform);
            gameController.isShopDialogSpawned = true;
            Animation anim = dialogInScene.GetComponent<Animation>();
            anim.Play("anim_panel_shop_dialog");
            Text txt = dialogInScene.transform.Find("dialog").Find("txt_item").gameObject.GetComponent<Text>();
            txt.text = itemName;
        }
    }
}
