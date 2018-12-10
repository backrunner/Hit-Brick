using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_panel_fail : MonoBehaviour {

	public virtual void animFinished()
    {
        Destroy(gameObject);
    }
}
