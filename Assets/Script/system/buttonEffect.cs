using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonEffect : MonoBehaviour
{
    public Action callback;

    public void press_SE()
    {
        AudioManager.PlaySE("_ecision3");
    }
   
    public void close_SE()
    {
        AudioManager.PlaySE("close");
    }
}
