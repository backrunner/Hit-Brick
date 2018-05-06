using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {

    //游戏初始化
    public static bool isInited;

    //玩家
    public static string player_name = "";

    //UI
    public static GameObject canvas;

    //UI预置
    public GameObject _panel_mainMenu;
    private static GameObject panel_mainMenu;

    //Level
    public string[] _levels;
    public static string[] levels;

    private void Awake()
    {
        //初始化静态变量
        if (_panel_mainMenu != null){
            panel_mainMenu = _panel_mainMenu;
        }
        levels = _levels;
        //Prefs
        isInited = PlayerPrefs.HasKey("player_name");        
        if (isInited)
        {
            //读入玩家信息
            player_name = PlayerPrefs.GetString("player_name"); //玩家名称
        }
    }

    void Start () {
        //初始化
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas");
        }
        //已初始化
        if (isInited)
        {
            //显示主菜单
            displayMainMenu();
        }
	}
	
	void Update () {
		
	}

    public static void setPlayerName(string name)
    {
        PlayerPrefs.SetString("player_name", name);
        player_name = name;
    }

    public static void displayMainMenu()
    {
        GameObject mainmenu = Instantiate(panel_mainMenu, canvas.transform);
        //播放动画
        GameObject logo = mainmenu.transform.Find("img_logo").gameObject;
        Animation anim = logo.GetComponent<Animation>();
        anim.Play("anim_logo");
    }
}
