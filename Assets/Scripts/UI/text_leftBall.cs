using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_leftBall : MonoBehaviour {

    public GameObject anim_fade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballController ctrl = collision.gameObject.GetComponent<ballController>();
        if (ctrl != null)
        {
            playAnim("fadein");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ballController ctrl = collision.gameObject.GetComponent<ballController>();
        if (ctrl != null)
        {
            playAnim("fadeout");
        }
    }

    void playAnim(string type)
    {
        Transform anim_transform = transform.Find("Anim_Text_Fade");
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
