using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userData : MonoBehaviour
{
    static userData _instance;
    public static userData instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("userData").AddComponent<userData>();
                //DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    public enum TimeType {
        morning,
        afternoon,
        night
    }

    public class save_data
    {
        public int id;
        public TimeSpan playTime;

        public save_data(int id = -1, TimeSpan playTime = new TimeSpan())
        {
            this.id = id;
            this.playTime = playTime;

        }
    }

    save_data data;
    DateTime temp_playTime;

    public void initUser(save_data data = null)
    {
        this.data = data;
        temp_playTime = DateTime.Now;
    }

    public save_data GetUserData()
    {
        return data;
    }

    public void newPlayerData()
    {
        data = new save_data();
    }
	public void updatePlayTime() {
		data.playTime += DateTime.Now - temp_playTime;
		temp_playTime = DateTime.Now;
	}
}
