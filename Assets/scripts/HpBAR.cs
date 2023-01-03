using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBAR : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(int currHP)
    {
        slider.value = currHP;
    }

    public void SetMaxHP(int HP)
    {
        slider.maxValue = HP;
        slider.value = HP;
    }
}
