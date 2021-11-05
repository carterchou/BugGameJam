using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
public class GlobalValueManager : MonoBehaviour
{
    private JsonData datas;
    private static GlobalValueManager _instance;

    public static GlobalValueManager GetInstance()
    {
        if (_instance == null)
        {
            GameObject temp = new GameObject("GlobalValueManager");
            _instance = temp.AddComponent<GlobalValueManager>();
            DontDestroyOnLoad(_instance);
        }

        return _instance;
    }
    public void update_data()
    {
        //StartCoroutine(update_data_(callBack));
        datas = JsonDataBase.Global_datas;
    }

    /*private IEnumerator update_data_(Action<string> callBack)
    {
        UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/globalValue.json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            Debug.LogWarning("Global data load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        string data_raw = uwr.downloadHandler.text;
        if (data_raw != null)
        {
            //data_raw = DeEncode.Decrypt(data_raw);
            datas = JsonMapper.ToObject(data_raw);
            callBack?.Invoke("");
        }
        else
        {
            Debug.LogWarning("Global data load fail!");
            callBack?.Invoke("error");
            yield break;
        }
    }*/

    //若不是使用quickTake方式拿取TC，請記得使用動態方式拿取，或自己監聽事件
    public T Get_value<T>(string key)
    {
        if (datas.Count <= 0) return default;

        try
        {
            JsonData data = null;
            for (int i = 0; i < datas.Count; i++)
            {
                if (datas[i]["key"].ToString().Equals(key))
                {
                    data = datas[i];
                    break;
                }
            }

            if (data != null && (typeof(T) == typeof(int) || typeof(T) == typeof(string)))
            {
                Debug.Log(string.Format("[GlobalValue] Found : {0} = {1}", key, Convert.ChangeType(data["value"].ToString(), typeof(T))));
                return (T)Convert.ChangeType(data["value"].ToString(), typeof(T));
            }
            else
            {
                Debug.Log(string.Format("[GlobalValue] Not Found : {0}", key));
                return default;
            }


        }
        catch (Exception e)
        {
            Debug.Log(string.Format("[GlobalValue] ERROR : {0}", key));
            return default;
        }

    }
}
