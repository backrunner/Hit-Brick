using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour {

    //关卡名称
    public string level_name;
    //关卡开关
    public static bool isLevelStarted;
    public static bool isGameOver;
    public static bool isLevelPaused;

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
    //球列表
    public static ArrayList ballList;
    //球初始y轴偏移量
    public static float ballInitOffset;

    //砖块
    public static ArrayList bricks;

    //粒子
    //刷新球的特效
    public static GameObject particle_ray_launcher;
    //公共
    //非静态
    public GameObject m_particle_ray_launcher;

    //特效
    public GameObject clearEffect;
    public GameObject gameoverEffect;

    //UI
    //canvas
    public static GameObject canvas;
    //暂停图标
    public GameObject image_Pause;  //用于编辑器指定Prefabs
    private GameObject _image_Pause;    //用于记录代码生成的obj

    //道具
    public static GameObject[] propList;  //道具obj列表
    public GameObject[] _propList;  //编辑器内指定Prefabs
    public static float[] propRateList; //爆率表
    public float[] _propRateList; //编辑器内指定
    public static float totalRate; //总爆率
    public float _totalRate;
    public GameObject _anim_prop; //动画
    public static GameObject anim_prop;

    private void Awake()
    {        
        //传递assest ball给静态变量
        if (m_ball != null)
        {
            _ball = m_ball;
        }
        //传递编辑器的指定量给静态变量
        leftBall = m_leftBall;

        //初始化列表
        ballList = new ArrayList();
        bricks = new ArrayList();
        //道具列表赋给静态变量
        if (_propList != null)
        {
            propList = _propList;
        }
        if (_propRateList != null)
        {
            propRateList = _propRateList;
        }

        //初始化总爆率
        totalRate = _totalRate;

        //初始化动画
        anim_prop = _anim_prop;

        //获取环境中的预置Brick
        GameObject[] brickObjs = GameObject.FindGameObjectsWithTag("Bricks");
        for (int i = 0; i < brickObjs.Length; i++)
        {
            bricks.Add(brickObjs[i]);
        }

        //传递粒子给静态变量
        particle_ray_launcher=m_particle_ray_launcher;

        //寻找板子
        findPad();

        //变量初始化
        ballInitOffset = 0.3f;
    }

    private void Start()
    {        
        //新建一个球
        newBall();

        //更新UI
        gameUIContorller.updateLeftBallUI();
    }

    private void Update()
    {
        //检查暂停
        checkPause();
        //检查游戏状态
        if (isLevelStarted)
        {
            checkGameStatus();
        }
    }

    //检查暂停状态
    private void checkPause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!isLevelPaused)
            {
                //更改状态和时间缩放
                isLevelPaused = true;
                Time.timeScale = 0;
                //UI
                Vector3 image_pause_position = new Vector3(8f, 4.2f, 1f);
                _image_Pause = Instantiate(image_Pause, image_pause_position, new Quaternion(0, 0, 0, 0));
                //置于canvas下
                _image_Pause.transform.SetParent(canvas.transform);
                //修改scale
                _image_Pause.transform.localScale = new Vector3(1, 1, 1);
                Debug.Log("Game Paused");
            } else
            {
                //更改状态和时间缩放
                isLevelPaused = false;
                Time.timeScale = 1;
                //UI
                Destroy(_image_Pause);
                Debug.Log("Game Resumed");
            }
        }
    }

    //用于刷新新球的静态方法
    public static void newBall()
    {
        Vector3 position;
        if (_pad != null)
        {
            //获取位置并做偏移
            position = pad.transform.position;
            position.y += ballInitOffset;
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
        //把球加入到球列表
        ballList.Add(ball_new);
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
        if (bricks.Count <= 0)
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

    //添加一条命
    public static void addLeftBall()
    {
        leftBall++;
        //更新UI
        gameUIContorller.updateLeftBallUI();
    }
}
