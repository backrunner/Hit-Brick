using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_multiplay : MonoBehaviour {

    public Button btn;

    public GameObject panel_multiplay;
    private GameObject panel_multiplay_inscene;

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
        if (!gameController.isMultiPlaySpawned)
        {
            //instance
            panel_multiplay_inscene = Instantiate(panel_multiplay, canvas.transform);
            gameController.panel_multiplay_inscene = panel_multiplay_inscene;
            gameController.isMultiPlaySpawned = true;
            //anim
            Animation anim = panel_multiplay_inscene.GetComponent<Animation>();
            anim.Play("anim_panel_multiplay");
            Animation anim_parent = gameController.panel_selectLevel_inscene.GetComponent<Animation>();
            anim_parent.Play("anim_panel_selectLevel_out_down");
            //text
            Text txt_playername = panel_multiplay_inscene.transform.Find("txt_currentPlayerName").gameObject.GetComponent<Text>();
            txt_playername.text = gameController.player_name;

            //multiplay controller
            gameController.multiplayController = new MultiplayController(gameController.serverAddress, gameController.serverPort);
            gameController.multiplayController.TryConnect();
        }
    }
}
