using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_ctrl_initname : MonoBehaviour {

    //Anim
    private Animation anim;

	void Start () {
        anim = GetComponent<Animation>();
        anim.Play("anim_initname");
	}

    public void closeAnimFinished()
    {
        gameController.displayBg();
        gameController.displayMainMenu();        
    }
}
