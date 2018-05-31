using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{

    public bool isAttracted;

    //GameObjects
    public GameObject pad;
    //rigidbody
    public Rigidbody2D rigid;

    //初始球速
    public float initMoveSpeed;

    //current speed scale
    public float speedScale;
    //上下限
    public float minSpeedScale;
    public float maxSpeedScale;

    //是否开启powerful状态
    public bool isPowerful;

    //位置卡死检测的时间线
    //卡住判定时间
    public float time_check_deadline;
    //卡死判定阀值
    public float check_limit;

    //位置记录和持续时间
    private Vector3 recordedPosition;
    private float position_deltaTime;

    //记录位置是否卡住
    private bool isStack_x;
    private bool isStack_y;

    //目标缩放
    public float targetScale;
    //缩放相关
    public float originScale; //原始大小
    public float minScale; //下限
    public float maxScale; //上限
    public float deltaScale;
    public float deltaScale_reverse;

    //anim
    public GameObject anim_ballSize;

    //吸附板的偏移量
    public float attractedOffset;

    //启用Powerful的子物体
    public GameObject powerful_child;

    private void Awake()
    {
        //初始化attractedOffset
        attractedOffset = levelController.ballInitOffset;
        //初始化originScale
        originScale = transform.localScale.x;
        targetScale = originScale;
        //初始化minScale deltaScale targetScale
        if (minScale == 0)
        {
            minScale = originScale * (maxScale - originScale) / originScale;
        }
        deltaScale = (maxScale - originScale) / 2;
        deltaScale_reverse = (originScale - minScale) / 2;

        //初始化变量
        isPowerful = false;
        speedScale = 1;

        //init rigidbody
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //游戏未开始ball附在pad上
        if (!levelController.isLevelStarted)
        {
            isAttracted = true;
        }
        //如果pad没有指定则获取levelController的公共变量pad或查找
        if (pad == null)
        {
            if (levelController.pad != null)
            {
                pad = levelController.pad;
            }
            else
            {
                pad = GameObject.Find("Pad");
                levelController.pad = pad;
            }
        }
        //初始化变量
        recordedPosition = transform.position;
        position_deltaTime = 0;
        isStack_x = false;
        isStack_y = false;
    }

    void Update()
    {
        Debug.Log(rigid.velocity);
        if (isAttracted)
        {
            attachToPad();
        }
        else
        {
            if (levelController.isLevelStarted && !levelController.isLevelPaused && !levelController.isGameOver)
            {
                //如果球未吸附在pad上，则检测球的位置是否出现卡死
                checkPosition();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pad")
        {
            tweakMovement();
        }
    }

    //运动调整
    void tweakMovement()
    {
        if (Mathf.Abs(rigid.velocity.x) <= 0.15f)
        {
            if (transform.position.x >= pad.transform.position.x)
            {
                rigid.velocity = new Vector2(rigid.velocity.y / Mathf.Sqrt(2), rigid.velocity.y / Mathf.Sqrt(2));
            }
            else
            {
                rigid.velocity = new Vector2(-rigid.velocity.y / Mathf.Sqrt(2), rigid.velocity.y / Mathf.Sqrt(2));
            }
        }
    }

    //绑定ball与pad的运动
    void attachToPad()
    {
        Vector3 position = pad.transform.position;
        position.y += attractedOffset;
        this.gameObject.transform.position = position;
    }

    public void launchBall()
    {
        if (isAttracted)
        {
            //关卡开始
            if (!levelController.isLevelStarted)
            {
                levelController.isLevelStarted = true;
            }
            //取消附着状态
            this.isAttracted = false;
            //向ball施加自定义力
            rigid.AddRelativeForce(new Vector3(0, initMoveSpeed, 0));
            //开启ball的trail render
            StartCoroutine(delayToEnableTrailRender(0.1f));
        }
    }

    //位置检查
    void checkPosition()
    {
        //球没有卡在y轴
        if (!isStack_y)
        {
            if (Mathf.Abs(recordedPosition.x - transform.position.x) <= check_limit)
            {
                isStack_x = true;
                position_deltaTime += Time.deltaTime;
                //当卡住的时间超过限定时间
                if (position_deltaTime > time_check_deadline)
                {
                    Debug.Log("Ball Stacked");
                    //分左右两侧给球施加一个反向外力解决卡位
                    if (transform.position.x <= 0)
                    {
                        Vector2 force = new Vector2(20f, 0f);
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                        updateRecordPosition();
                    }
                    else
                    {
                        Vector2 force = new Vector2(-20f, 0);
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                        updateRecordPosition();
                    }
                }
            }
            else
            {
                if (isStack_x)
                {
                    position_deltaTime = 0;
                }
                isStack_x = false;
            }
        }

        //球没有卡在x轴
        if (!isStack_x)
        {
            if (Mathf.Abs(recordedPosition.y - transform.position.y) <= check_limit || rigid.velocity.y==0)
            {
                isStack_y = true;
                position_deltaTime += Time.deltaTime;
                //当卡住的时间超过限定时间
                if (position_deltaTime > time_check_deadline)
                {
                    Debug.Log("Ball Stacked");
                    //分上下两侧给球施加一个反向外力解决卡位
                    if (transform.position.y <= 0)
                    {
                        Vector2 force = new Vector2(0, 20f);
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                        updateRecordPosition();
                    }
                    else
                    {
                        Vector2 force = new Vector2(0, -20f);
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                        updateRecordPosition();
                    }
                }
            }
            else
            {
                if (isStack_y)
                {
                    position_deltaTime = 0;
                }
                isStack_y = false;
            }
        }
        //记录新的位置
        if (!isStack_x && !isStack_y)
        {
            recordedPosition = transform.position;
        }
    }

    //更新记录的位置
    public void updateRecordPosition()
    {
        recordedPosition = transform.position;
        position_deltaTime = 0;
        isStack_x = false;
        isStack_y = false;
    }

    //延迟开启球的尾迹渲染
    private IEnumerator delayToEnableTrailRender(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.GetComponent<TrailRenderer>().enabled = true;
    }

    //改变球体大小
    public void upBallSize()
    {
        if (targetScale < maxScale)
        {
            if (targetScale >= originScale)
            {
                targetScale += deltaScale;
            }
            else
            {
                targetScale += deltaScale_reverse;
            }
            //改变球的位置以免发生碰撞
            if (this.isAttracted)
            {
                attractedOffset += 0.025f;
            }
            //调整尾部
            gameObject.GetComponent<TrailRenderer>().widthMultiplier += 0.1f;
            //动画
            Transform anim_trans = transform.Find("Anim_ballSize(Clone)");
            if (anim_trans == null)
            {
                Instantiate(anim_ballSize, transform);
            }
        }
    }
    public void downBallSize()
    {
        if (targetScale > minScale)
        {
            if (targetScale > originScale)
            {
                targetScale -= deltaScale;
            }
            else
            {
                targetScale -= deltaScale_reverse;
            }
            //改变球的位置以免发生碰撞
            if (this.isAttracted)
            {
                attractedOffset -= 0.025f;
            }
            //调整尾部
            gameObject.GetComponent<TrailRenderer>().widthMultiplier -= 0.1f;
            //动画
            Transform anim_trans = transform.Find("Anim_ballSize(Clone)");
            if (anim_trans == null)
            {
                Instantiate(anim_ballSize, transform);
            }
        }
    }

    //powerful的启用
    public void enablePowerful()
    {
        //检测重复物体
        Transform child = transform.Find("Prop_powerful_child(Clone)");
        if (child != null)
        {
            //存在重复，重置时间
            Prop_powerful_child ctrl = child.gameObject.GetComponent<Prop_powerful_child>();
            ctrl.resetLiveTime();
        }
        else
        {
            Instantiate(powerful_child, transform);
        }
    }

    //球速加减控制
    public void addSpeed()
    {
        if (speedScale * 1.5f > maxSpeedScale)
        {
            rigid.velocity = rigid.velocity * (maxScale / speedScale);
        }
        else
        {
            rigid.velocity = rigid.velocity * 1.5f;
            speedScale *= 1.2f;
        }
    }

    public void downSpeed()
    {
        if (speedScale * (1f / 1.5f) < minSpeedScale)
        {
            rigid.velocity = rigid.velocity * (minScale / speedScale);
        }
        else
        {
            rigid.velocity = rigid.velocity * (1f / 1.5f);
            speedScale *= 1f / 1.5f;
        }
    }
}