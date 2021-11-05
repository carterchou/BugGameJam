using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setting_volume : MonoBehaviour
{
    public Slider slider;
    public InputField value_text;

    public void updateText()
    {
        value_text.text = Math.Round(slider.value * 100).ToString();
    }

    public void updateSlider()
    {
        try
        {
            slider.value = Convert.ToInt32(value_text.text) / 100.0f;
        }
        catch (Exception e)
        {

        }
    }
}
