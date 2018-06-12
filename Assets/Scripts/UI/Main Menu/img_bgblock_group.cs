using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class img_bgblock_group : MonoBehaviour {

    //运动方向
    private Vector3 moveToward;

    public float moveSpeed;

    private GameObject nextGroup;

    private bool isNextGroupSpawned = false;

    private GameObject canvas;

    public GameObject Canvas
    {
        get
        {
            return canvas;
        }

        set
        {
            canvas = value;
        }
    }

    void Start () {
        canvas = gameController.canvas;
        //初始化变量
        moveToward = new Vector3(0, 1, 0);

        isNextGroupSpawned = false;
    }
	
	void Update () {
        move();
	}

    void move()
    {
        //移动
        transform.Translate(moveToward * moveSpeed * Time.deltaTime);
        //清除屏幕外的obj
        if (transform.localPosition.y > 1080f)
        {
            Destroy(gameObject);            
        }
        if (transform.localPosition.y > 0 && !isNextGroupSpawned)
        {
            isNextGroupSpawned = true;
            nextGroup = Instantiate(gameObject, canvas.transform);
            gameController.bgblockList.Add(nextGroup);
            nextGroup.transform.localPosition = new Vector3(0, -1080 + transform.position.y, 10);
            nextGroup.transform.SetSiblingIndex(0); //置于底层
        }
    }
}
