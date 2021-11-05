using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Encoders;

public class save_item : MonoBehaviour
{
    int item_id;
    int type; // 0 load 1 save 2 save and start 3 delete
    bool isDelete = false;
    [Header("newOBJ / has data OBJ")]
    public GameObject[] new_hasData;
    public Button item_btn;
    [Header("data use")]
    public Text text_id;
    public Text text_data;
    public Text text_playTime;
    public Image screenImg;

    [Header("Delete use")]
    public GameObject deleteOBJ;

    userData.save_data data = null;
    save_controller controller;

    //若data 無資料會回傳false
    public void init(int id, int type, save_controller controller ,Action<bool> callback)
    {
        item_id = id;
        this.controller = controller;
        this.type = type;
        new_hasData[0].SetActive(false);
        new_hasData[1].SetActive(false);
        item_btn.interactable = false;

        //SaveGame.LoadCallback = new SaveGame.LoadHandler(Done_load);     
        //SaveGame.customSavePath = Path.Combine(Application.persistentDataPath, "saved");
        SaveGame.customSavePath = Path.Combine(Application.streamingAssetsPath, "data", "saveData");
        if (!SaveGame.Exists("save_data_" + item_id, SaveGamePath.CustomPath))
        {
            if (type == 1) //若是存檔則都可以選擇，若為載入，則無檔案不可選
            {
                new_hasData[0].SetActive(true);
                item_btn.interactable = true;
            }

            callback(false);
            return;
        }

        load();

        new_hasData[1].SetActive(true);
        item_btn.interactable = true;

        callback(true);
    }

    public void show_close(bool show)
    {
        gameObject.SetActive(show);
    }

    public void press()
    {
        if (isDelete && data != null)
        {
            Debug.Log("[Save/Load] Prepate to Delete!");
            popup_manager.GetInstance().show_normal_window(
                TC_manager.GetInstance().GetTC_value("popup_askDeleteTitle"),
                TC_manager.GetInstance().GetTC_value("popup_askDeleteContent"),
                DeleteData,
                null,
                true,
                () =>
                {
                    AudioManager.PlaySE("open");
                }
            );

            return;
        }else if (isDelete && data == null)
        {
            return;
        }

        if (type == 1) {
            popup_manager.GetInstance().show_normal_window(
                data == null ? TC_manager.GetInstance().GetTC_value("popup_askSaveTitle") : TC_manager.GetInstance().GetTC_value("popup_askSaveOverwriteTitle"),
                data == null ? TC_manager.GetInstance().GetTC_value("popup_askSaveContent") : TC_manager.GetInstance().GetTC_value("popup_askSaveOverwriteContent"),
                () =>
                {
                    Debug.Log("[Save/Load] Save save data");
                    updateSave();
                },
                null,
                true,
                () =>
                {
                    AudioManager.PlaySE("open");
                }
            );

        }
        else if (type == 0 && data != null)
        {
            //TC_manager.GetInstance().GetTC_value("setting_saveSettingContent")
            popup_manager.GetInstance().show_normal_window(
                TC_manager.GetInstance().GetTC_value("popup_askLoadTitle"),
                TC_manager.GetInstance().GetTC_value("popup_askLoadContent"),
                () =>
                {
                    Debug.Log("[Save/Load] Load save data");
                    loadingManager.GetInstance().StartLoading();
                    //mainGameManager.GetInstance().StartGame(data, () => { controller.gameObject.SetActive(false); loadingManager.GetInstance().DoneLoading(); });
                },
                null,
                true,
                () =>
                {
                    AudioManager.PlaySE("open");
                }
            );
        }
                
    }

    private void DeleteData()
    {
        Debug.Log("[Save/Load] Delete save data!");
        SaveGame.customSavePath = Path.Combine(Application.streamingAssetsPath, "data", "saveData");
        SaveGame.Delete("save_data_" + item_id, SaveGamePath.CustomPath);

        new_hasData[0].SetActive(false);
        new_hasData[1].SetActive(false);

        deleteOBJ.SetActive(false);
        data = null;
        popup_manager.GetInstance().show_normal_window(
            TC_manager.GetInstance().GetTC_value("popup_DoneDeleteTitle"),
            TC_manager.GetInstance().GetTC_value("popup_DoneDeleteContent"),
            null,
            true,
            () =>
            {
                AudioManager.PlaySE("open");
            }
        );
    }

    private void updateSave()
    {
        userData.instance.updatePlayTime();
        data = userData.instance.GetUserData();
        data.id = item_id;
        SaveGame.customSavePath = Path.Combine(Application.streamingAssetsPath, "data", "saveData");
        SaveGame.Save<userData.save_data>("save_data_" + item_id, data, SaveGamePath.CustomPath);

        new_hasData[0].SetActive(false);
        new_hasData[1].SetActive(false);
        item_btn.interactable = false;

        load();

        new_hasData[1].SetActive(true);
        item_btn.interactable = true;
    }

    private void load()
    {
        SaveGame.customSavePath = Path.Combine(Application.streamingAssetsPath, "data", "saveData");
        data = SaveGame.Load<userData.save_data>("save_data_" + item_id, SaveGamePath.CustomPath);
        text_id.text = String.Format("No.{0}", data.id);
        text_data.text = String.Format("{0} {1}\n{2}{3}", TC_manager.GetInstance().GetTC_value("day"), data.game_day, TC_manager.GetInstance().GetTC_value("saiminLV"), data.skill.saiminLV);
        text_data.text = text_data.text.Replace("\\n", "\n");
        int hour = (int)data.playTime.TotalSeconds / 3600;
        int minute = ((int)data.playTime.TotalSeconds % 3600) / 60;
        int sec = ((int)data.playTime.TotalSeconds % 3600) % 60;
        text_playTime.text = String.Format("{0} - {1:00.}:{2:00.}:{3:00.}", TC_manager.GetInstance().GetTC_value("playTime"), hour, minute, sec);
    }

    public void initDelete()
    {
        isDelete = true;
        if (data == null)
        {
            if (type == 1 || type == 2)
            {
                new_hasData[0].SetActive(false);
            }
        }
        else
        {
            deleteOBJ.SetActive(true);
        }
    }

    public void closeDelete()
    {
        if (data == null)
        {
            if (type == 1 || type == 2)
            {
                new_hasData[0].SetActive(true);
            }
        }

        deleteOBJ.SetActive(false);
        isDelete = false;
    }

    /*void testSaveBack(object obj, string identifier, bool encode, string password, ISaveGameSerializer serializer, ISaveGameEncoder encoder, Encoding encoding, SaveGamePath path)
    {
        Debug.LogWarning("***done save !");
        Debug.LogWarning(((dataOBJ)obj).data);
    }*/

    /*void Done_load(object obj, string identifier, bool encode, string password, ISaveGameSerializer serializer, ISaveGameEncoder encoder, Encoding encoding, SaveGamePath path)
    {

    }*/
}
