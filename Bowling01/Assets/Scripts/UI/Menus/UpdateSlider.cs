
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateSlider : MonoBehaviour
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
        text.text = ((int)value).ToString();
    }
}
