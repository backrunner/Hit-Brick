using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    //货币
    public static long coin;
    public static long targetcoin;

    void Awake()
    {
        coin = 0;
        //init

        if (Save.checkKey("coin"))
        {
            coin = Save.getData("coin");
            Debug.Log("get");
        }
        else
        {
            Save.setData("coin", 0);
            Debug.Log("reset");
        }

        //init target
        targetcoin = coin;
    }

    //保存数据
    public static void saveData()
    {
        Save.setData("coin", coin);
    }
}
