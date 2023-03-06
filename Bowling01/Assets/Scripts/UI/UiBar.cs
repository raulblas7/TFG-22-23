using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] RectTransform _handleTransform;

    private int maxValue;
    private int minValue;
    private float currentValue;

    void Start()
    {
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
}
