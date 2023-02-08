using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] int MaxValue;
    [SerializeField] int MinValue;
    [SerializeField] RectTransform _handleTransform;

    private float currentValue;





    void Start()
    {
        _handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 42.66667f);


    }

 
    void Update()
    {
        currentValue = GameManager.Instance.getAngle();
        float aux = (float)(currentValue - MinValue) / (float)(MaxValue - MinValue);
        slider.value = aux;



    }
}
