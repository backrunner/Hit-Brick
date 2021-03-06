﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_anim_fade : MonoBehaviour {

    public GameObject anim_fade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballController ctrl = collision.gameObject.GetComponent<ballController>();
        if (ctrl != null)
        {
            playAnim("fadeout");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ballController ctrl = collision.gameObject.GetComponent<ballController>();
        if (ctrl != null)
        {
            playAnim("fadein");
        }
    }

    void playAnim(string type)
    {
        Transform anim_transform = transform.Find("Anim_Text_Fade(Clone)");
        if (anim_transform == null)
        {
            GameObject anim = Instantiate(anim_fade, transform);
            anim_text_fade anim_ctrl = anim.GetComponent<anim_text_fade>();
            anim_ctrl.type = type;
        }
        else
        {
            anim_text_fade anim_ctrl = anim_transform.gameObject.GetComponent<anim_text_fade>();
            anim_ctrl.type = type;
        }
    }
}
