using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //货币
    public static long coin = 0;

	void Awake () {
		if (!gameController.isInited)
        {            
            if (Save.checkKey("coin"))
            {
                coin = Save.getData("coin");
            } else
            {
                Save.setData("coin", 0);
            }
        } else
        {
            Save.setData("coin", 0);
        }
	}

}
