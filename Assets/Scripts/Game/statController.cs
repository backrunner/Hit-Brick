using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statController : MonoBehaviour {

    //brick
    public static long hollowBrickCount;    //空心砖块打碎数
    public static long normalBrickCount;    //普通砖块打碎数
    public static long hardBrickCount;  //硬砖块打碎数
    //props
    public static long propCount;   //总掉落道具
    public static long propPickedCount; //拾取了的道具
    //game
    public static long deadBallCount;   //死球数
    public static long pauseCount;  //暂停次数
    //level
    public static long levelplayCount;  //关卡游玩次数
    public static long gameoverCount; //gameover次数
    public static long clearCount;  //clear次数

    private void Awake()
    {
        
    }

    void initStatData()
    {

    }
}
