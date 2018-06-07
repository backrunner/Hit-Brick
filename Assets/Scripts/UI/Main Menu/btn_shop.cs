using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_shop : MonoBehaviour {

    private Button btn;

    public GameObject panel_shop;
    private GameObject panel_shop_inscene;

    private GameObject canvas;

	void Start () {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
        //canvas init
        canvas = gameController.canvas;
	}

    void onClick()
    {
        if (!gameController.isShopPanelSpawned)
        {
            //instance
            panel_shop_inscene = Instantiate(panel_shop, canvas.transform);
            gameController.panel_shop_inscene = panel_shop_inscene;
            gameController.isShopPanelSpawned = true;
            //anim
            Animation anim = panel_shop_inscene.GetComponent<Animation>();
            anim.Play("anim_panel_shop");
            Animation anim_parent = gameController.panel_selectLevel_inscene.GetComponent<Animation>();
            anim_parent.Play("anim_panel_selectLevel_out_down");
            //text
            Text txt = panel_shop_inscene.transform.Find("txt_coin").gameObject.GetComponent<Text>();
            txt.text = playerController.coin.ToString();
        }
    }
}
