using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //货币
    public static long coin = 0;
    public static long targetcoin;

	void Awake () {
        //init
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
        //init target
        targetcoin = coin;
	}

    //保存数据
    public static void saveData()
    {
        Save.setData("coin", coin);
        Debug.Log("coin data saved");
    }
}
