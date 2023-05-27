
using UnityEngine;
using UnityEngine.UI;

public class UIExerciseSlider : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private RectTransform _handleTransform;
    [SerializeField] private BarColor barColor;
    [SerializeField] private Image fillArea;



    private int exerciseAngle;
    private int minAngle;


    void Start()
    {
        _handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 55.0f);
        exerciseAngle = GameManager.Instance.GetGameAngle();
        minAngle = GameManager.Instance.GetMinAngle();
        slider.maxValue = 1;
        slider.minValue = 0;

    }

    public void UpdateSlider(float currentValue, Movement state)
    {

        //pasamos el valor actual(entre 0-180) a un rango 0-ejercicio
      //  Debug.Log("current value: " + currentValue);
        if (currentValue <= 90-minAngle && currentValue >= (90 - exerciseAngle))
        {
            //slider.value = (exerciseAngle - (currentValue - (90 - exerciseAngle)));
            //(valor_original - (180 - y)) / (y - x)
            slider.value = 1-( (currentValue - (90 - exerciseAngle)) / (exerciseAngle - minAngle));
        }
        else if (currentValue < (90 - exerciseAngle))
        {
            slider.value = 1;
           // Debug.Log("angulo menor de 40 = : " + maxSlider.value);
        }
        else if (currentValue > 90 - minAngle)
        {
            slider.value = 0;
        }
        //comprobacione del estado-----------------
        if(state == Movement.UP)
        {
            fillArea.color = barColor.readyToPlay;
        }
        else fillArea.color = barColor.returnToIniPos;

    }

}
