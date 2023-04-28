using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExerciseSlider : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] RectTransform _handleTransform;

    private int exerciseAngle;

    void Start()
    {
        _handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 55.0f);
        exerciseAngle = (int)GameManager.Instance.GetAngleToDoIt();
        slider.maxValue = exerciseAngle;
    }

    public void UpdateSlider(float currentValue)
    {
        currentValue = ((currentValue - 270.0f) / 90.0f) * 180.0f;
        currentValue = 180.0f - currentValue;
        Debug.Log("Currentvalue es: " + currentValue);
        //pasamos el valor actual(entre 0-180) a un rango 0-ejercicio
        if (currentValue <= 180 && currentValue >= (180 - exerciseAngle))
        {
            slider.value = (exerciseAngle - (currentValue - (180 - exerciseAngle)));
        }
        else if(currentValue < 180 - exerciseAngle)
        {
            slider.value = exerciseAngle;
        }
    }
}
