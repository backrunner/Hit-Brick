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
        hollowBrickCount = Save.getData("hollowBrickCount");
        normalBrickCount = Save.getData("normalBrickCount");
        hardBrickCount = Save.getData("hardBrickCount");
        propPickedCount = Save.getData("propPickedCount");
        deadBallCount = Save.getData("deadBallCount");
        pauseCount = Save.getData("pauseCount");
        levelplayCount = Save.getData("levelplayCount");
        gameoverCount = Save.getData("gameoverCount");
        clearCount = Save.getData("clearCount");
    }

    public static void refreshData()
    {
        hollowBrickCount = Save.getData("hollowBrickCount");
        normalBrickCount = Save.getData("normalBrickCount");
        hardBrickCount = Save.getData("hardBrickCount");
        propPickedCount = Save.getData("propPickedCount");
        deadBallCount = Save.getData("deadBallCount");
        pauseCount = Save.getData("pauseCount");
        levelplayCount = Save.getData("levelplayCount");
        gameoverCount = Save.getData("gameoverCount");
        clearCount = Save.getData("clearCount");
    }

    public static void saveData()
    {
        Save.setData("hollowBrickCount", hollowBrickCount);
        Save.setData("normalBrickCount", normalBrickCount);
        Save.setData("hardBrickCount", hardBrickCount);
        Save.setData("propPickedCount", propPickedCount);
        Save.setData("deadBallCount", deadBallCount);
        Save.setData("pauseCount", pauseCount);
        Save.setData("levelplayCount", levelplayCount);
        Save.setData("gameoverCount", gameoverCount);
        Save.setData("clearCount", clearCount);
    }
}
