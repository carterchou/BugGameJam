using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Encoders;

public class save_controller : MonoBehaviour
{
    public GameObject itemsRoot;
    public GameObject[] pageBtn;
    public Text title;
    private List<save_item> save_Items;

    int nowPage = 0; //從0開始
    bool isDelete = false;


    public void init(int type)
    {
        loadingManager.GetInstance().blockScreen();
        closeManager.GetInstance().save_Controller = this;
        CoroutineHub.GetInstance().StartCoroutine(init_(type));
    }

    IEnumerator init_(int type)
    {
        switch (type)
        {
            case 0: //載入
                title.text = TC_manager.GetInstance().GetTC_value("saveLoad_loadTitle");
                break;
            case 1: //存檔
                title.text = TC_manager.GetInstance().GetTC_value("saveLoad_saveTitle");
                break;
        }

        loadingManager.GetInstance().StartLoading();
        loadingManager.GetInstance().closeblockScreen();

        if (save_Items == null)
        {
            save_Items = new List<save_item>();

            for (int i = 0; i < itemsRoot.transform.childCount; i++)
            {
                save_item tmp = itemsRoot.transform.GetChild(i).GetComponent<save_item>();
                if (tmp != null) save_Items.Add(tmp);
                bool isinit = true;
                tmp.init(i + 1, type, this, (hasData)=> { isinit = false; });
                yield return new WaitWhile(() => isinit);
            }
        }
        else
        {
            for (int i = 0; i < save_Items.Count; i++)
            {
                bool isinit = true;
                save_Items[i].init(i + 1, type, this, (hasData) => { isinit = false; });
                yield return new WaitWhile(() => isinit);
            }

            for (int i = 0; i < save_Items.Count; i++)
            {
                save_Items[i].closeDelete();
            }
            isDelete = false;
        }

        showPage(0);

        pop_item pop_Item = GetComponent<pop_item>();
        pop_Item.popup();
        loadingManager.GetInstance().DoneLoading();
    }

    private void OnEnable()
    {
        scroll_Manager.GetInstance().AddMission(this.NextPage, this.lastPage);
    }

    private void OnDisable()
    {
        scroll_Manager.GetInstance().RemoveMission(this.NextPage, this.lastPage);

        if (isDelete) closeManager.GetInstance().RemoveMission(this.endDelete);
    }

    public void showPage(int index)
    {
        nowPage = index;
        int startIndex = nowPage * 6;
        int endIndex = nowPage * 6 + 5;

        for (int i = 0; i < save_Items.Count; i++)
        {
            save_Items[i].show_close(startIndex <= i && i <= endIndex);
        }

        for (int i = 0; i < pageBtn.Length; i++)
        {
            if (i == index) pageBtn[i].GetComponent<Image>().color = new Color(255.0f/255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 0.5f);
            else pageBtn[i].GetComponent<Image>().color = new Color(180.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f, 0.5f);
        }
    }

    public void lastNextPage(bool isNext)
    {
        int index = nowPage;
        if (isNext)
        {
            if (index == 7) index = 0;
            else index++;
        }
        else
        {
            if (index == 0) index = 7;
            else index--;
        }
        showPage(index);
    }

    /// <summary>
    /// for scroll manager
    /// </summary>
    void NextPage()
    {
        AudioManager.PlaySE("_ecision3");
        lastNextPage(true);
    }

    /// <summary>
    /// for scroll manager
    /// </summary>
    void lastPage()
    {
        AudioManager.PlaySE("_ecision3");
        lastNextPage(false);
    }

    public void pressDelete()
    {
        if (isDelete) endDelete();
        else startDelete(); 
    }

    void startDelete()
    {
        isDelete = true;
        for (int i = 0; i < save_Items.Count; i++)
        {
            save_Items[i].initDelete();
        }

        AudioManager.PlaySE("open");
        closeManager.GetInstance().AddMission(this.endDelete);
    }

    void endDelete()
    {        
        AudioManager.PlaySE("close");
        for (int i = 0; i < save_Items.Count; i++)
        {
            save_Items[i].closeDelete();
        }
        isDelete = false;

        
        closeManager.GetInstance().RemoveMission(this.endDelete);
    }
}
