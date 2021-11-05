using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup_manager : MonoBehaviour
{
    static popup_manager instance;
    private GameObject popLayer;

    public GameObject save_load_window;
    public List<GameObject> popup_normal_window;
    public GameObject setting_window;
    public GameObject gallery_window;
    public GameObject poppup_gameMenu;
    public GameObject poppup_GameEvent;
    public GameObject popup_nameEditor;

    public static popup_manager GetInstance()
    {
        if (instance == null)
        {
            GameObject temp = new GameObject("popup_manager");
            instance = temp.AddComponent<popup_manager>();
            
            instance.popup_normal_window = new List<GameObject>();
            DontDestroyOnLoad(instance);
            //DontDestroyOnLoad(instance.popLayer);
        }

        return instance;
    }

    public Transform Get_popLayer()
    {
        if(popLayer == null) instance.popLayer = Instantiate(Resources.Load<GameObject>("prefab/popup/pop_layer"));
        instance.popLayer.GetComponent<setMainCamera>().setCamera();
        return popLayer.transform;
    }

    public void show_save_load_window(int type) //0 load 1 save 2 new game (save and start)
    {
        if (save_load_window == null)
        {
            save_load_window = Instantiate(Resources.Load<GameObject>("prefab/save_load_window"), Get_popLayer());
        }
        else
        {
            int count = popup_manager.GetInstance().Get_popLayer().childCount;
            save_load_window.transform.SetSiblingIndex(count - 1);
        }

        save_load_window.SetActive(false);
        save_load_window.GetComponent<save_controller>().init(type);
    }


    //yes no type
    public void show_normal_window(string title, string content, Action yesCB, Action noCB,bool needSE_fromCloseManager = true, Action openCB = null, Action closeCB = null, bool onlyOpenCB = false, bool onlyCloseCB = false)
    {
        for (int i = popup_normal_window.Count - 1; i >= 0; i--) if (popup_normal_window[i] == null) popup_normal_window.RemoveAt(i);

        if (popup_normal_window.Count == 0)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("prefab/popup/popup_normalWindow"), Get_popLayer());
            popup_normal_window.Add(temp);

            temp.SetActive(false);

            pop_item pop_Item = temp.GetComponent<pop_item>();
            pop_Item.setting_btnType(pop_item.btnType.yes_no);
            pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
            pop_Item.Setting_Title_content(title, content);
            pop_Item.Setting_BtnCB(yesCB, noCB);
            pop_Item.Setting_CB(openCB, closeCB);
            pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);

            pop_Item.popup();
        }else if (popup_normal_window.Count > 0 && popup_normal_window[popup_normal_window.Count - 1].activeSelf)
        {
            GameObject temp_popup_normal_window = Instantiate(Resources.Load<GameObject>("prefab/popup/popup_normalWindow"), Get_popLayer());
            popup_normal_window.Add(temp_popup_normal_window);
            temp_popup_normal_window.SetActive(false);
            pop_item pop_Item = temp_popup_normal_window.GetComponent<pop_item>();
            pop_Item.setting_btnType(pop_item.btnType.yes_no);
            pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
            pop_Item.Setting_Title_content(title, content);
            pop_Item.Setting_BtnCB(yesCB, noCB);
            pop_Item.Setting_CB(openCB, closeCB);
            pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);
            pop_Item.Setting_closeAndDestory(true);
            pop_Item.popup();
        }
        else
        {
            int count = popup_manager.GetInstance().Get_popLayer().childCount;
            popup_normal_window[0].transform.SetSiblingIndex(count - 1);

            popup_normal_window[0].SetActive(false);

            pop_item pop_Item = popup_normal_window[0].GetComponent<pop_item>();
            pop_Item.setting_btnType(pop_item.btnType.yes_no);
            pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
            pop_Item.Setting_Title_content(title, content);
            pop_Item.Setting_BtnCB(yesCB, noCB);
            pop_Item.Setting_CB(openCB, closeCB);
            pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);

            pop_Item.popup();
        }
    }

    //OK type
    public void show_normal_window(string title, string content, Action okCB, bool needSE_fromCloseManager = true, Action openCB = null, Action closeCB = null, bool onlyOpenCB = false, bool onlyCloseCB = false)
    {
        for (int i = popup_normal_window.Count - 1; i >= 0; i--) if (popup_normal_window[i] == null) popup_normal_window.RemoveAt(i);

        if (popup_normal_window.Count == 0)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("prefab/popup/popup_normalWindow"), Get_popLayer());
            popup_normal_window.Add(temp);

            temp.SetActive(false);

            pop_item pop_Item = temp.GetComponent<pop_item>();
            pop_Item.setting_btnType(pop_item.btnType.ok);
            pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
            pop_Item.Setting_Title_content(title, content);
            pop_Item.Setting_BtnCB(okCB, null);
            pop_Item.Setting_CB(openCB, closeCB);
            pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);

            pop_Item.popup();
        }
        else if (popup_normal_window.Count > 0 && popup_normal_window[popup_normal_window.Count - 1].activeSelf)
        {
            GameObject temp_popup_normal_window = Instantiate(Resources.Load<GameObject>("prefab/popup/popup_normalWindow"), Get_popLayer());
            popup_normal_window.Add(temp_popup_normal_window);
            temp_popup_normal_window.SetActive(false);
            pop_item pop_Item = temp_popup_normal_window.GetComponent<pop_item>();
            pop_Item.setting_btnType(pop_item.btnType.ok);
            pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
            pop_Item.Setting_Title_content(title, content);
            pop_Item.Setting_BtnCB(okCB, null);
            pop_Item.Setting_CB(openCB, closeCB);
            pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);
            pop_Item.Setting_closeAndDestory(true);
            pop_Item.popup();
        }
        else
        {
            int count = popup_manager.GetInstance().Get_popLayer().childCount;
            popup_normal_window[0].transform.SetSiblingIndex(count - 1);

            popup_normal_window[0].SetActive(false);

            pop_item pop_Item = popup_normal_window[0].GetComponent<pop_item>();
            pop_Item.setting_btnType(pop_item.btnType.ok);
            pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
            pop_Item.Setting_Title_content(title, content);
            pop_Item.Setting_BtnCB(okCB, null);
            pop_Item.Setting_CB(openCB, closeCB);
            pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);

            pop_Item.popup();
        }
    }

    //only OK
    public void show_nameEditor_window(Action openCB = null, Action closeCB = null, bool onlyOpenCB = false, bool onlyCloseCB = false)
    {
        if (popup_nameEditor == null)
        {
            popup_nameEditor = Instantiate(Resources.Load<GameObject>("prefab/popup/popup_nameEditor"), Get_popLayer());
        }
        else
        {
            int count = popup_manager.GetInstance().Get_popLayer().childCount;
            popup_nameEditor.transform.SetSiblingIndex(count - 1);
        }

        popup_nameEditor.SetActive(false);
        pop_item pop_Item = popup_nameEditor.GetComponent<pop_item>();
        pop_Item.Setting_needSE_fromCloseManager(false);
        pop_Item.Setting_CB(openCB, closeCB);
        pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);
        pop_Item.setting_btnType(pop_item.btnType.ok);
        pop_Item.popup();
    }

    public void show_setting_window(bool needSE_fromCloseManager = true, Action openCB = null, Action closeCB = null, bool onlyOpenCB = false, bool onlyCloseCB = false)
    {
        if (setting_window == null)
        {
            setting_window = Instantiate(Resources.Load<GameObject>("prefab/setting_window"), Get_popLayer());
        }
        else
        {
            int count = popup_manager.GetInstance().Get_popLayer().childCount;
            setting_window.transform.SetSiblingIndex(count - 1);
        }

        setting_window.SetActive(false);

        pop_item pop_Item = setting_window.GetComponent<pop_item>();
        pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
        pop_Item.Setting_CB(openCB, closeCB);
        pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);

        setting_window.GetComponent<settingWindow>().init();
    }

    public void show_gameMenu(bool needSE_fromCloseManager = true, Action openCB = null, Action closeCB = null, bool onlyOpenCB = false, bool onlyCloseCB = false)
    {
        if (poppup_gameMenu == null)
        {
            poppup_gameMenu = Instantiate(Resources.Load<GameObject>("prefab/popup/poppup_gameMenu"), Get_popLayer());
        }
        else
        {
            int count = popup_manager.GetInstance().Get_popLayer().childCount;
            poppup_gameMenu.transform.SetSiblingIndex(count - 1);
        }

        poppup_gameMenu.SetActive(false);

        pop_item pop_Item = poppup_gameMenu.GetComponent<pop_item>();
        pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
        pop_Item.Setting_CB(openCB, closeCB);
        pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);

        pop_Item.popup();
    }

    public void show_mainGame_event(int type, bool needSE_fromCloseManager = true, Action openCB = null, Action closeCB = null, bool onlyOpenCB = false, bool onlyCloseCB = false)
    {
        if (poppup_GameEvent == null)
        {
            poppup_GameEvent = Instantiate(Resources.Load<GameObject>("prefab/mainGame/GameEvent"), Get_popLayer());
        }
        else
        {
            int count = popup_manager.GetInstance().Get_popLayer().childCount;
            poppup_GameEvent.transform.SetSiblingIndex(count - 1);
        }

        poppup_GameEvent.SetActive(false);

        pop_item pop_Item = poppup_GameEvent.GetComponent<pop_item>();
        pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
        pop_Item.Setting_CB(openCB, closeCB);
        pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);
    }


    public void closeAllPOPUP()
    {
        for (int i = popup_normal_window.Count - 1; i >= 0; i--) if (popup_normal_window[i] == null) popup_normal_window.RemoveAt(i);
        foreach (GameObject popup_normal_window_ in popup_normal_window)
        {
            popup_normal_window_.SetActive(false);
        }

        if (poppup_gameMenu != null) poppup_gameMenu.SetActive(false);
        if (poppup_GameEvent != null) poppup_GameEvent.SetActive(false);
        if (popup_nameEditor != null) popup_nameEditor.SetActive(false);
    }
}
