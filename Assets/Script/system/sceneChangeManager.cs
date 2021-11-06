using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChangeManager : MonoBehaviour
{
    static sceneChangeManager instance;
	public int progress;

	public static sceneChangeManager GetInstance()
    {
        if (instance == null)
        {
            GameObject temp = new GameObject("sceneChangeManager");
            instance = temp.AddComponent<sceneChangeManager>();
			instance.progress = 0;
			DontDestroyOnLoad(instance);
        }

        return instance;
    }

    public void changeScene(string sceneName)
    {
		loadingManager.GetInstance().StartLoading();
		StartCoroutine(work_sceneChange(sceneName));
    }

    private IEnumerator work_sceneChange(string sceneName)
    {
        int progress = 0;

        //背景載入
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        progress = (int)(async.progress * 100);
        //取消自動切場景
        async.allowSceneActivation = false;

        while (progress < 0.9f)
        {
            progress = (int)(async.progress * 100);
            yield return null;
        }
        //它需要自己補滿100%
        progress = 100;
        async.allowSceneActivation = true;
        loadingManager.GetInstance().DoneLoading();
    }
}
