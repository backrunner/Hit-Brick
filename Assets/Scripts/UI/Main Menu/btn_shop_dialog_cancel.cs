using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_shop_dialog_cancel : MonoBehaviour {

    private Button btn;

    private GameObject panel;

	void Start () {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
	}
	
	public void onClick()
    {
        panel = transform.parent.parent.gameObject;
        Animation anim = panel.GetComponent<Animation>();
        anim.Play("anim_panel_shop_dialog_out");
    }
}
