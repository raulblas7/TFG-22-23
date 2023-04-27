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
      
        if (currentValue >= 270 && currentValue <= (270 + exerciseAngle))
        {
            slider.value = /*exerciseAngle -*/ (currentValue - 270);

        }

    }
}
