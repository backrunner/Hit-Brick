using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameUIContorller : MonoBehaviour {

    //用于代码之间调用的静态变量
    private static Text _text_leftBall;
    //用于编辑器指定的非静态变量
    public Text text_leftBall;
    //前缀
    public const string leftBall_prefix = "Left Ball: ";

    private void Start()
    {
        //初始化静态变量
        if (text_leftBall != null)
        {
            _text_leftBall = text_leftBall;
        } else
        {
            try
            {
                _text_leftBall = GameObject.Find("Text_leftBall").GetComponent<Text>();
                text_leftBall = _text_leftBall;
            }
            catch(Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    //更新leftBall UI的静态方法
    public static void updateLeftBallUI()
    {
        if (levelController.leftBall < 0)
        {
            _text_leftBall.text = leftBall_prefix + 0;
        } else
        {
            _text_leftBall.text = leftBall_prefix + levelController.leftBall;
        }
    } 
}
