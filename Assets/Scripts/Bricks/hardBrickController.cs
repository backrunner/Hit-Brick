using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hardBrickController : brickController {

    //砖块需要被打多少次才会烂
    public int brokenTimes;

    //Render
    private SpriteRenderer render;

    //Particles
    public GameObject particleLauncher;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    public override void ballHit_collision()
    {
        if (brokenTimes > 0)
        {
            //收到击打，次数减少
            brokenTimes--;
            Color t = randomColor();
            render.color = t;
            //粒子特效
            GameObject launcher = Instantiate(particleLauncher, ball.transform.position, new Quaternion(0,0,0,0));
            particle_hardBrick_rect_launcher ctrl = launcher.GetComponent<particle_hardBrick_rect_launcher>();
            ctrl.particle_color = t;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //随机一个淡色
    private Color randomColor(){
        int rgb = Random.Range(1, 4);
        Color t = new Color();
        t.a = 1;
        switch (rgb)
        {
            case 1:
                t.r = 1;
                t.g = randomColorValue();
                t.b = randomColorValue();
                break;
            case 2:
                t.r = randomColorValue();
                t.g = 1;
                t.b = randomColorValue();
                break;
            case 3:
                t.r = randomColorValue();
                t.g = randomColorValue();
                t.b = 1;
                break;
        }
        return t;
    }

    //随机颜色值
    float randomColorValue()
    {
       return Random.Range(130f, 255f) / 255f;
    }
}
