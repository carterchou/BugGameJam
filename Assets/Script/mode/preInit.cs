using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

using LitJson;

public class preInit : MonoBehaviour
{
    public static preInit instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        loadingManager.GetInstance().initLoad();
        closeManager.GetInstance().initClose();
        loadingManager.GetInstance().StartLoading();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Debug.unityLogger.logEnabled = false;
        IEnumerator_init();
    }
    private void IEnumerator_init()
    {
        CoroutineHub.GetInstance().StartCoroutine(IEnumerator_init_());
    }

    IEnumerator IEnumerator_init_()
    {
        AudioManager.initSetting();
        languageManager.initLanguage();
        resolution_manager.initResolution();
        
        bool isInit = true;
        string err = "";
        JsonDataBase.Init( (err_) => { err = err_; isInit = false; });
        yield return new WaitWhile(() => isInit);

        if (err != "") {
            sceneChangeManager.GetInstance().changeScene("preInit");
            yield break;
        }

        //TC
        TC_manager.GetInstance().updateTC_data();
        //Global
        GlobalValueManager.GetInstance().update_data();

        system_data.instance.init();

        sceneChangeManager.GetInstance().changeScene("main");
        loadingManager.GetInstance().DoneLoading();
    }

}
