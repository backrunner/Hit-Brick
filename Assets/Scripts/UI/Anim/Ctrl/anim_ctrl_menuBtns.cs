using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_menuBtns : MonoBehaviour {

    //预置
    public GameObject menuBtns;
    private GameObject panel;

    private void Start()
    {
        panel = transform.parent.gameObject;
    }

    //上移动画播放完成
    public void upAnimFinished()
    {
        GameObject btns = Instantiate(menuBtns, panel.transform);
        Animation anim = btns.GetComponent<Animation>();
        anim.Play("anim_menubtns");
    }
}
