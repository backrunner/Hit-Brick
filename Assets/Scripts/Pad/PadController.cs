using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moveWithMouse();
	}

    void moveWithMouse()
    {
        Vector3 padPos = new Vector3(0f, this.transform.position.y, 0);
        //获取鼠标坐标映射到游戏场景
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        //计算pad坐标，保证pad不出屏幕
        //pad长度为1.2u
        padPos.x = Mathf.Clamp(mousePos, (-7.62f-(1f-transform.localScale.x)*0.6f),(7.62f+(1f-transform.localScale.x)*0.6f));
        this.transform.position = padPos;
    }
}
