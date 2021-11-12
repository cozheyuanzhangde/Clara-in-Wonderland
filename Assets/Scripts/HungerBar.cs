using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider slider;

    public void SetHunger(float current_hunger)
    {
        slider.value = current_hunger;
    }

    public void SetMaxHunger(float max_hunger)
    {
        slider.maxValue = max_hunger;
        slider.value = max_hunger;
    }
}
