using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_shoot_launcher : MonoBehaviour {

    private float launchCooldown;
    public float launchRate;
    public float liveTime;
    private float originliveTime;

    public float offset_x;

    //bullet obj
    public GameObject bullet;
    //pad
    private GameObject pad;

    private void Awake()
    {
        //init
        launchCooldown = launchRate;
        originliveTime = liveTime;
    }

    void Start () {
		if (levelController.pad != null)
        {
            pad = levelController.pad;
        } else
        {
            pad = GameObject.Find("Pad");
            levelController.pad = pad;
        }
	}
	
	// Update is called once per frame
	void Update () {
        launch();
	}

    void launch()
    {
        if (!levelController.isLevelPaused && !levelController.isGameOver)
        {
            liveTime -= Time.deltaTime;
            if (liveTime < 0)
            {
                Destroy(gameObject);
            }
            if (launchCooldown > 0)
            {
                launchCooldown -= Time.deltaTime;
            }
            else
            {
                Instantiate(bullet, new Vector3(pad.transform.position.x + offset_x, pad.transform.position.y, pad.transform.position.z), new Quaternion(0, 0, 0, 0));
                Instantiate(bullet, new Vector3(pad.transform.position.x - offset_x, pad.transform.position.y, pad.transform.position.z), new Quaternion(0, 0, 0, 0));
                launchCooldown = launchRate;
            }
        }
    }

    //reset
    public void resetliveTime() {
        liveTime = originliveTime;
    }
}
