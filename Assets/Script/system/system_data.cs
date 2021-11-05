using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Encoders;

public class system_data : MonoBehaviour
{
    static system_data _instance;
    public static system_data instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("system_data").AddComponent<system_data>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    public class save_data{
        public List<string> GalleryID;

        public save_data()
        {
            GalleryID = new List<string>();
        }
    }

    save_data data;

    public void init()
    {
        SaveGame.customSavePath = Path.Combine(Application.streamingAssetsPath, "data", "saveData");
        if (SaveGame.Exists("save_data_system", SaveGamePath.CustomPath)) load();
        else
        {
            data = new save_data();
            updateSystemData();
        }
    }

    private void updateSystemData()
    {
        SaveGame.customSavePath = Path.Combine(Application.streamingAssetsPath, "data", "saveData");
        SaveGame.Save<system_data.save_data>("save_data_system", data, SaveGamePath.CustomPath);
    }


    private void load()
    {
        SaveGame.customSavePath = Path.Combine(Application.streamingAssetsPath, "data", "saveData");
        data = SaveGame.Load<system_data.save_data>("save_data_system", SaveGamePath.CustomPath);
        if (data == null) data = new save_data();
    }

    public bool CanShowInGallery(string GalleryID)
    {
        if (data.GalleryID == null) data.GalleryID = new List<string>();
        return data.GalleryID.Contains(GalleryID);
    }

    public void Add_GalleryID(string ID)
    {
        if (data.GalleryID == null) data.GalleryID = new List<string>();
        if (!data.GalleryID.Contains(ID)) data.GalleryID.Add(ID);
        updateSystemData();
    }
}
