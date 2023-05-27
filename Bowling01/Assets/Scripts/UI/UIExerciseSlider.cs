
using UnityEngine;
using UnityEngine.UI;

public class UIExerciseSlider : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] RectTransform _handleTransform;
    [SerializeField] private BarColor barColor;
    [SerializeField] private Image fillArea;



    private int exerciseAngle;
    private int minExerciseAngle;


    void Start()
    {
        _handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 55.0f);
        exerciseAngle = GameManager.Instance.GetExerciseAngle();
        minExerciseAngle = GameManager.Instance.GetMinExerciseAngle();
        slider.maxValue = 1;
        slider.minValue = 0;

    }

    public void UpdateSlider(float currentValue, Movement state)
    {
        //pasamos el valor actual(entre 0-180) a un rango 0-ejercicio
        if (currentValue <=( 180 -minExerciseAngle) && currentValue >= (180 - exerciseAngle))
        {
            // slider.value = (exerciseAngle - (currentValue - (180 - exerciseAngle)));
            // (valor_original - (180 - y)) / (y - x)
            slider.value = 1- ((currentValue - (180 - exerciseAngle)) / (exerciseAngle - minExerciseAngle));

        }
        else if(currentValue <(180 - exerciseAngle))
        {
            slider.value = 1;
        }
        else if(currentValue > (180 - minExerciseAngle))
        {
            slider.value =0;
        }
        //Debug.Log("Slider: " + slider.value);

        //comprobación del estado-------------------------
        if (state == Movement.UP)
        {
            fillArea.color = barColor.readyToPlay;
        }
        else fillArea.color = barColor.returnToIniPos;


    }
}
