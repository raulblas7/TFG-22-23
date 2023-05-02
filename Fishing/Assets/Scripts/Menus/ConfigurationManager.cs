using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConfigurationManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI errorTextTime;
    [SerializeField] TMP_InputField textTime;

    [SerializeField] TextMeshProUGUI errorTextRepeat;
    [SerializeField] TMP_InputField textRepeat;


    [SerializeField] TextMeshProUGUI errorTextSerie;
    [SerializeField] TMP_InputField textSerie;

    [SerializeField] Button button;
    [SerializeField] SliderConfig slider;

    // Start is called before the first frame update
    void Start()
    {
        textRepeat.text = GameManager.Instance.GetMaxFish().ToString();
        textTime.text = GameManager.Instance.GetMaxTime().ToString();
        textSerie.text = GameManager.Instance.GetMaxSeries().ToString();
        slider.SetValue(GameManager.Instance.GetGameAngle());
        errorTextRepeat.gameObject.SetActive(false);
        errorTextTime.gameObject.SetActive(false);
        errorTextSerie.gameObject.SetActive(false);

    }



    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }

    public void SetGameAngle(float angle)
    {
        GameManager.Instance.SetGameAngle((int)angle);
    }

    public void SetTime(string time)
    {
        try
        {
            errorTextTime.gameObject.SetActive(false);

            float t = float.Parse(time);
            Debug.Log(t);
            if (t <= 0)
            {
                errorTextTime.gameObject.SetActive(true);
                button.interactable = false;
            }
            else
            {
                GameManager.Instance.SetMaxTime(t);

            }
            if (!errorTextTime.gameObject.activeSelf && !errorTextRepeat.gameObject.activeSelf && !errorTextSerie.gameObject.activeSelf)
            {
                button.interactable = true;
            }
        }
        catch
        {
            errorTextTime.gameObject.SetActive(true);
            button.interactable = false;
        }
    }

    public void SetRepeat(string num)
    {
        try
        {
            errorTextRepeat.gameObject.SetActive(false);
            int n = int.Parse(num);
            if (n <= 0)
            {
                errorTextRepeat.gameObject.SetActive(true);
                button.interactable = false;
            }
            else
            {
                GameManager.Instance.SetMAxFish(n);

            }
           
            if (!errorTextTime.gameObject.activeSelf && !errorTextRepeat.gameObject.activeSelf && !errorTextSerie.gameObject.activeSelf)
            {
                button.interactable = true;
            }
        }
        catch
        {
            errorTextRepeat.gameObject.SetActive(true);
            button.interactable = false;
        }
    }

    public void SetSeries(string num)
    {
        try
        {
            errorTextSerie.gameObject.SetActive(false);
            int n = int.Parse(num);
            if (n <= 0)
            {
                errorTextSerie.gameObject.SetActive(true);
                button.interactable = false;
            }
            else
            {
                GameManager.Instance.SetMaxSeries(n);

            }
            if (!errorTextTime.gameObject.activeSelf && !errorTextRepeat.gameObject.activeSelf && !errorTextSerie.gameObject.activeSelf)
            {
                button.interactable = true;
            }
        }
        catch
        {
            errorTextSerie.gameObject.SetActive(true);
            button.interactable = false;
        }
    }

    public void SafeConfig()
    {
        GameManager.Instance.SafeConfig();
    }
}
