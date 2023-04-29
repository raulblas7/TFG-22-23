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
        if (currentValue <= 350.0f && currentValue >= (350.0f - exerciseAngle))
        {
            slider.value = exerciseAngle - (currentValue - (350.0f - exerciseAngle));
        }
        else if(currentValue < 350.0f - exerciseAngle)
        {
            slider.value = exerciseAngle;
        }
    }
}
