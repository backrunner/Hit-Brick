using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anim_ctrl_loading : MonoBehaviour {

    //level
    public string filename;

    //progressbar
    private Image progressbar;

    //Anim
    private Animation anim;

    private AsyncOperation progress;

    private void Start()
    {
        progressbar = gameObject.transform.Find("progressbar").GetComponent<Image>();
        anim = gameObject.GetComponent<Animation>();
    }

    public void animFinished()
    {
        progress = SceneManager.LoadSceneAsync(filename);
        StartCoroutine(loadScene());
    }

    //设置滑动条  
    private void setProgressValue(int value)
    {
        progressbar.fillAmount = (float)value / 100;
        Debug.Log((float)value / 100);
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
                showProgress+=4;
                setProgressValue(showProgress);
                yield return new WaitForEndOfFrame(); //等待一帧  
            }
        }
        toProgress = 100;
        while (showProgress < toProgress)
        {
            showProgress+=4;
            setProgressValue(showProgress);
            yield return new WaitForEndOfFrame(); //等待一帧  
        }

        //跳转
        progress.allowSceneActivation = true;
    }

}