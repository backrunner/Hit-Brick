using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class versionTextController : MonoBehaviour {

    //定义变量
    public GameObject _txt_version;
    private Text txt_version;

    void Start () {

        //初始化
        _txt_version = transform.Find("txt_version").gameObject;
        if (_txt_version != null)
        {
            txt_version = _txt_version.GetComponent<Text>();
            //清空文字
            txt_version.text = "";
        }

        if (txt_version != null)
        {
            //初始化左上角标识
            if (gameController.player_name.Length > 0)
            {
                txt_version.text += gameController.player_name;
                if (buildController.version.Length > 0)
                {
                    txt_version.text += "\nVer. " + buildController.version;
                }
                if (buildController.build.Length > 0)
                {
                    txt_version.text += " Build " + buildController.build;
                }
            }
            else
            {
                if (buildController.version.Length > 0)
                {
                    txt_version.text += buildController.version;
                }
                if (buildController.build.Length > 0)
                {
                    txt_version.text += " Build " + buildController.build;
                }
            }
        }
        
	}

}
