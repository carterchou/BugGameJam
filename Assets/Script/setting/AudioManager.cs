using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static string BGM_Name;
    static string SFX_Name;

	static float se_volume;

	static public void PlayBGM(string name, bool needLoop = true, float fadeInSec = 0.5f, float fadeOut = 0.5f, float currentFadeOut = 0.5f, bool forcePlay = false)
    {
		if (BGM_Name == name && forcePlay == false) {
			return;
		}

        AudioClip targetAC = Resources.Load<AudioClip>("audio/BGM/" + name);

        if (targetAC == null)
        {
            Debug.LogError("PlayBGM Missing AudioClip : " + name);
            return;
        }

		Fungus.FungusManager.Instance.MusicManager.PlayMusic(targetAC, needLoop, 0.5f, 0);
		BGM_Name = name;
    }

    static public void StopBGM()
    {
		Fungus.FungusManager.Instance.MusicManager.StopMusic();
		BGM_Name = string.Empty;
    }

    static public void PauseBGM()
    {
		//Fungus.FungusManager.Instance.MusicManager.();
	}

    static public void ResumeBGM()
    {

		//Fungus.FungusManager.Instance.MusicManager.resumeMusic();
	}

	/// <summary>
	/// 可多個疊加撥放
	/// </summary>
	/// <param name="name"></param>
	static public void PlaySound(string name) {
		AudioClip targetAC = Resources.Load<AudioClip>("audio/Sound/" + name);

		if (targetAC == null) {
			Debug.LogError("PlaySound Missing AudioClip : " + name);
			return;
		}

		//Hellmade.Sound.EazySoundManager.PlaySound(targetAC, Hellmade.Sound.EazySoundManager.GlobalSoundsVolume);
	}

	/// <summary>
	/// 可多個疊加撥放
	/// </summary>
	/// <param name="name"></param>
	static public void PlaySE(string name)
    {
        AudioClip targetAC = Resources.Load<AudioClip>("audio/SE/" + name);

        if (targetAC == null)
        {
            Debug.LogError("PlaySE Missing AudioClip : " + name);
            return;
        }

		//Hellmade.Sound.EazySoundManager.PlayUISound(targetAC, Hellmade.Sound.EazySoundManager.GlobalUISoundsVolume);
	}

    static public void SetBGMVolume(float value)
    {
		//Hellmade.Sound.EazySoundManager.GlobalMusicVolume = value;
		PlayerPrefs.SetFloat("BGMVolume", value);
    }

    static public void SetSoundVolume(float value)
    {
		//Hellmade.Sound.EazySoundManager.GlobalSoundsVolume = value;
		PlayerPrefs.SetFloat("SoundVolume", value);
    }

    static public void SetSEVolume(float value)
    {
		//Hellmade.Sound.EazySoundManager.GlobalUISoundsVolume = value;
        PlayerPrefs.SetFloat("SEVolume", value);
    }

    static public void StopSound()
    {
		//Hellmade.Sound.EazySoundManager.StopAllSounds();
	}

    static public void StopSFX()
    {
		//Hellmade.Sound.EazySoundManager.StopAllUISounds();
	}

    static public void initSetting()
    {
        SetBGMVolume(PlayerPrefs.GetFloat("BGMVolume", 1));
        SetSEVolume(PlayerPrefs.GetFloat("SEVolume", 1));
		SetSoundVolume(PlayerPrefs.GetFloat("SoundVolume", 1));
    }
}
