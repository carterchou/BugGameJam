using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class TC_manager : MonoBehaviour
{
    private JsonData TC_datas;
    private JsonData story_datas;
    private static TC_manager _instance;

    public static TC_manager GetInstance()
    {
        if (_instance == null)
        {
            GameObject temp = new GameObject("TC_manager");
            _instance = temp.AddComponent<TC_manager>();
            DontDestroyOnLoad(_instance);
        }

        return _instance;
    }

    public void updateTC_data()
    {
        //StartCoroutine(updateTC_data_(callBack));
        TC_datas = JsonDataBase.TC_datas;
        story_datas = JsonDataBase.story_datas;
    }

    /*private IEnumerator updateTC_data_(Action<string> callBack)
    {
        UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/clientTC.json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            Debug.LogWarning("TC data load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        string data_raw = uwr.downloadHandler.text;
        if (data_raw != null)
        {
            //data_raw = DeEncode.Decrypt(data_raw);
            TC_datas = JsonMapper.ToObject(data_raw);
        }
        else
        {
            Debug.LogWarning("TC data load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/storyTC.json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            Debug.LogWarning("Stroy data load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        data_raw = uwr.downloadHandler.text;
        if (data_raw != null)
        {
            //data_raw = DeEncode.Decrypt(data_raw);
            story_datas = JsonMapper.ToObject(data_raw);
            callBack?.Invoke("");
        }
        else
        {
            Debug.LogWarning("Stroy data load fail!");
            callBack?.Invoke("error");
            yield break;
        }
    }*/

    //若不是使用quickTake方式拿取TC，請記得使用動態方式拿取，或自己監聽事件
    public string GetTC_value(string key)
    {
        if (TC_datas.Count <= 0) return "";

        try
        {
            JsonData data = new JsonData();
            for (int i = 0; i < TC_datas.Count; i++)
            {
                if (TC_datas[i]["key"].ToString().Equals(key))
                {
                    data = TC_datas[i];
                    break;
                }
            }

            return data.Count > 0 ? data["value"][languageManager.Language_index].ToString() : "";
        }
        catch (Exception e)
        {
            return "";
        }
       
    }

    public string GetStroy_value(string key)
    {
        if (story_datas.Count <= 0) return "";

        try
        {
            JsonData data = new JsonData();
            for (int i = 0; i < story_datas.Count; i++)
            {
                if (story_datas[i]["key"].ToString().Equals(key))
                {
                    data = story_datas[i];
                    break;
                }
            }

            return data.Count > 0 ? data["value"][languageManager.Language_index].ToString() : "";
        }
        catch (Exception e)
        {
            return "";
        }

    }
}
