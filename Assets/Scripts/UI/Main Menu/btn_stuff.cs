using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_stuff : btn_mainmenu {

    public override void clicked()
    {
        gameController.displayStuff();
        base.clicked();
    }
}
