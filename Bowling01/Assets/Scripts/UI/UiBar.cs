using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] RectTransform _handleTransform;
    [SerializeField] RectTransform _sliderTransform;

    private int maxValue;
    private int minValue;
    private float currentValue;

    void Start()
    {
        int difficulty = GameManager.Instance.GetDifficulty();
        switch (difficulty)
        {
            case 0:
                ChangesliderSize(3.6f);
                break;
            case 1:
                ChangesliderSize(3.0f);
                break;
            case 2:
                ChangesliderSize(2.1f);
                break;
            case 3:
                ChangesliderSize(1.5f);
                break;
            case 4:
                ChangesliderSize(1.1f);
                break;

        }
        _handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 42.66667f);
        maxValue = GameManager.Instance.GetGameAngle();
        minValue = GameManager.Instance.GetGameAngle() * -1;
    }

    void Update()
    {
        currentValue = GameManager.Instance.GetAngle();
        float aux = (float)(currentValue - minValue) / (float)(maxValue - minValue);
        slider.value = aux;



    }

    private void ChangesliderSize(float  size)
    {
        _sliderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / size);
    }
}
