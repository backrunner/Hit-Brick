using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour {

    //关卡名称
    public string level_name;
    //关卡开关
    public static bool isLevelStarted;

    //板子
    private static GameObject _pad;
    //公共变量
    public static GameObject pad
    {
        get
        {
            return _pad;
        }
        set
        {
            _pad = value;
        }
    }
    
    //球
    //用于刷新的球obj
    public static GameObject _ball;
    //剩余球
    private static int _leftBall;
    //非静态量，用于编辑器中指定
    public int m_leftBall;
    //公共变量
    public static int leftBall
    {
        get
        {
            return _leftBall;
        }
        set
        {
            _leftBall = value;
        }
    }
    //非静态ball，用于编辑器中指定obj
    public GameObject m_ball;
    //公共变量
    public GameObject ball
    {
        get
        {
            return _ball;
        }
        set
        {
            _ball = value;
        }
    }

    private void Start()
    {
        //关卡开始时寻找板子
        if (pad == null)
        {
            pad = GameObject.Find("Pad");
        }
        //传递ball给静态变量
        if (m_ball != null)
        {
            _ball = m_ball;
        }
        //新建一个球
        newBall();
    }

    public static void newBall()
    {
        Vector3 position;
        if (_pad != null)
        {
            //获取位置并做偏移
            position = pad.transform.position;
            position.y += 0.25f;
        } else
        {
            //抓取不到pad，使用默认位置
            position = new Vector3(0, -4.15f, 0);
        }
        //刷新球
        GameObject ball_new = Instantiate(_ball, position, new Quaternion(0, 0, 0, 0));
        ballController ctrl = ball_new.GetComponent<ballController>();
        //让球附在板子上
        ctrl.isAttracted = true;
    }
}
