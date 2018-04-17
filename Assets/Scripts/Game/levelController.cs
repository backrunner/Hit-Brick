using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour {

    //关卡名称
    public string level_name;
    //关卡开关
    public static bool isLevelStarted;
    public static bool isGameOver;

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
    public static PadController pad_ctrl;

    //球
    //用于刷新的球obj
    private static GameObject _ball;
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
    //公共变量_obj
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
    //静态变量 当前球
    public static GameObject currentBall;

    //砖块
    public static int leftBricks;
    public static GameObject[] bricks;

    //粒子
    //刷新球的特效
    public static GameObject particle_ray_launcher;
    //公共
    //非静态
    public GameObject m_particle_ray_launcher;

    //特效
    public GameObject clearEffect;
    public GameObject gameoverEffect;

    private void Awake()
    {        
        //传递assest ball给静态变量
        if (m_ball != null)
        {
            _ball = m_ball;
        }
        //传递编辑器的指定量给静态变量
        leftBall = m_leftBall;

        //获取环境中的预置Brick
        bricks = GameObject.FindGameObjectsWithTag("Bricks");
        leftBricks = bricks.Length;

        //传递粒子给静态变量
        particle_ray_launcher=m_particle_ray_launcher;               
    }

    private void Start()
    {
        //寻找板子
        findPad();
        //新建一个球
        newBall();

        //更新UI
        gameUIContorller.updateLeftBallUI();
    }

    private void Update()
    {
        if (isLevelStarted)
        {
            checkGameStatus();
        }
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
        //初始球不刷新特效
        if (isLevelStarted)
        {
            Instantiate(particle_ray_launcher, position, new Quaternion(0, 0, 0, 0));
        }
        //刷新新球
        GameObject ball_new = Instantiate(_ball, position, new Quaternion(0, 0, 0, 0));
        currentBall = ball_new;
        //加入待发射列表
        if (pad_ctrl == null)
        {
            findPad();
        }
        pad_ctrl.addBallToLaunchList(ball_new);
        ballController ctrl = ball_new.GetComponent<ballController>();
        //让球附在板子上
        ctrl.isAttracted = true;
    }    

    //游戏状态检查
    void checkGameStatus()
    {
        if (leftBricks <= 0)
        {
            //标识游戏结束
            isLevelStarted = false;
            isGameOver = true;
            Instantiate(clearEffect, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            Debug.Log("Clear!");
        }
        if (leftBall < 0)
        {
            //标识游戏结束
            isLevelStarted = false;
            isGameOver = true;
            //触发特效
            Instantiate(gameoverEffect, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            Debug.Log("Game Over!");
        }
    }

    //寻找板子
    public static void findPad() {        
        if (pad == null)
        {
            pad = GameObject.Find("Pad");
            pad_ctrl = pad.GetComponent<PadController>();
        }
    }
}
