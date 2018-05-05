using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_submitName : MonoBehaviour {

    //预置
    public GameObject mainMenu;

    //UI Obj
    public GameObject _txt_warn;
    public GameObject _input_name;

    //UI元件
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
    public void onClick()
    {
        string name = input_name.text.Trim();
        if (name != "")
        {
            txt_warn.text = "";
            gameController.setPlayerName(name);
            //播放动画
            Animation anim = gameObject.transform.parent.gameObject.GetComponent<Animation>();
            anim.Play("anim_initname_close");
        } else
        {
            txt_warn.text = "请输入昵称";
        }
    }
}
