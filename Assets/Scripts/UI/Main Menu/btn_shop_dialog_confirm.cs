using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_shop_dialog_confirm : MonoBehaviour {

    private Button btn;

    private GameObject panel;

    public int index;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        panel = transform.parent.parent.gameObject;
        Animation anim = panel.GetComponent<Animation>();
        anim_ctrl_shop_dialog ctrl = anim.GetComponent<anim_ctrl_shop_dialog>();    //控制器
        ctrl.index = index; //传递index
        ctrl.soldout = true;    //开关
        anim.Play("anim_panel_shop_dialog_out");
    }


}
