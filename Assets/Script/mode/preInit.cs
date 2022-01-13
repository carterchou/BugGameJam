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
#if !UNITY_EDITOR
		Debug.unityLogger.logEnabled = false;
#endif
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

		//Data
		effectData.update_data();
		itemData.update_data();
		commandData.update_data();

		//system_data.instance.init(); 目前主選單不太有需要紀錄的東西

		sceneChangeManager.GetInstance().changeScene("title");
        loadingManager.GetInstance().DoneLoading();
    }

}
