using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setMainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        setCamera();
        event_manager.AddListener(event_manager.EventType.change_scene ,setCamera);
    }

    private void OnDestroy()
    {
        event_manager.RemoveListener(event_manager.EventType.change_scene, setCamera);
    }

    public void setCamera()
    {
        Canvas targetCanvas = GetComponent<Canvas>();
        targetCanvas.worldCamera = Camera.main;
    }
}
