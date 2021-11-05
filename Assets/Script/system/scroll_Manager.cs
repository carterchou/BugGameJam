using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll_Manager : MonoBehaviour
{
    public List<Action> scrollNextActions;
    public List<Action> scrollLastActions;

    static scroll_Manager instance;

    int lockScroll;

    public static scroll_Manager GetInstance()
    {
        if (instance == null)
        {
            GameObject temp = new GameObject("scroll_Manager");
            instance = temp.AddComponent<scroll_Manager>();
            instance.scrollNextActions = new List<Action>();
            instance.scrollLastActions = new List<Action>();
            instance.lockScroll = 0;
            DontDestroyOnLoad(instance);
        }

        return instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockScroll > 0 || scrollNextActions.Count <= 0 || scrollLastActions.Count <= 0) return;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            pauseScroll(true, 0.05f);
            scrollNextActions[scrollNextActions.Count - 1]?.Invoke();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            pauseScroll(true, 0.05f);
            scrollLastActions[scrollLastActions.Count - 1]?.Invoke();
        }
    }

    public void pauseScroll(bool blockAwhile = false, float time = 0.25f)
    {
        lockScroll++;
        if (blockAwhile) Invoke("UnpauseScroll", time);
    }

    public void UnpauseScroll()
    {
        lockScroll--;
        if (lockScroll < 0) lockScroll = 0;
    }

    public void AddMission(Action NextAction, Action LastAction)
    {
        pauseScroll(true);
        if (scrollNextActions == null) scrollNextActions = new List<Action>();
        if (scrollLastActions == null) scrollLastActions = new List<Action>();
        scrollNextActions.Add(NextAction);
        scrollLastActions.Add(LastAction);
    }

    public void RemoveMission(Action NextAction, Action LastAction)
    {
        if (scrollNextActions != null) scrollNextActions.Remove(NextAction);
        if (scrollLastActions != null) scrollLastActions.Remove(LastAction);
    }
}
