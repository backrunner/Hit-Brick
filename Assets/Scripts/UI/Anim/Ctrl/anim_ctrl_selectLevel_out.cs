using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_selectLevel_out : MonoBehaviour {

    //动画播放完成
	public void animFinished()
    {
        //重置开关
        gameController.isSelectLevelSpawned = false;
        //摧毁obj
        Destroy(gameObject);
    }
}
