using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SliderConfig : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] RectTransform _handleTransform;
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText(slider.value);
        _handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 30.0f);
    }

    public void UpdateText(float value)
    {
        text.text = ((int)value).ToString();
    }

    public void SetValue(float value)
    {
        slider.value = value;
        UpdateText(value);
    }
}
