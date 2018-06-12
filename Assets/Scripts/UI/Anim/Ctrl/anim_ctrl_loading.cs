using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anim_ctrl_loading : MonoBehaviour
{

    //level
    public string filename;
    public int levelIndex = -1;
    //progressbar
    private Image progressbar;

    //Anim
    private Animation anim;

    private AsyncOperation progress;

    private void Awake()
    {
        //初始化
        levelIndex = -1;
    }

    private void Start()
    {
        progressbar = gameObject.transform.Find("progressbar").GetComponent<Image>();
        anim = gameObject.GetComponent<Animation>();
        DontDestroyOnLoad(gameController.canvas);
    }

    public void animFinished()
    {
        if (levelIndex > -1)
        {
            if (levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                progress = SceneManager.LoadSceneAsync(levelIndex);
            }
            else
            {
                progress = SceneManager.LoadSceneAsync(0);
                levelIndex = 0;
                //清空物件
                Destroy(gameController.eventSystem);
                Destroy(gameController.panel_mainMenu_inscene);
                Destroy(gameController.thisgameObj);
            }
        }
        else
        {
            progress = SceneManager.LoadSceneAsync(filename);            
        }
        Destroy(gameController.panel_selectLevel_inscene);
        gameController.isSelectLevelSpawned = false;    //重置开关
        //清空背景
        gameController.clearBg();
        StartCoroutine(loadScene());
    }

    public void outAnimFinished()
    {
        levelController.isLevelPaused = false;
        gameController.isLoadingPanelSpawned = false;
        Destroy(gameObject);
    }

    //设置滑动条  
    private void setProgressValue(int value)
    {
        progressbar.fillAmount = (float)value / 100;
    }

    private IEnumerator loadScene()
    {
        //禁用关卡跳转
        progress.allowSceneActivation = false;

        int toProgress = 0;
        int showProgress = 0;
        //测试了一下，进度最大就是0.9  
        while (progress.progress < 0.9f)
        {
            toProgress = (int)(progress.progress * 100);

            while (showProgress < toProgress)
            {
                showProgress += 4;
                setProgressValue(showProgress);
                yield return new WaitForEndOfFrame(); //等待一帧  
            }
        }
        toProgress = 100;
        while (showProgress < toProgress)
        {
            showProgress += 4;
            setProgressValue(showProgress);
            yield return new WaitForEndOfFrame(); //等待一帧  
        }

        anim.Play("anim_panel_loading_out");
        //跳转
        progress.allowSceneActivation = true;
        yield return new WaitForFixedUpdate();
        gameController.currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (gameController.currentLevelIndex == 1 || gameController.currentLevelIndex == 2)
        {
            //重绘背景
            gameController.displayBg();
        }
    }

}