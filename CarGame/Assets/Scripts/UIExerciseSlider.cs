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
    }

    public void UpdateSlider(float currentValue, Movement state)
    {
        if (currentValue <= 350.0f - exerciseAngleMin && currentValue >= (350.0f - exerciseAngle))
        {
            //slider.value = exerciseAngle - (currentValue - (350.0f - exerciseAngle));
            slider.value = 1 - (currentValue - (350.0f - exerciseAngle)) / (exerciseAngle - exerciseAngleMin);
        }
        else if(currentValue < 350.0f - exerciseAngle)
        {
            slider.value = 1;
        }
        else if(currentValue > 350.0f - exerciseAngleMin)
        {
            slider.value = 0;
        }

        //comprobamos el estado---------------
        if (state == Movement.RESTART)
        {
            fillArea.color = barColor.readyToPlay;
        }
        else fillArea.color = barColor.returnToIniPos;
    }
}
