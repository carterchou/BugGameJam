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
    public GameObject poppup_gameMenu;

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
		if (popLayer == null) {
			instance.popLayer = Instantiate(Resources.Load<GameObject>("prefab/popup/pop_layer"));
		}
        //instance.popLayer.GetComponent<setMainCamera>().setCamera();
        return popLayer.transform;
    }

    //yes no type
    public void show_normal_window(string title, string content, Action yesCB, Action noCB,bool needSE_fromCloseManager = true, Action openCB = null, Action closeCB = null, bool onlyOpenCB = false, bool onlyCloseCB = false, pop_item.popType popType = pop_item.popType.normal, string btnLabel1 = "", string btnLabel2 = "")
    {
        for (int i = popup_normal_window.Count - 1; i >= 0; i--) if (popup_normal_window[i] == null) popup_normal_window.RemoveAt(i);

        if (popup_normal_window.Count == 0)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("prefab/popup/popup_normal"), Get_popLayer());
            popup_normal_window.Add(temp);

            temp.SetActive(false);

            pop_item pop_Item = temp.GetComponent<pop_item>();
            pop_Item.setting_btnType(pop_item.btnType.yes_no);
			pop_Item.setting_popType(popType);
			pop_Item.setting_btnLable(btnLabel1, btnLabel2);
			pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
            pop_Item.Setting_Title_content(title, content);
            pop_Item.Setting_BtnCB(yesCB, noCB);
            pop_Item.Setting_CB(openCB, closeCB);
            pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);

            pop_Item.popup();
        }else if (popup_normal_window.Count > 0 && popup_normal_window[popup_normal_window.Count - 1].activeSelf)
        {
            GameObject temp_popup_normal_window = Instantiate(Resources.Load<GameObject>("prefab/popup/popup_normal"), Get_popLayer());
            popup_normal_window.Add(temp_popup_normal_window);
            temp_popup_normal_window.SetActive(false);
            pop_item pop_Item = temp_popup_normal_window.GetComponent<pop_item>();
            pop_Item.setting_btnType(pop_item.btnType.yes_no);
			pop_Item.setting_popType(popType);
			pop_Item.setting_btnLable(btnLabel1, btnLabel2);
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
			pop_Item.setting_popType(popType);
			pop_Item.setting_btnLable(btnLabel1, btnLabel2);
			pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
            pop_Item.Setting_Title_content(title, content);
            pop_Item.Setting_BtnCB(yesCB, noCB);
            pop_Item.Setting_CB(openCB, closeCB);
            pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);

            pop_Item.popup();
        }
    }

    //OK type
    public void show_normal_window(string title, string content, Action okCB, bool needSE_fromCloseManager = true, Action openCB = null, Action closeCB = null, bool onlyOpenCB = false, bool onlyCloseCB = false, pop_item.popType popType = pop_item.popType.normal, string btnLabel1 = "", string btnLabel2 = "")
    {
        for (int i = popup_normal_window.Count - 1; i >= 0; i--) if (popup_normal_window[i] == null) popup_normal_window.RemoveAt(i);

        if (popup_normal_window.Count == 0)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("prefab/popup/popup_normal"), Get_popLayer());
            popup_normal_window.Add(temp);

            temp.SetActive(false);

            pop_item pop_Item = temp.GetComponent<pop_item>();
            pop_Item.setting_btnType(pop_item.btnType.ok);
			pop_Item.setting_popType(popType);
			pop_Item.setting_btnLable(btnLabel1, btnLabel2);
			pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
            pop_Item.Setting_Title_content(title, content);
            pop_Item.Setting_BtnCB(okCB, null);
            pop_Item.Setting_CB(openCB, closeCB);
            pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);

            pop_Item.popup();
        }
        else if (popup_normal_window.Count > 0 && popup_normal_window[popup_normal_window.Count - 1].activeSelf)
        {
            GameObject temp_popup_normal_window = Instantiate(Resources.Load<GameObject>("prefab/popup/popup_normal"), Get_popLayer());
            popup_normal_window.Add(temp_popup_normal_window);
            temp_popup_normal_window.SetActive(false);
            pop_item pop_Item = temp_popup_normal_window.GetComponent<pop_item>();
            pop_Item.setting_btnType(pop_item.btnType.ok);
			pop_Item.setting_popType(popType);
			pop_Item.setting_btnLable(btnLabel1, btnLabel2);
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
			pop_Item.setting_popType(popType);
			pop_Item.setting_btnLable(btnLabel1, btnLabel2);
			pop_Item.Setting_needSE_fromCloseManager(needSE_fromCloseManager);
            pop_Item.Setting_Title_content(title, content);
            pop_Item.Setting_BtnCB(okCB, null);
            pop_Item.Setting_CB(openCB, closeCB);
            pop_Item.Setting_onlyCB(onlyOpenCB, onlyCloseCB);

            pop_Item.popup();
        }
    }

    public void show_setting_window(bool needSE_fromCloseManager = true, Action openCB = null, Action closeCB = null, bool onlyOpenCB = false, bool onlyCloseCB = false, string btnLabel1 = "", string btnLabel2 = "")
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
		pop_Item.setting_btnLable(btnLabel1, btnLabel2);
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

    public void closeAllPOPUP()
    {
        for (int i = popup_normal_window.Count - 1; i >= 0; i--) if (popup_normal_window[i] == null) popup_normal_window.RemoveAt(i);
        foreach (GameObject popup_normal_window_ in popup_normal_window)
        {
            popup_normal_window_.SetActive(false);
        }

        if (poppup_gameMenu != null) poppup_gameMenu.SetActive(false);
    }
}
