using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_mp_createGame : MonoBehaviour {

    private Button btn;

    private GameObject panel_multiplay_room_inscene;
    private GameObject canvas;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);

        //canvas init
        canvas = gameController.canvas;
    }

    void onClick()
    {
        if (!gameController.isMultiPlayRoomSpawned)
        {
            //instance
            panel_multiplay_room_inscene = Instantiate(gameController.panel_multiplay_room, canvas.transform);
            gameController.panel_multiplay_room_inscene = panel_multiplay_room_inscene;
            gameController.isMultiPlaySpawned = true;
            //anim
            Animation anim = panel_multiplay_room_inscene.GetComponent<Animation>();
            anim.Play("anim_panel_multiplay_room");
            Animation anim_parent = gameController.panel_multiplay_inscene.GetComponent<Animation>();
            anim_parent.Play("anim_panel_multiplay_out_left");


        }
    }
}
