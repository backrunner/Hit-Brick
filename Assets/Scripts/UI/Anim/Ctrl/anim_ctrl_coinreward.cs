using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anim_ctrl_coinreward : MonoBehaviour {

    private GameObject txt_coin;
    public long addNumber;

    private void Start()
    {
        txt_coin = transform.parent.Find("txt_coin").gameObject;
    }

    public void animEvent()
    {
        anim_coinreward ctrl = txt_coin.GetComponent<anim_coinreward>();
        ctrl.forwardNumber(addNumber + playerController.coin);
        if (!gameController.isLoadingPanelSpawned) { 
            playerController.coin += addNumber;
        }
    }	

    public void animFinished()
    {
        //获取父对象
        GameObject parent = transform.parent.gameObject;
        //清除列表里的该项
        anim_ctrl_clear ctrl = parent.GetComponent<anim_ctrl_clear>();
        ctrl.removeCurrent();
        ctrl.doList();  //执行下一项

        Destroy(gameObject);
    }
}
