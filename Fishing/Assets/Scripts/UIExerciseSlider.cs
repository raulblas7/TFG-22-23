using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExerciseSlider : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] RectTransform _handleTransform;



    private int exerciseAngle;
    private int minAngle;


    void Start()
    {
        _handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 55.0f);
        exerciseAngle = GameManager.Instance.GetGameAngle();
        minAngle = GameManager.Instance.GetMinAngle();
        slider.maxValue = 1;
        slider.minValue = 0;

    }

    public void UpdateSlider(float currentValue)
    {

        //pasamos el valor actual(entre 0-180) a un rango 0-ejercicio
      //  Debug.Log("current value: " + currentValue);
        if (currentValue <= 90-minAngle && currentValue >= (90 - exerciseAngle))
        {
            //slider.value = (exerciseAngle - (currentValue - (90 - exerciseAngle)));
            //(valor_original - (180 - y)) / (y - x)
            slider.value = 1-( (currentValue - (90 - exerciseAngle)) / (exerciseAngle - minAngle));
        }
        else if (currentValue < (90 - exerciseAngle))
        {
            slider.value = 1;
           // Debug.Log("angulo menor de 40 = : " + maxSlider.value);
        }
        else if (currentValue > 90 - minAngle)
        {
            slider.value = 0;
        }

    }

    //original
    //public void UpdateSlider(float currentValue)
    //{

    //    if (currentValue >= 270 && currentValue <= (270 + exerciseAngle))
    //    {
    //        maxSlider.value = /*exerciseAngle -*/ (currentValue - 270);

    //    }
    //    else if (currentValue > (270 + exerciseAngle))
    //    {
    //        maxSlider.value = exerciseAngle;
    //    }

    //}
}
