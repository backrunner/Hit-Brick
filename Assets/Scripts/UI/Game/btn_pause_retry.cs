using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_pause_retry : MonoBehaviour {

    private Button btn;
    //canvas
    private GameObject canvas;
    //panel
    public GameObject panel_loading;

	void Start () {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
        canvas = gameController.canvas;
	}
	
	public void onClick()
    {
        if (!gameController.isLoadingPanelSpawned)
        {
            Time.timeScale = 1;
            GameObject panel_in_scene = Instantiate(panel_loading, canvas.transform);
            anim_ctrl_loading ctrl = panel_in_scene.GetComponent<anim_ctrl_loading>();
            ctrl.filename = levelController.level_filename;
            gameController.isLoadingPanelSpawned = true;
        }
    }
}