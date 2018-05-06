using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_mainmenu : MonoBehaviour {

    private Button btn;

    //canvas
    public GameObject canvas;

    public virtual void Awake()
    {
        btn = GetComponent<Button>();
    }

    public virtual void Start()
    {
        if (levelController.canvas != null)
        {
            canvas = levelController.canvas;
        } else
        {
            canvas = GameObject.Find("Canvas");
        }

        //绑定监听事件        
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        clicked();
    }

    public virtual void clicked()
    {

    }
}
