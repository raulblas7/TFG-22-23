using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExerciseSlider : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] RectTransform _handleTransform;
    [SerializeField] private BarColor barColor;
    [SerializeField] private Image fillArea;

    private int exerciseAngle;
    private int exerciseAngleMin;

    void Start()
    {
        _handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 55.0f);
        exerciseAngle = (int)GameManager.Instance.GetAngleToDoIt();
        exerciseAngleMin = (int)GameManager.Instance.GetAngleMinToDoIt();
        //slider.maxValue = exerciseAngle;
    }

    public void UpdateSlider(float currentValue, Movement state)
    {
        if (currentValue >= 270 + exerciseAngleMin && currentValue <= (270 + exerciseAngle))
        {
            slider.value = (currentValue - (270 + exerciseAngleMin)) / (exerciseAngle - exerciseAngleMin);
        }
        else if(currentValue > (270 + exerciseAngle))
        {
            slider.value = 1;
        }
        else if(currentValue < 270 + exerciseAngleMin)
        {
            slider.value = 0;
        }

        //comprobamos el estado-------------------
        if (state == Movement.RESTART)
        {
            fillArea.color = barColor.readyToPlay;
        }
        else fillArea.color = barColor.returnToIniPos;
    }
}
