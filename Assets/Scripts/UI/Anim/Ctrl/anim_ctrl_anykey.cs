using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_anykey : MonoBehaviour {

    private GameObject txt_anykey;

    private void Start()
    {
        txt_anykey = transform.parent.Find("txt_anykey").gameObject;
    }

    public void playAnykeyAnim()
    {
        Animation anim = txt_anykey.GetComponent<Animation>();
        anim.Play("anim_anykey");
    }

}
