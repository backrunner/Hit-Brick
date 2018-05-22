using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class anim_ctrl_logo : MonoBehaviour {

    public string mainmenu_filename;
    public int mainmenu_index;

    private Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void animFinished()
    {
        anim.Play("anim_panel_logo_out");
    }

    public void outAnimFinished()
    {
        if (mainmenu_filename != "")
        {
            SceneManager.LoadSceneAsync(mainmenu_filename);
        } else
        {
            SceneManager.LoadSceneAsync(mainmenu_index);
        }
    }
}
