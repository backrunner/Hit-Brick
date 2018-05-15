using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_clear : Effect {

    //相机size缩放百分比
    private float deltaPercent;
    private float percent;

    //相机目标size
    public float targetSize;
    private float originSize;

    //相机平移平滑阻尼时间
    public float smoothTime;

    //velocity
    private float velocity_x;
    private float velocity_y;

    //球的阻力大小
    public float drag;

    //GameObject
    private GameObject ball;

    void Start()
    {
        //获取原size用于插值计算
        originSize = camera.orthographicSize;
        //计算deltaPercent
        deltaPercent = Time.fixedDeltaTime / liveTime;        
        //获取ball
        if (levelController.currentBall != null)
        {
            ball = levelController.currentBall;
            Rigidbody2D rigidbody = ball.GetComponent<Rigidbody2D>();
            rigidbody.drag = drag;
        }
    }

    private void Update()
    {
        //处理相机
        processSize();
        move();
    }

    void processSize()
    {
        //缩放相机画面
        camera.orthographicSize = Mathf.Lerp(originSize, targetSize, percent);
        percent += deltaPercent;
    }

    void move()
    {
        //用平滑阻尼平移相机
        if (ball != null)
        {
            float position_x = Mathf.SmoothDamp(camera.transform.position.x, ball.transform.position.x, ref velocity_x, smoothTime);
            float position_y = Mathf.SmoothDamp(camera.transform.position.y, ball.transform.position.y, ref velocity_y, smoothTime);
            camera.transform.position = new Vector3(position_x, position_y, -10);
        }
    }
}
