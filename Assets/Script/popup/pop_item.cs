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

	[Header("Yes / OK Type")]
    public GameObject btn1;
	public Text btn1_label;
	[Header("No / Cancel Type")]
	public GameObject btn2;
	public Text btn2_label;

	public enum btnType{
        ok,
        yes_no,
        other
    }
	public enum popType
	{
		normal,
		warning
	}

	btnType btn_Type = btnType.other;
	popType pop_Type = popType.normal;
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
		btn_Type = type;

        switch (type)
        {
            case btnType.ok:
                needLockClose = true;

				if(btn1 != null) {
					if (btn1.activeSelf == false) {
						btn1.SetActive(true);
					}
				}
				if (btn2 != null) {
					if (btn2.activeSelf == true) {
						btn2.SetActive(false);
					}
				}

				break;
            case btnType.yes_no:
				if (btn1 != null) {
					if (btn1.activeSelf == false) {
						btn1.SetActive(true);
					}
				}
				if (btn2 != null) {
					if (btn2.activeSelf == false) {
						btn2.SetActive(true);
					}
				}
				break;
        }
    }

	public void setting_btnLable(string label1 = "", string label2 = "") {
		if (string.IsNullOrEmpty(label1)) {
			switch (btn_Type) {
				case btnType.ok:
					label1 = TC_manager.GetInstance().GetTC_value("button_ok");
					break;
				case btnType.yes_no:
					label1 = TC_manager.GetInstance().GetTC_value("button_yes");
					break;
			}			
		}
		if (string.IsNullOrEmpty(label2)) {
			switch (btn_Type) {
				case btnType.yes_no:
					label2 = TC_manager.GetInstance().GetTC_value("button_no");
					break;
			}
		}

		if(btn1_label != null) {
			btn1_label.text = label1;
			btn1_label.text = btn1_label.text.Replace("\\n", "\n");
		}
		if (btn2_label != null) {
			btn2_label.text = label2;
			btn2_label.text = btn2_label.text.Replace("\\n", "\n");
		}
	}
	public void setting_popType(popType type = popType.normal) {
		pop_Type = type;
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

		if (onlyOpenCallBack) {
			return;
		}
        gameObject.SetActive(true);

		switch (pop_Type) {
			case popType.normal:
				AudioManager.PlaySE("open");
				break;
			case popType.warning:
				AudioManager.PlaySE("warning");
				break;
		}
    }
    
    public void Close()
    {
        closeCallBack?.Invoke();

		if (onlyCloseCallBack) {
			return;
		} else {
			gameObject.SetActive(false);
		}
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
		if (needLockClose) {
			closeManager.GetInstance().AddMission(this.DoNothing);
		}
		else {
			closeManager.GetInstance().AddMission(this.Close_fromCloseManager);
		}
    }

    /// <summary>
    /// 僅給予CloseManager使用，請不要亂call它
    /// needSE_fromCloseManager 可以設定使用右鍵關閉是否要音效
    /// </summary>
    public void Close_fromCloseManager()
    {
		if (needSE_fromCloseManager) {
			AudioManager.PlaySE("cancel");
		}
        Close();
    }

    void DoNothing(){}

    void removeFromCloseManager()
    {
		if (needLockClose) { closeManager.GetInstance().RemoveMission(this.DoNothing);
		} else {
			closeManager.GetInstance().RemoveMission(this.Close_fromCloseManager);
		}
    }

	public btnType GetBtnType() {
		return btn_Type;
	}
}
