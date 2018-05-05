using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class txt_anykey : MonoBehaviour {

    //obj
    private GameObject img_logo;
    private Animation anim_img_logo;

    private void Start()
    {
        img_logo = transform.parent.Find("img_logo").gameObject;
        anim_img_logo = img_logo.GetComponent<Animation>();
    }

    void Update () {
		if (Input.anyKeyDown && !anim_img_logo.isPlaying)
        {
            anim_img_logo.Play("anim_logo_up");
            //销毁防止二次触发
            Destroy(gameObject);
        }
	}

}
