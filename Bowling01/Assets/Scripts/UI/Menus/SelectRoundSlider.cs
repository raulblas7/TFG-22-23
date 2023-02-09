using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectRoundSlider : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] RectTransform _handleTransform;
    
    void Start()
    {
        UpdateText(gameObject.GetComponent<Slider>().value);
        _handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 42.66667f);
    }

    public void UpdateText(float value)
    {
        text.text = ((int)value).ToString();
    }
}
