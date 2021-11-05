using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMenu : MonoBehaviour
{

    public void openSave_Load(int type) // type 0 load 1 save
    {
        //popup_manager.GetInstance().show_save_load_window(type);
    }

    public void openSetting()
    {
        popup_manager.GetInstance().show_setting_window();
    }

    public void backMainMenu()
    {
        popup_manager.GetInstance().show_normal_window(
            TC_manager.GetInstance().GetTC_value("popup_gameBackMenuTitle"),
            TC_manager.GetInstance().GetTC_value("popup_gameBackMenuContent"),
            () =>
            {
                loadingManager.GetInstance().blockScreen(true);
                menu.instance.enterMenu();
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
