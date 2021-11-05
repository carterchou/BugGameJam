using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TC_quickTake : MonoBehaviour
{
    public string key;

    private void OnEnable()
    {
        update_text();
        event_manager.AddListener(event_manager.EventType.change_language,update_text);
    }

    void update_text()
    {
        Text text = GetComponent<Text>();
        if (text != null) text.text = TC_manager.GetInstance().GetTC_value(key);
    }

    void OnDestory()
    {
        event_manager.RemoveListener(event_manager.EventType.change_language, update_text);
    }
}
