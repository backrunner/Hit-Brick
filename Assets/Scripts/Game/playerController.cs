using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    //货币
    public static long coin;
    public static long targetcoin;

    public static Text txt_selectLevel_coin_inscene;

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
        refreshText();
    }

    public static void refreshText()
    {
        if (txt_selectLevel_coin_inscene != null)
        {
            txt_selectLevel_coin_inscene.text = coin.ToString();
        }
    }
}
