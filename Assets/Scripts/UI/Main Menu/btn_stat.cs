using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_stat : MonoBehaviour {

    private Button btn;
    private GameObject canvas;

    //ui
    public GameObject panel_stat;
    private GameObject panel_inscene;

    //contents
    private Text txt_totalBricks;
    private Text txt_hollowBricks;
    private Text txt_normalBricks;
    private Text txt_hardBricks;
    private Text txt_pickedProp;
    private Text txt_totalLife;
    private Text txt_gameover;
    private Text txt_clear;
    private Text txt_pause;

    //anim
    private Animation anim;

	void Start () {
        //canvas
        canvas = gameController.canvas;
        //btn
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
	}
	
	void onClick()
    {
        panel_inscene = Instantiate(panel_stat, canvas.transform);
        gameController.panel_stat_inscene = panel_inscene;
        gameController.isStatPanelSpawned = true;
        //初始化
        statController.refreshData();
        initObjects();
        initText();
        //anim
        anim = panel_inscene.GetComponent<Animation>();
        anim.Play("anim_panel_stat");
        Animation anim_parent = gameController.panel_selectLevel_inscene.GetComponent<Animation>();
        anim_parent.Play("anim_panel_selectLevel_out_down");
    }

    //初始化
    void initObjects()
    {
        GameObject contents = panel_inscene.transform.Find("contents").gameObject;
        txt_totalBricks = getText(contents, "txt_totalbricks");
        txt_hollowBricks = getText(contents, "txt_hollowbricks");
        txt_normalBricks = getText(contents, "txt_normalbricks");
        txt_hardBricks = getText(contents, "txt_hardbricks");
        txt_pickedProp = getText(contents, "txt_pickedprop");
        txt_totalLife = getText(contents, "txt_totallife");
        txt_gameover = getText(contents, "txt_gameover");
        txt_clear = getText(contents, "txt_clear");
        txt_pause = getText(contents, "txt_pause");
    }

    void initText()
    {
        refreshText(txt_totalBricks, statController.hollowBrickCount + statController.normalBrickCount + statController.hardBrickCount);
        refreshText(txt_hollowBricks, statController.hollowBrickCount);
        refreshText(txt_normalBricks, statController.normalBrickCount);
        refreshText(txt_hardBricks, statController.hardBrickCount);
        refreshText(txt_pickedProp, statController.propPickedCount);
        refreshText(txt_totalLife, statController.deadBallCount);
        refreshText(txt_clear, statController.clearCount);
        refreshText(txt_gameover, statController.gameoverCount);
        refreshText(txt_pause, statController.pauseCount);
    }

    //获取Text
    Text getText(GameObject parent,string name)
    {
        GameObject obj = parent.transform.Find(name).gameObject;
        if (obj == null)
        {
            return null;
        }
        else
        {
            return obj.GetComponent<Text>();
        }
    }

    //刷新Text
    void refreshText(Text txt,long count)
    {
        txt.text = count.ToString();
    }
}
