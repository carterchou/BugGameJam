using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoroutineHub : MonoBehaviour
{
    static CoroutineHub instance = null;

    public static CoroutineHub GetInstance()
    {
        if (instance == null)
        {
            GameObject temp = new GameObject("CoroutineHub");
            instance = temp.AddComponent<CoroutineHub>();
            DontDestroyOnLoad(instance);
        }
        return instance;
    }

    public bool IsRunning()
    {
        return gameObject != null && enabled;
    }

    void OnDisable()
    {
        //Debug.LogWarning("Disabling CoroutinHub might stop the execution of other coroutines. \nMake sure this is what you want.");
    }

    // 不需要吧 ?!
    //void OnDestroy () {
    //    instance = null;
    //}
}


public class CoroutineHubEx : MonoBehaviour
{
    public static string NamePrefix { get { return "_CorHub_"; } }
    public class SubHub : MonoBehaviour
    {
        public string HubName { get { return NamePrefix + name; } }

        public static SubHub CreateHub(string n)
        {
            SubHub res = null;
            GameObject temp = new GameObject(NamePrefix + n);
            res = temp.AddComponent<SubHub>();
            //res.coroutines = new List<IEnumerator>();
            DontDestroyOnLoad(temp);
            return res;
        }
        //public List<IEnumerator> coroutines = null;
    }

    static CoroutineHubEx instance = null;
    static Dictionary<string, SubHub> groupMap = null;
    public static CoroutineHubEx GetInstance()
    {
        if (instance == null)
        {
            GameObject temp = new GameObject("CoroutineHub");
            instance = temp.AddComponent<CoroutineHubEx>();
            DontDestroyOnLoad(instance);
            groupMap = new Dictionary<string, SubHub>();
        }
        return instance;
    }

    public bool IsRunning()
    {
        return gameObject != null && enabled;
    }

    void OnDisable()
    {
        //Debug.LogWarning("Disabling CoroutinHub might stop the execution of other coroutines. \nMake sure this is what you want.");
    }

    // 不需要吧 ?!
    //void OnDestroy () {
    //    instance = null;
    //}

    public Coroutine StartCoroutine(IEnumerator routine, string group = null)
    {
        if (string.IsNullOrEmpty(group))
        {
            return StartCoroutine(routine);
        }
        SubHub host = null;
        if (!groupMap.TryGetValue(group, out host))
        {
            // Create new group
            host = SubHub.CreateHub(group);
            groupMap.Add(group, host);
        }
        return host.StartCoroutine(routine);
    }

    public void StopCoroutine(IEnumerator routine, string group = null)
    {
        if (string.IsNullOrEmpty(group))
        {
            StopCoroutine(routine);
            return;
        }
        SubHub host = null;
        if (groupMap.TryGetValue(group, out host))
        {
            if (host != null)
            {
                host.StopCoroutine(routine);
            }
        }
        return;
    }

    public void StopCoroutineGroup(string group = null)
    {
        if (string.IsNullOrEmpty(group)) { return; }
        SubHub host = null;
        if (!groupMap.TryGetValue(group, out host)) { return; }
        if (host == null)
        {
            Debug.LogError("Removing bad coroutine group:" + group);
            groupMap.Remove(group);
        }
        host.StopAllCoroutines();
    }

    public SubHub GetCoroutineGroup(string group)
    {
        if (string.IsNullOrEmpty(group)) { return null; }
        SubHub res = null;
        if (!groupMap.TryGetValue(group, out res)) { return null; }
        return res;
    }
}