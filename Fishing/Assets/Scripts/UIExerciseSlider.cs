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
        exerciseAngle = GameManager.Instance.GetGameAngle();
        slider.maxValue = exerciseAngle;

    }

    public void UpdateSlider(float currentValue)
    {

        //pasamos el valor actual(entre 0-180) a un rango 0-ejercicio
      //  Debug.Log("current value: " + currentValue);
        if (currentValue <= 90 && currentValue >= (90 - exerciseAngle))
        {
            slider.value = (exerciseAngle - (currentValue - (90 - exerciseAngle)));
           // Debug.Log("angulo = : " + slider.value );
        }
        else if (currentValue < (90 - exerciseAngle))
        {
            slider.value = exerciseAngle;
           // Debug.Log("angulo menor de 40 = : " + slider.value);
        }

    }

    //original
    //public void UpdateSlider(float currentValue)
    //{

    //    if (currentValue >= 270 && currentValue <= (270 + exerciseAngle))
    //    {
    //        slider.value = /*exerciseAngle -*/ (currentValue - 270);

    //    }
    //    else if (currentValue > (270 + exerciseAngle))
    //    {
    //        slider.value = exerciseAngle;
    //    }

    //}
}
