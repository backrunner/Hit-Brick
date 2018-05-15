using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_level : MonoBehaviour {

    private Button btn;

    //UI
    public GameObject panel_loading;
    private GameObject canvas;
    //关卡名称
    public string filename;

    void Start() {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
        //canvas
        if (gameController.canvas != null)
        {
            canvas = gameController.canvas;
        } else
        {
            canvas = GameObject.Find("Canvas");
        }
	}
	
	public void onClick()
    {
        GameObject panel_in_scene = Instantiate(panel_loading,canvas.transform);
        anim_ctrl_loading ctrl = panel_in_scene.GetComponent<anim_ctrl_loading>();
        ctrl.filename = filename;
    }
}
