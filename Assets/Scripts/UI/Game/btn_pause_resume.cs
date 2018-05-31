using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_pause_resume : MonoBehaviour {

    private Button btn;

    private GameObject panel;

	void Start () {
        //初始化
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
        //获取panel
        if (levelController.panel_pause_inscene != null)
        {
            panel = levelController.panel_pause_inscene;
        } else
        {
            panel = GameObject.Find("Panel_pause(Clone)");
        }
	}
	
	public void onClick()
    {
        //UI
        Animation anim = panel.GetComponent<Animation>();
        anim["Anim_pause"].speed = -2;
        anim.Play("Anim_pause");
        //设置倒放
        anim_unscaledTime ctrl = panel.GetComponent<anim_unscaledTime>();
        ctrl.progress = 0.85f;
        ctrl.isReverse = true;
        Debug.Log("Game Resumed");
    }
}
