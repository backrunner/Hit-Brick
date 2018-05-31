using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logoScreenController : MonoBehaviour
{

    public GameObject canvas;
    public GameObject panel_logo;
    public static GameObject panel_logo_inscene;

    void Start()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas");
        }
        if (panel_logo != null)
        {
            panel_logo_inscene = Instantiate(panel_logo, canvas.transform);
            Animation anim = panel_logo_inscene.GetComponent<Animation>();
            anim.Play("anim_panel_logo");
        }
    }
}
