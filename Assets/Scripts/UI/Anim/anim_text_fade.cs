using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anim_text_fade : UIAnim {

    //动画的类型
    public string type;
    //目标Alpha
    public float targetAlpha;
    //deltaAlpha
    private float deltaAlpha;
    //render
    private Text text;

    private void Awake()
    {
        //初始化Text
        text = transform.parent.gameObject.GetComponent<Text>();
        //deltaAlpha初始化
        if (Time.deltaTime > 0)
        {
            deltaAlpha = 1f / (liveTime / Time.deltaTime);
        }
        else
        {
            deltaAlpha = 1f / (liveTime / 0.0167f);
        }
    }

    void Start () {               

    }
	
	// Update is called once per frame
	void Update () {
        play();
	}

    public void play()
    {
        Color t = text.color;
        switch (type)
        {
            case "fadein":
                if (t.a<1)
                {
                    t.a += deltaAlpha;
                    if (t.a > 1)
                    {
                        t.a = 1;
                        text.color = t;
                        Destroy(gameObject);
                        return;
                    }
                    text.color = t;
                } else
                {
                    Destroy(gameObject);
                }
                break;
            case "fadeout":
                if (t.a>targetAlpha)
                {
                    t.a -= deltaAlpha;
                    if (t.a < targetAlpha)
                    {
                        t.a = targetAlpha;
                        text.color = t;
                        Destroy(gameObject);
                        return;
                    }
                    text.color = t;
                } else
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
