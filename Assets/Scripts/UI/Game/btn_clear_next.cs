using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_clear_next : MonoBehaviour {

    private Button btn;
    //canvas
    private GameObject canvas;
    //ui
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
            GameObject panel_in_scene = Instantiate(panel_loading, canvas.transform);
            //读取下一关卡
            anim_ctrl_loading ctrl = panel_in_scene.GetComponent<anim_ctrl_loading>();
            ctrl.levelIndex = gameController.currentLevelIndex + gameController.levelindexoffset + 1;
            gameController.isLoadingPanelSpawned = true;
            //刷写金钱数据
            playerController.coin = playerController.targetcoin;
            playerController.saveData();
        }
    }
}
