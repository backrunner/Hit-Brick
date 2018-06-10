using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_settings_dialog : MonoBehaviour {

    public bool isClear = false;

    private void Awake()
    {
        isClear = false;
    }

    public void animFinished()
    {
        gameController.isSettingsDialogSpawned = false;
        Destroy(gameObject);
        if (isClear)
        {
            Save.purge();   //清空存档
        }
    }
}

