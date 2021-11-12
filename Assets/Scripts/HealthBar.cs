using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(float current_health)
    {
        slider.value = current_health;
    }

    public void SetMaxHealth(float max_health)
    {
        slider.maxValue = max_health;
        slider.value = max_health;
    }
}
