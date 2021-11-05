using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pop_item : MonoBehaviour
{
    Action openCallBack = null;
    bool onlyOpenCallBack = false;
    Action closeCallBack = null;
    bool onlyCloseCallBack = false;

    Action btnCallBack = null; //OK YES
    Action btnCallBack2 = null; // NO

    bool needSE_fromCloseManager = true;
    bool closeAndDestory = false;

    public Text title;
    public Text content;

    public GameObject[] btnTypeObj;

    public enum btnType{
        ok,
        yes_no,
        other
    }

    btnType popType = btnType.other;
    bool needLockClose = false;

    public void Setting_CB(Action open, Action close)
    {
        openCallBack += open;
        closeCallBack += close;
    }

    public void Setting_needSE_fromCloseManager(bool needSE_fromCloseManager)
    {
        this.needSE_fromCloseManager = needSE_fromCloseManager;
    }

    public void Setting_Title_content(string title_text, string content_text)
    {
        if (title != null)
        {
            title.text = title_text;
            title.text = title.text.Replace("\\n", "\n");
        }

        if (content != null)
        {
            content.text = content_text;
            content.text = content.text.Replace("\\n", "\n");
        }

    }

    public void setting_btnType(btnType type)
    {
        foreach(GameObject btnTypeObj_ in btnTypeObj)
        {
            btnTypeObj_.SetActive(false);
        }

        popType = type;

        switch (type)
        {
            case btnType.ok:
                needLockClose = true;
                if(btnTypeObj.Length >= 1) btnTypeObj[0].SetActive(true);
                break;
            case btnType.yes_no:
                if (btnTypeObj.Length >= 2) btnTypeObj[1].SetActive(true);
                break;
        }

    }

    public void Setting_BtnCB(Action btnCallBack, Action btnCallBack2)
    {
        this.btnCallBack += btnCallBack;
        this.btnCallBack2 += btnCallBack2;
    }

    public void Setting_onlyCB(bool onlyOpenCallBack, bool onlyCloseCallBack)
    {
        this.onlyOpenCallBack = onlyOpenCallBack;
        this.onlyCloseCallBack = onlyCloseCallBack;
    }

    public void pressBtn(int index)
    {
        switch (index)
        {
            case 0:
                btnCallBack?.Invoke();
                break;
            case 1:
                btnCallBack2?.Invoke();
                break;
        }
    }

    public void Setting_closeAndDestory(bool closeAndDestory)
    {
        this.closeAndDestory = closeAndDestory;
    }

    public void popup()
    {
        openCallBack?.Invoke();
               
        if (onlyOpenCallBack) return;

        gameObject.SetActive(true);
    }
    
    public void Close()
    {
        closeCallBack?.Invoke();

        if (onlyCloseCallBack) return;
        else gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        addToCloseManager();
    }

    private void OnDisable()
    {
        openCallBack = null;
        closeCallBack = null;
        btnCallBack = null; //OK YES
        btnCallBack2 = null; // NO

        removeFromCloseManager();

        if (closeAndDestory)
        {
            popup_manager.GetInstance().popup_normal_window.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    void addToCloseManager()
    {
        //closeManager.GetInstance().lockClose(true); // 參數為是否開啟短暫阻擋後解鎖
        if (needLockClose) closeManager.GetInstance().AddMission(this.DoNothing);
        else closeManager.GetInstance().AddMission(this.Close_fromCloseManager);
    }

    /// <summary>
    /// 僅給予CloseManager使用，請不要亂call它
    /// needSE_fromCloseManager 可以設定使用右鍵關閉是否要音效
    /// </summary>
    public void Close_fromCloseManager()
    {
        if (needSE_fromCloseManager) AudioManager.PlaySE("close");
        Close();
    }

    void DoNothing(){}

    void removeFromCloseManager()
    {
        if (needLockClose) closeManager.GetInstance().RemoveMission(this.DoNothing);
        else closeManager.GetInstance().RemoveMission(this.Close_fromCloseManager);

    }
}
