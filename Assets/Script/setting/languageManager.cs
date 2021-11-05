using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class languageManager : MonoBehaviour
{
    public static string nowLanguage;
    public static int Language_index; //0 zh-tw 1 en
    public static List<string> support_Language;
    public static List<string> support_Language_code;

    public static void initLanguage()
    {
        string LanguageSetting = PlayerPrefs.GetString("LanguageSetting", "");
        nowLanguage = "";

        support_Language = new List<string>();
        support_Language_code = new List<string>();

        support_Language.Add("繁體中文");
        support_Language.Add("English");

        support_Language_code.Add("zh-TW");
        support_Language_code.Add("en");

        if (LanguageSetting.Equals(""))
        {
            settingLanguage(System.Globalization.CultureInfo.CurrentCulture.Name);
        }
        else
        {  
            settingLanguage(LanguageSetting);
        }
       
    }

    public static void settingLanguage(string LanguageName)
    {
        if (nowLanguage == LanguageName) return;
        
        if (LanguageName.Equals("zh-TW"))
        {
            Debug.Log("[Set language] zh_tw");
            //flowChart_manager.Get_instance().Setting_localization("Zh_TW");
            //flowChart_manager.Get_instance().localization_controller.SetActiveLanguage("Zh_TW",true);
            PlayerPrefs.SetString("LanguageSetting", "zh-TW");
            Language_index = 0;
        }
        else if (LanguageName.Contains("en"))
        {
            Debug.Log("[Set language] English");
            //flowChart_manager.Get_instance().Setting_localization("EN");
            //flowChart_manager.Get_instance().localization_controller.SetActiveLanguage("EN", true);
            PlayerPrefs.SetString("LanguageSetting", "en");
            Language_index = 1;
        }
        else
        {
            Debug.Log("[Set language] Standard");
            //flowChart_manager.Get_instance().Setting_localization("Standard");
            //flowChart_manager.Get_instance().localization_controller.SetActiveLanguage("Standard", true);
            PlayerPrefs.SetString("LanguageSetting", "Standard");
            Language_index = 1;
        }

        nowLanguage = PlayerPrefs.GetString("LanguageSetting", "");
        event_manager.Broadcast(event_manager.EventType.change_language);
    }
}
