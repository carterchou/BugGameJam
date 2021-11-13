using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pop_nameEditor : MonoBehaviour
{
    public InputField input_name;

    private void OnEnable()
    {
        input_name.text = "你";
    }

    public void press()
    {
        if (checkName())
        {
            popup_manager.GetInstance().show_normal_window(
                TC_manager.GetInstance().GetTC_value("popup_nameEditorCheckTitle"),
                TC_manager.GetInstance().GetTC_value("popup_nameEditorCheckContent").Replace("{$var}", input_name.text),
                () =>
                {
                    modifyName();
                },
                null,
                true,
                () =>
                {
                    AudioManager.PlaySE("open");
                });
        }
    }

    void modifyName()
    {
		//userData.instance.GetUserData().name = input_name.text;
        //flowChart_manager.Get_instance().flowcharts[0].SetStringVariable("playerName", userData.instance.GetUserData().name);
        //flowChart_manager.Get_instance().flowcharts[(int)nextStroyType].SendFungusMessage(nextBlock);
        //flowChart_manager.Get_instance().flowcharts[0].SetBooleanVariable("isWaiting", false);
        GetComponent<pop_item>().Close();
    }

    bool checkName()
    {
        if (input_name.text.Equals(""))
        {
            popup_manager.GetInstance().show_normal_window(
                TC_manager.GetInstance().GetTC_value("popup_nameEditorNoneTitle"),
                TC_manager.GetInstance().GetTC_value("popup_nameEditorNoneContent"),
                () =>
                {
                },
                true,
                () =>
                {
                    AudioManager.PlaySE("open");
                });

            return false;
        }
        return true;
    }
}
