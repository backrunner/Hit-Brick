using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_back : MonoBehaviour {

    public Button btn;

    public GameObject mainmenu;

    public virtual void Awake()
    {
        btn = GetComponent<Button>();
    }

    public virtual void Start () {
        btn.onClick.AddListener(onClick);
        //初始化
        if (gameController.panel_mainMenu_inscene != null)
        {
            mainmenu = gameController.panel_mainMenu_inscene;
        }
	}
	
	private void onClick()
    {
        Clicked();
    }

    public virtual void Clicked()
    {

    }
}
