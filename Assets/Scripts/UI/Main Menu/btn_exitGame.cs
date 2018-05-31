using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_exitGame : btn_mainmenu {

    public override void clicked()
    {
        gameController.exitGame();
        base.clicked();
    }
}
