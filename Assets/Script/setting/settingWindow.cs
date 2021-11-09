using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingWindow : MonoBehaviour
{
    public Slider bgm_volume;
    public Slider sound_volume;
    public Slider se_volume;
    public Dropdown language;
    public Dropdown resolution;
    public Toggle fullscreen;

    public void init()
    {
        loadingManager.GetInstance().StartLoading();
        closeManager.GetInstance().settingWindow = this;
        CoroutineHub.GetInstance().StartCoroutine(init_());
    }

    IEnumerator init_()
    {
        yield return 0;

        bgm_volume.value = PlayerPrefs.GetFloat("BGMVolume", 1);
		sound_volume.value = PlayerPrefs.GetFloat("SoundVolume", 1);
        se_volume.value = PlayerPrefs.GetFloat("SEVolume", 1);
		

        resolution.ClearOptions();

        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        int nowChoice = 0;
        for(int i = 0; i < resolution_manager.support_Resolutions.Count; i++)
        {
            if (resolution_manager.support_Resolutions[i].width == resolution_manager.now_resolution_w
                && resolution_manager.support_Resolutions[i].height == resolution_manager.now_resolution_h) nowChoice = i;
            string resText = string.Format("{0}x{1}", resolution_manager.support_Resolutions[i].width, resolution_manager.support_Resolutions[i].height);
            optionDatas.Add(new Dropdown.OptionData(resText));
        }
        resolution.AddOptions(optionDatas);
        resolution.value = nowChoice;

        language.ClearOptions();
        optionDatas = new List<Dropdown.OptionData>();
        nowChoice = languageManager.Language_index;
        for (int i = 0; i < languageManager.support_Language.Count; i++) optionDatas.Add(new Dropdown.OptionData(languageManager.support_Language[i]));
        language.AddOptions(optionDatas);
        language.value = nowChoice;

        fullscreen.isOn = resolution_manager.IsFullScreen;

        pop_item pop_Item = GetComponent<pop_item>();
        pop_Item.Setting_CB(null, close);
        pop_Item.Setting_onlyCB(false, true);
        pop_Item.Setting_needSE_fromCloseManager(false);
        pop_Item.popup();
        loadingManager.GetInstance().DoneLoading();
    }

    public void pressSave()
    {
        popup_manager.GetInstance().show_normal_window(TC_manager.GetInstance().GetTC_value("setting_saveSettingTitle"),
            TC_manager.GetInstance().GetTC_value("setting_saveSettingContent"),
            save_setting,
            null,
            true,
            () =>
            {
                AudioManager.PlaySE("open");
            }
        );
    }

    public void close()
    {
        popup_manager.GetInstance().show_normal_window(
            TC_manager.GetInstance().GetTC_value("setting_closeSettingTitle"),
            TC_manager.GetInstance().GetTC_value("setting_closeSettingContent"),
            () =>
            {
                gameObject.SetActive(false);
            },
            null
            ,
            true,
            () =>
            {
                AudioManager.PlaySE("open");
            }
        );
    }

    public void save_setting()
    {
        AudioManager.SetBGMVolume(bgm_volume.value);
        AudioManager.SetSEVolume(se_volume.value);
        AudioManager.SetSoundVolume(sound_volume.value);

        int i = resolution.value;
        resolution_manager.settingResolution(resolution_manager.support_Resolutions[i].width, resolution_manager.support_Resolutions[i].height, fullscreen.isOn);
        languageManager.settingLanguage(languageManager.support_Language_code[language.value]);

        gameObject.SetActive(false);
    }
}
