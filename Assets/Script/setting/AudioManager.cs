using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static string BGM_Name;
    static string BGSE_Name;
    static string Voice_Name;

    static float se_volume;

    static public void PlayBGM(string name, bool needLoop = true)
    {
        if (BGM_Name == name) return;

        AudioClip targetAC = Resources.Load<AudioClip>("audio/BGM/" + name);

        if (targetAC == null)
        {
            Debug.LogError("PlayBGM Missing AudioClip : " + name);
            return;
        }

        //Fungus.FungusManager.Instance.MusicManager.PlayMusic(targetAC, needLoop, 0.5f, 0);
        BGM_Name = name;
    }

    static public void StopBGM()
    {
        //Fungus.FungusManager.Instance.MusicManager.StopMusic();
        BGM_Name = string.Empty;
    }

    static public void PauseBGM()
    {
        //Fungus.FungusManager.Instance.MusicManager.PauceMusic();
    }

    static public void ResumeBGM()
    {
        //Fungus.FungusManager.Instance.MusicManager.resumeMusic();
    }

    static public void PlayBGSE(string name, bool needLoop = false)
    {
        //if (BGSE_Name == name) return;

        AudioClip targetAC = Resources.Load<AudioClip>("audio/SE/" + name);

        if (targetAC == null)
        {
            Debug.LogError("PlayBGSE Missing AudioClip : " + name);
            return;
        }

        //Fungus.FungusManager.Instance.MusicManager.PlayAmbianceSound(targetAC, needLoop, 1);
        //BGSE_Name = name;
    }

    static public void PauseBGSE()
    {
        //Fungus.FungusManager.Instance.MusicManager.PauceAmbiance();
    }

    static public void ResumeBGSE()
    {
        //Fungus.FungusManager.Instance.MusicManager.resumeAmbiance();
    }

    static public void PlayVoice(string name, bool needLoop = false)
    {
        //if (Voice_Name == name) return;

        AudioClip targetAC = Resources.Load<AudioClip>("audio/voice/" + name);

        if (targetAC == null)
        {
            Debug.LogError("PlayVoice Missing AudioClip : " + name);
            return;
        }

        //Fungus.FungusManager.Instance.MusicManager.PlayVoiceSound(targetAC, needLoop, 1);
        //Voice_Name = name;
    }

    static public void PauseVoice()
    {
        //Fungus.FungusManager.Instance.MusicManager.PauceVoice();
    }

    static public void ResumeVoice()
    {
        //Fungus.FungusManager.Instance.MusicManager.resumeVoice();
    }

    //無法終止，可多個疊加撥放
    static public void PlaySE(string name)
    {
        //if (BGSE_Name == name) return;

        AudioClip targetAC = Resources.Load<AudioClip>("audio/SE/" + name);

        if (targetAC == null)
        {
            Debug.LogError("PlayBGSE Missing AudioClip : " + name);
            return;
        }

        //Fungus.FungusManager.Instance.MusicManager.PlaySound(targetAC, se_volume);
        //BGSE_Name = name;
    }

    static public void SetBGMVolume(float value)
    {
        //Fungus.FungusManager.Instance.MusicManager.SetAudioVolume(value, 0.1f, null);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    static public void SetVoiceVolume(float value)
    {
        //Fungus.FungusManager.Instance.MusicManager.SetAudioVoiceVolume(value, 0.1f, null);
        PlayerPrefs.SetFloat("VoiceVolume", value);
    }

    static public void SetBGSEVolume(float value)
    {
        //Fungus.FungusManager.Instance.MusicManager.SetAudioAmbianceVolume(value, 0.1f, null);
        PlayerPrefs.SetFloat("BGSEVolume", value);
    }

    static public void SetSEVolume(float value)
    {
        se_volume = value;
        PlayerPrefs.SetFloat("SEVolume", value);
    }

    static public void StopVoice()
    {
        //Fungus.FungusManager.Instance.MusicManager.StopVoice();
    }

    static public void StopBGSE()
    {
        //Fungus.FungusManager.Instance.MusicManager.StopAmbiance();
    }

    static public void initSetting()
    {
        SetBGMVolume(PlayerPrefs.GetFloat("BGMVolume", 1));
        SetSEVolume(PlayerPrefs.GetFloat("SEVolume", 1));
        SetVoiceVolume(PlayerPrefs.GetFloat("VoiceVolume", 1));
        SetBGSEVolume(PlayerPrefs.GetFloat("BGSEVolume", 1));
    }
}
