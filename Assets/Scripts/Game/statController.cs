using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statController : MonoBehaviour {

    //brick
    public static long hollowBrickCount = 0;    //空心砖块打碎数
    public static long normalBrickCount = 0;    //普通砖块打碎数
    public static long hardBrickCount = 0;  //硬砖块打碎数
    //props
    public static long propPickedCount = 0; //拾取了的道具
    //game
    public static long deadBallCount = 0;   //死球数
    public static long pauseCount = 0;  //暂停次数
    //level
    public static long levelplayCount = 0;  //关卡游玩次数
    public static long gameoverCount = 0; //gameover次数
    public static long clearCount = 0;  //clear次数

    private void Awake()
    {
        initStatData();
    }

    void initStatData()
    {
        hollowBrickCount = getData("hollowBrickCount");
        normalBrickCount = getData("normalBrickCount");
        hardBrickCount = getData("hardBrickCount");
        propPickedCount = getData("propPickedCount");
        deadBallCount = getData("deadBallCount");
        pauseCount = getData("pauseCount");
        levelplayCount = getData("levelplayCount");
        gameoverCount = getData("gameoverCount");
        clearCount = getData("clearCount");
    }

    public static void refreshData()
    {
        hollowBrickCount = getData("hollowBrickCount");
        normalBrickCount = getData("normalBrickCount");
        hardBrickCount = getData("hardBrickCount");
        propPickedCount = getData("propPickedCount");
        deadBallCount = getData("deadBallCount");
        pauseCount = getData("pauseCount");
        levelplayCount = getData("levelplayCount");
        gameoverCount = getData("gameoverCount");
        clearCount = getData("clearCount");
    }

    public static void saveData()
    {
        setData("hollowBrickCount", hollowBrickCount);
        setData("normalBrickCount", normalBrickCount);
        setData("hardBrickCount", hardBrickCount);
        setData("propPickedCount", propPickedCount);
        setData("deadBallCount", deadBallCount);
        setData("pauseCount", pauseCount);
        setData("levelplayCount", levelplayCount);
        setData("gameoverCount", gameoverCount);
        setData("clearCount", clearCount);
    }

    private static void setData(string key,long data)
    {
        PlayerPrefs.SetString(key, data.ToString().Trim());
    }

    private static long getData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string data =  PlayerPrefs.GetString(key);
            return long.Parse(data);
        } else
        {
            return 0;
        }
    }
}
