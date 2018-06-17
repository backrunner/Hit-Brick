using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{

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
    public GameObject _panel_stuff;
    private static GameObject panel_stuff;
    public static GameObject panel_stuff_inscene;
    public GameObject _panel_settings;
    private static GameObject panel_settings;
    public static GameObject panel_settings_inscene;
    public static GameObject panel_shop_inscene;

    public GameObject _img_bgblock_group;
    public static GameObject img_bgblock_group;
    public float bgblock_opacity;

    public static ArrayList bgblockList;

    //关卡
    public string[] _levels;
    public static string[] levels;
    public string[] _levels_filename;
    public static string[] levels_filename;
    public string[] _levels_unlock;
    public static string[] levels_unlock;

    //关卡索引
    public static int currentLevelIndex;
    public static int levelindexoffset = 3; //关卡序号偏移量

    //开关
    public static bool isMainMenuSpawned = false;
    public static bool isSelectLevelSpawned = false;
    public static bool isLoadingPanelSpawned = false;
    public static bool isStatPanelSpawned = false;
    public static bool isStuffPanelSpawned = false;
    public static bool isSettingsPanelSpawned = false;
    public static bool isShopPanelSpawned = false;

    public static bool isShopDialogSpawned = false;
    public static bool isSettingsDialogSpawned = false;

    private void Awake()
    {
        //静态变量_panel
        panel_mainMenu = _panel_mainMenu;
        panel_selectLevel = _panel_selectLevel;
        panel_stuff = _panel_stuff;
        panel_settings = _panel_settings;

        img_bgblock_group = _img_bgblock_group;

        //静态变量
        btn_level = _btn_level;
        eventSystem = _eventSystem;
        thisgameObj = gameObject;

        //初始化关卡列表
        levels = _levels;
        levels_filename = _levels_filename;
        levels_unlock = _levels_unlock;

        bgblockList = new ArrayList();

        //init currentLevelIndex
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        //bgblock
        bgblock_opacity = 0;

        //Prefs
        isInited = Save.checkKey("player_name");
        if (isInited)
        {
            //读入玩家信息
            player_name = Save.getString("player_name"); //玩家名称
        }

        //初始化开关
        isMainMenuSpawned = false;
        isSelectLevelSpawned = false;
        isLoadingPanelSpawned = false;
        isStatPanelSpawned = false;
        isStuffPanelSpawned = false;
        isSettingsPanelSpawned = false;
        isShopPanelSpawned = false;

        isShopDialogSpawned = false;
        isSettingsDialogSpawned = false;

        //保留的游戏物件
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(eventSystem);
    }

    void Start()
    {
        //初始化
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas");
        }
        //调整camera
        Canvas canvas_ctrl = canvas.GetComponent<Canvas>();
        canvas_ctrl.worldCamera = Camera.main;
        //已初始化
        if (isInited)
        {
            //显示背景
            displayBg();
            //显示主菜单
            displayMainMenu();
        }
    }

    public static void displayBg()
    {
        GameObject bg = Instantiate(img_bgblock_group, canvas.transform);
        bgblockList.Add(bg);
        //Debug.Log(currentLevelIndex);
        bg.transform.position = new Vector3(0, 0, 0);
        bg.transform.SetSiblingIndex(0);
        Animation anim = thisgameObj.GetComponent<Animation>();
        anim.Play("anim_bgblock_opacity");
        //Debug.Log("ok");
    }

    public static void clearBg()
    {
        foreach (GameObject obj in bgblockList)
        {
            Destroy(obj);
        }
    }

    public static void setPlayerName(string name)
    {
        Save.setData("player_name", name);
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

    public static void displaySelectLevel()
    {
        if (!isSelectLevelSpawned && !isStuffPanelSpawned && !isSettingsPanelSpawned)
        {
            panel_selectLevel_inscene = Instantiate(panel_selectLevel, canvas.transform);
            //添加按钮
            GameObject content = panel_selectLevel_inscene.transform.Find("scroll_levels").Find("Viewport").Find("content_btns").gameObject;
            string clearedlevel = "";
            if (Save.checkKey("clearedLevel"))
            {
                clearedlevel = Save.getString("clearedLevel");
            }
            for (int i = 0; i < levels.Length; i++)
            {
                GameObject btn = Instantiate(btn_level, content.transform);
                GameObject text_obj = btn.transform.Find("txt_btn_level").gameObject;
                btn_level ctrl = btn.GetComponent<btn_level>();
                Text text = text_obj.GetComponent<Text>();
                text.text = levels[i];

                //检查unlock
                if (levels_unlock[i] != null&&levels_unlock[i] != "")
                {
                    if (!clearedlevel.Contains(levels_unlock[i]))
                    {
                        GameObject img_lock = btn.transform.Find("img_lock").gameObject;
                        Image img = img_lock.GetComponent<Image>();
                        Color t = img.color;
                        t.a = 1;
                        img.color = t;
                        ctrl.enabled = false;
                    }
                }

                //设置clear图片
                if (clearedlevel.Contains(levels_filename[i]))
                {
                    GameObject img_clear = btn.transform.Find("img_clear").gameObject;
                    Image img = img_clear.GetComponent<Image>();
                    Color t = img.color;
                    t.a = 1;
                    img.color = t;
                }
                
                ctrl.filename = levels_filename[i];
                ctrl.index = i; //设置序号
                //coin
                Text txt_coin = panel_selectLevel_inscene.transform.Find("txt_coin").gameObject.GetComponent<Text>();
                playerController.txt_selectLevel_coin_inscene = txt_coin;
                txt_coin.text = playerController.coin.ToString();
            }
            //设置content高度
            RectTransform rect = content.GetComponent<RectTransform>();
            if (levels.Length > 5)
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, levels.Length * 148 - 48);
            }
            else
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

    public static void displayStuff()
    {
        if (!isSelectLevelSpawned && !isStuffPanelSpawned && !isSettingsPanelSpawned)
        {
            //ui
            panel_stuff_inscene = Instantiate(panel_stuff, canvas.transform);
            isStuffPanelSpawned = true;
            //anim
            Animation anim = panel_stuff_inscene.GetComponent<Animation>();
            anim.Play("anim_panel_stuff");
            Animation anim_mainmenu = panel_mainMenu_inscene.GetComponent<Animation>();
            anim_mainmenu.Play("anim_panel_mainmenu_out");
        }
    }

    public static void displaySettings()
    {
        if (!isSelectLevelSpawned && !isStuffPanelSpawned && !isSettingsPanelSpawned)
        {
            //ui
            panel_settings_inscene = Instantiate(panel_settings, canvas.transform);
            isSettingsPanelSpawned = true;
            Text txt_player = panel_settings_inscene.transform.Find("txt_player").GetComponent<Text>();
            txt_player.text = "当前玩家： " + player_name;
            //anim
            Animation anim = panel_settings_inscene.GetComponent<Animation>();
            anim.Play("anim_panel_settings");
            Animation anim_mainmenu = panel_mainMenu_inscene.GetComponent<Animation>();
            anim_mainmenu.Play("anim_panel_mainmenu_out");
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
