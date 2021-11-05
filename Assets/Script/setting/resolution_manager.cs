using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resolution_manager : MonoBehaviour
{
    public static List<Resolution> support_Resolutions;
    public static int now_resolution_w;
    public static int now_resolution_h;
    public static bool IsFullScreen;

    public static void initResolution()
    {
        now_resolution_w = PlayerPrefs.GetInt("resolution_w", -1);
        now_resolution_h = PlayerPrefs.GetInt("resolution_h", -1);
        IsFullScreen = PlayerPrefs.GetInt("IsFullScreen", 1) == 1;

        Resolution[] resolutions = Screen.resolutions;
        support_Resolutions = new List<Resolution>();
        foreach (var res in resolutions)  support_Resolutions.Add(res);

        if (now_resolution_w == -1 || now_resolution_h == -1)
        {
            settingResolution(Screen.width, Screen.height, true);
        }
        else
        {
            settingResolution(now_resolution_w, now_resolution_h, IsFullScreen);
        }
    }

    public static void settingResolution(int resolution_w, int resolution_h, bool fullScreen)
    {
        Debug.Log(string.Format("[Setting resolution] - {0}x{1} isFullScreen:{2}", resolution_w, resolution_h, fullScreen));
        Screen.SetResolution(resolution_w, resolution_h, fullScreen);
        PlayerPrefs.SetInt("resolution_w", resolution_w);
        PlayerPrefs.SetInt("resolution_h", resolution_h);
        PlayerPrefs.SetInt("IsFullScreen", fullScreen? 1 : 0);
        now_resolution_w = resolution_w;
        now_resolution_h = resolution_h;
        IsFullScreen = fullScreen;
    }
}
