using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider bar;
    public Gradient gradient;
    public Image fill;

    public void SetMaxValue(int value)
    {
        bar.maxValue = value;
        bar.value = value;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetValue(int value)
    {
        bar.value = value;

        fill.color = gradient.Evaluate(bar.normalizedValue);
        if(value == 0)
        {
            fill.color = Color.clear;
        }
    }
}
