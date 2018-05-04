using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_submitName : MonoBehaviour {

    public GameObject _txt_warn;
    public GameObject _input_name;

    private InputField input_name;
    private Text txt_warn;

    private Button btn;

    private void Awake()
    {
        //初始化
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        //初始化
        _txt_warn = gameObject.transform.parent.Find("txt_warn").gameObject;
        _input_name = gameObject.transform.parent.Find("input_name").gameObject;
        //获得控件属性
        txt_warn = _txt_warn.GetComponent<Text>();
        input_name = _input_name.GetComponent<InputField>();

        //绑定事件
        btn.onClick.AddListener(onClick);
    }

    //点击触发
    private void onClick()
    {
        string name = input_name.text.Trim();
        if (name != "")
        {
            txt_warn.text = "";
            PlayerPrefs.SetString("player_name", name);
            gameObject.transform.parent.gameObject.AddComponent<anim_panel_inputName_close>();
            Messenger.AddListener("input name panel closed",displayMainMenu);
        } else
        {
            txt_warn.text = "请输入昵称";
        }
    }

    void displayMainMenu()
    {

    }
}
