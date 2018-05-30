using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

    public static GameObject thisgameObj;

    //游戏初始化
    public static bool isInited;

    //玩家
    public static string player_name = "";

    //UI
    public static GameObject canvas;
    public static GameObject eventSystem;
    public GameObject _eventSystem;
    //UI预置
    public GameObject _panel_mainMenu;
    private static GameObject panel_mainMenu;
    public static GameObject panel_mainMenu_inscene;
    //预置
    public GameObject _panel_selectLevel;
    private static GameObject panel_selectLevel;
    public static GameObject panel_selectLevel_inscene;
    public GameObject _btn_level;
    private static GameObject btn_level;
    public static GameObject panel_stat_inscene;

    //关卡
    public string[] _levels;
    public static string[] levels;
    public string[] _levels_filename;
    public static string[] levels_filename;

    //关卡索引
    public static int currentLevelIndex;
    public static int levelindexoffset = 3; //关卡序号偏移量

    //开关
    public static bool isMainMenuSpawned = false;
    public static bool isSelectLevelSpawned = false;
    public static bool isLoadingPanelSpawned = false;
    public static bool isStatPanelSpawned = false;


    private void Awake()
    {
        //初始化静态变量
        panel_mainMenu = _panel_mainMenu;
        panel_selectLevel = _panel_selectLevel;
        btn_level = _btn_level;
        eventSystem = _eventSystem;
        thisgameObj = gameObject;
        //初始化关卡列表
        levels = _levels;
        levels_filename = _levels_filename;
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
        isLoadingPanelSpawned = false;

        //保留的游戏物件
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(eventSystem);
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
            //添加按钮
            GameObject content = panel_selectLevel_inscene.transform.Find("scroll_levels").Find("Viewport").Find("content_btns").gameObject;
            for (int i = 0; i < levels.Length; i++)
            {
                GameObject btn = Instantiate(btn_level, content.transform);
                GameObject text_obj = btn.transform.Find("txt_btn_level").gameObject;
                Text text = text_obj.GetComponent<Text>();
                text.text = levels[i];
                //设置clear图片
                if (PlayerPrefs.HasKey("clearedLevel"))
                {
                    string clearedlevel = PlayerPrefs.GetString("clearedLevel");
                    if (clearedlevel.Contains(levels_filename[i]))
                    {
                        GameObject img_clear = btn.transform.Find("img_clear").gameObject;
                        Image img = img_clear.GetComponent<Image>();
                        Color t = img.color;
                        t.a = 1;
                        img.color = t;
                    }
                }
                btn_level ctrl = btn.GetComponent<btn_level>();
                ctrl.filename = levels_filename[i];
                ctrl.index = i; //设置序号
            }
            //设置content高度
            RectTransform rect = content.GetComponent<RectTransform>();
            if (levels.Length > 5)
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, levels.Length * 148 - 48);
            } else
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, levels.Length * 148);
            }
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
        //UnityEditor.EditorApplication.isPlaying = false; //Editor
        Application.Quit();
    }

    //刷新canvas
    public static void refreshCanvas()
    {
        canvas = GameObject.Find("canvas");
    }

}
