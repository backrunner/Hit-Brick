using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_settings : btn_mainmenu {

    public override void clicked()
    {
        gameController.displaySettings();
        base.clicked();
    }
}
