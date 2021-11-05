using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class JsonDataBase : MonoBehaviour
{
    public static JsonData TC_datas;
    public static JsonData story_datas;
    public static JsonData Global_datas;
    public static JsonData Galley_datas;
    public static JsonData eventData;
    public static JsonData commandData;
    public static JsonData CharaData;
    public static JsonData ItemData;


    public static void Init(Action<string> callBack = null)
    {
        CoroutineHub.GetInstance().StartCoroutine(_Init(callBack));
    }

    static IEnumerator _Init(Action<string> callBack)
    {
        string err = "";
        bool isInit = false;
        yield return 0;

        //TC & Story TC
        isInit = true;
        CoroutineHub.GetInstance().StartCoroutine(update_data_TC_Story((err_) => { err = err_; isInit = false; }));
        yield return new WaitWhile(() => isInit);
        if (err != "")
        {
            callBack?.Invoke("error");
            yield break;
        }

        //Global
        isInit = true;
        CoroutineHub.GetInstance().StartCoroutine(update_data_Global((err_) => { err = err_; isInit = false; }));
        yield return new WaitWhile(() => isInit);
        if (err != "")
        {
            callBack?.Invoke("error");
            yield break;
        }

        //Gallery
        isInit = true;
        CoroutineHub.GetInstance().StartCoroutine(update_data_Gallery((err_) => { err = err_; isInit = false; }));
        yield return new WaitWhile(() => isInit);
        if (err != "")
        {
            callBack?.Invoke("error");
            yield break;
        }

        //Event
        isInit = true;
        CoroutineHub.GetInstance().StartCoroutine(update_data_event((err_) => { err = err_; isInit = false; }));
        yield return new WaitWhile(() => isInit);
        if (err != "")
        {
            callBack?.Invoke("error");
            yield break;
        }

        //Command
        isInit = true;
        CoroutineHub.GetInstance().StartCoroutine(update_data_Command((err_) => { err = err_; isInit = false; }));
        yield return new WaitWhile(() => isInit);
        if (err != "")
        {
            callBack?.Invoke("error");
            yield break;
        }

        //Chata
        isInit = true;
        CoroutineHub.GetInstance().StartCoroutine(update_data_Chara((err_) => { err = err_; isInit = false; }));
        yield return new WaitWhile(() => isInit);
        if (err != "")
        {
            callBack?.Invoke("error");
            yield break;
        }

        //Item
        isInit = true;
        CoroutineHub.GetInstance().StartCoroutine(update_data_Item((err_) => { err = err_; isInit = false; }));
        yield return new WaitWhile(() => isInit);
        if (err != "")
        {
            callBack?.Invoke("error");
            yield break;
        }

        yield return 0;
        callBack?.Invoke("");
    }

    //TC
    private static IEnumerator update_data_TC_Story(Action<string> callBack)
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
    }

    //Global
    private static IEnumerator update_data_Global(Action<string> callBack)
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
            Global_datas = JsonMapper.ToObject(data_raw);
            callBack?.Invoke("");
        }
        else
        {
            Debug.LogWarning("Global data load fail!");
            callBack?.Invoke("error");
            yield break;
        }
    }

    //Gallery
    private static IEnumerator update_data_Gallery(Action<string> callBack)
    {
        //Gallery Data
        UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/GalleryData.json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            Debug.LogWarning("GalleryData load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        string data_raw = uwr.downloadHandler.text;
        if (data_raw != null)
        {
            //data_raw = DeEncode.Decrypt(data_raw);
            Galley_datas = JsonMapper.ToObject(data_raw);
            //sort
            for (int x = 0; x < Galley_datas.Count; x++)
            {
                for (int i = Galley_datas[x].Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < i; ++j)
                    {
                        if ((int)Galley_datas[x][j]["sortID"] > (int)Galley_datas[x][j + 1]["sortID"])
                        {
                            JsonData temp = Galley_datas[x][j];
                            Galley_datas[x][j] = Galley_datas[x][j + 1];
                            Galley_datas[x][j + 1] = temp;
                        }
                    }
                }
            }

        }
        else
        {
            Debug.LogWarning("GalleryData load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        callBack?.Invoke("");
        ///Gallery Data
    }

    //Event
    private static IEnumerator update_data_event(Action<string> callBack)
    {
        UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/Event.json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            Debug.LogWarning("Event data load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        string data_raw = uwr.downloadHandler.text;
        if (data_raw != null)
        {
            //data_raw = DeEncode.Decrypt(data_raw);
            eventData = JsonMapper.ToObject(data_raw);
            callBack?.Invoke("");
        }
        else
        {
            Debug.LogWarning("Event data load fail!");
            callBack?.Invoke("error");
            yield break;
        }
    }

    //Command
    private static IEnumerator update_data_Command(Action<string> callBack)
    {
        UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/command.json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            Debug.LogWarning("command data load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        string data_raw = uwr.downloadHandler.text;
        if (data_raw != null)
        {
            //data_raw = DeEncode.Decrypt(data_raw);
            commandData = JsonMapper.ToObject(data_raw);
            //sort
            for (int i = commandData.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < i; ++j)
                {
                    if ((int)commandData[j]["sortID"] > (int)commandData[j + 1]["sortID"])
                    {
                        JsonData temp = commandData[j];
                        commandData[j] = commandData[j + 1];
                        commandData[j + 1] = temp;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("command data load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        callBack?.Invoke("");
    }

    //Chara
    private static IEnumerator update_data_Chara(Action<string> callBack)
    {
        UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/CharaList.json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            Debug.LogWarning("CharaList data load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        string data_raw = uwr.downloadHandler.text;
        if (data_raw != null)
        {
            //data_raw = DeEncode.Decrypt(data_raw);
            CharaData = JsonMapper.ToObject(data_raw);
            callBack?.Invoke("");
        }
        else
        {
            Debug.LogWarning("CharaList data load fail!");
            callBack?.Invoke("error");
            yield break;
        }
    }


    //Chara
    private static IEnumerator update_data_Item(Action<string> callBack)
    {
        UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/ItemList.json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            Debug.LogWarning("ItemList data load fail!");
            callBack?.Invoke("error");
            yield break;
        }

        string data_raw = uwr.downloadHandler.text;
        if (data_raw != null)
        {
            //data_raw = DeEncode.Decrypt(data_raw);
            ItemData = JsonMapper.ToObject(data_raw);
            callBack?.Invoke("");
        }
        else
        {
            Debug.LogWarning("ItemList data load fail!");
            callBack?.Invoke("error");
            yield break;
        }
    }
}
