using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_clearsave : MonoBehaviour {

    private Button btn;

    private GameObject canvas;

    public GameObject panel_settings_dialog;
    private GameObject panel_settings_dialog_inscene;

	void Start () {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
        canvas = gameController.canvas;
	}
	
    public void onClick()
    {
        if (!gameController.isSettingsDialogSpawned)
        {
            panel_settings_dialog_inscene = Instantiate(panel_settings_dialog, canvas.transform);
            Animation anim = panel_settings_dialog_inscene.GetComponent<Animation>();
            anim.Play("anim_panel_settings_dialog");
            gameController.isSettingsDialogSpawned = true;
        }
    }
	
}
