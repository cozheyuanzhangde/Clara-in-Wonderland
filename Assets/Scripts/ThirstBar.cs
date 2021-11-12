using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirstBar : MonoBehaviour
{
    public Slider slider;

    public void SetThirst(float current_thirst)
    {
        slider.value = current_thirst;
    }

    public void SetMaxThirst(float max_thirst)
    {
        slider.maxValue = max_thirst;
        slider.value = max_thirst;
    }
}
