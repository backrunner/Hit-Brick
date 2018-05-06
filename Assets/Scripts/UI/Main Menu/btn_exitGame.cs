using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_exitGame : MonoBehaviour {

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        //绑定事件
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        Debug.Log("Application Quit...");
        UnityEditor.EditorApplication.isPlaying = false; //Editor
        Application.Quit();
    }
}
