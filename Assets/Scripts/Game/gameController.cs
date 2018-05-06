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
    public static GameObject panel_mainMenu_inscene;
    //预置
    public GameObject _panel_selectLevel;
    private static GameObject panel_selectLevel;
    public static GameObject panel_selectLevel_inscene;

    //Level
    public string[] _levels;
    public static string[] levels;

    //开关
    public static bool isMainMenuSpawned = false;
    public static bool isSelectLevelSpawned = false;

    private void Awake()
    {
        //初始化静态变量
        panel_mainMenu = _panel_mainMenu;
        panel_selectLevel = _panel_selectLevel;
        levels = _levels;
        //Prefs
        isInited = PlayerPrefs.HasKey("player_name");        
        if (isInited)
        {
            //读入玩家信息
            player_name = PlayerPrefs.GetString("player_name"); //玩家名称
        }

        //初始化开关
        isMainMenuSpawned = false;
        isSelectLevelSpawned = false;
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

    public static void setPlayerName(string name)
    {
        PlayerPrefs.SetString("player_name", name);
        player_name = name;
    }

    public static void displayMainMenu()
    {
        if (!isMainMenuSpawned)
        {
            panel_mainMenu_inscene = Instantiate(panel_mainMenu, canvas.transform);
            //播放动画
            GameObject logo = panel_mainMenu_inscene.transform.Find("img_logo").gameObject;
            Animation anim = logo.GetComponent<Animation>();
            anim.Play("anim_logo");
            //设定开关
            isMainMenuSpawned = true;
        }
    }

    public static void displaySelectLevel() {
        if (!isSelectLevelSpawned)
        {
            panel_selectLevel_inscene = Instantiate(panel_selectLevel, canvas.transform);
            //播放动画
            Animation anim_selectlevel = panel_selectLevel_inscene.GetComponent<Animation>();
            anim_selectlevel.Play("anim_panel_selectLevel");
            Animation anim_mainmenu = panel_mainMenu_inscene.GetComponent<Animation>();
            anim_mainmenu.Play("anim_panel_mainmenu_out");
            //设定开关
            isSelectLevelSpawned = true;
        }
    }

    public static void exitGame()
    {
        Debug.Log("Application Quit...");
        UnityEditor.EditorApplication.isPlaying = false; //Editor
        Application.Quit();
    }

}
