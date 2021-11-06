using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class TC_manager : MonoBehaviour
{
    private JsonData TC_datas; //資料從 JsonDataBase 來
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
        TC_datas = JsonDataBase.TC_datas;
    }

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

}
