using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    public static menu instance;
    public Transform mainLayer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        instance.enterMenu();
        event_manager.Broadcast(event_manager.EventType.change_scene);
    }

    public void enterMenu()
    {
        AudioManager.PlayBGM("norowaretapiano");
        closeManager.GetInstance().closeAllWindows();
        loadingManager.GetInstance().closeblockScreen();

        loadingManager.GetInstance().initLoad();
        closeManager.GetInstance().initClose();

        /*pop_item item = gameObject.AddComponent<pop_item>();
        item.Setting_CB(null, Close);
        item.Setting_onlyCB(true, true);
        item.Setting_needSE_fromCloseManager(false);*/
        closeManager.GetInstance().AddMission(this.Close);
    }

    public void Close() //若在menu按下右鍵，詢問是否離開遊戲
    {
        
        popup_manager.GetInstance().show_normal_window(TC_manager.GetInstance().GetTC_value("popup_exitTitle"), 
            TC_manager.GetInstance().GetTC_value("popup_exitContent"),
            () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            },
            null,
            true,
            () =>
            {
                AudioManager.PlaySE("open");
            }           
        );


    }

    public void Setting()
    {
        popup_manager.GetInstance().show_setting_window();
    }

    public void start_NewGame()
    {

    }

    public void load()
    {
        popup_manager.GetInstance().show_save_load_window(0);
    }

}
