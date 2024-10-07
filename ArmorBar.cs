using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour
{

    public Slider slider;
    public void SetMaxHealth(int armor)
    {
        slider.maxValue = armor;
        slider.value = armor;
    }
    public void SetHealth(int armor)
    {
        slider.value = armor;

    }
}
