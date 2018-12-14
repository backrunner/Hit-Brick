using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_mp_createGame : MonoBehaviour {

    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    void onClick()
    {
        if (!gameController.isMultiPlayRoomSpawned)
        {
            gameController.multiplayController.CreateRoom();
        }
    }
}
