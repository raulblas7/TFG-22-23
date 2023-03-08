using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectDifficulty : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] RectTransform _handleTransform;
    [SerializeField] Slider slider;

    void Start()
    {
        UpdateText(slider.value);
        _handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 42.66667f);
    }

    public void SetValue(float value)
    {
        slider.value = value;
        UpdateText(value);
    }

    public void UpdateText(float value)
    {
       // text.text = ((int)value).ToString();

        switch ((int)value)
        {

            case 0:
                text.text = "Muy Fácil";
                break;
            case 1:
                text.text = "Fácil";
                break;
            case 2:
                text.text = "Medio";
                break;
            case 3:
                text.text = "Dificil";
                break;
            case 4:
                text.text = "Muy Dificil";
                break;

        }
    }
}
