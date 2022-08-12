using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    //public Image circle;

    public void SetMaxTime(float time)
    {
        slider.maxValue = time;
        slider.value = time;
        //circle.color = gradient.Evaluate(1f);
        gradient.Evaluate(1f);
    }

    public void SetTime(float time)
    {
        slider.value = time;

        fill.color = gradient.Evaluate(slider.normalizedValue);

       /* if (slider.normalizedValue < 0.1f) 
        { 
            circle.color = gradient.Evaluate(slider.normalizedValue);
        }*/
    }
}
