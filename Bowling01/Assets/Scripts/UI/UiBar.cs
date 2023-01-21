using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] int MaxValue = 60;
    [SerializeField] int MinValue = -60;

    private float currentValue;
  
 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentValue = GameManager.Instance.getAngle();
        float aux = (float)(currentValue - MinValue) / (float)(MaxValue - MinValue);
        slider.value = aux;

       
        
    }
}
