using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider = null;

    [SerializeField] private Image heart0 = null;
    [SerializeField] private Image heart1 = null;
    [SerializeField] private Image heart2 = null;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void ChangeTries(int tries)
    {
        switch (tries)
        {
            case 0:
                heart0.enabled = false;
                heart1.enabled = false;
                heart2.enabled = false;
                break;
            case 1:
                heart0.enabled = true;
                heart1.enabled = false;
                heart2.enabled = false;
                break;
            case 2:
                heart0.enabled = true;
                heart1.enabled = true;
                heart2.enabled = false;
                break;
            case 3:
                heart0.enabled = true;
                heart1.enabled = true;
                heart2.enabled = true;
                break;
        }
    }
}
