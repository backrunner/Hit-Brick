using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_startgame : btn_mainmenu {      

    public override void clicked()
    {
        gameController.displaySelectLevel();
        base.clicked();
    }

}
