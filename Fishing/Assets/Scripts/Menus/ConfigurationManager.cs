using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConfigurationManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI errorTextTime;
    [SerializeField] TextMeshProUGUI placeholderTime;

    [SerializeField] TextMeshProUGUI errorTextRepeat;
    [SerializeField] TextMeshProUGUI placeholderRepeat;

    [SerializeField] Button button;

    // Start is called before the first frame update
    void Start()
    {
        placeholderRepeat.text = GameManager.Instance.GetMaxFish().ToString();
        placeholderTime.text = GameManager.Instance.GetMaxTime().ToString();
        errorTextRepeat.gameObject.SetActive(false);
        errorTextTime.gameObject.SetActive(false);

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
            GameManager.Instance.SetMaxTime(t);
            button.interactable = true;
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
            GameManager.Instance.SetMAxFish(n);
            button.interactable = true;
        }
        catch
        {
            errorTextRepeat.gameObject.SetActive(true);
            button.interactable = false;
        }
    }

    public void SafeConfig()
    {
       // GameManager.Instance.SafeConfig();
    }
}
