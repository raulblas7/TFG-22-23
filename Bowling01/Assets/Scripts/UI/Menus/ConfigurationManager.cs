
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfigurationManager : MonoBehaviour
{

    [SerializeField] UpdateSlider roundSlider;
    [SerializeField] UpdateSlider seriesSlider;
    [SerializeField] UpdateSlider exerciseAngleSlider;
    [SerializeField] UpdateSlider minExerciseAngleSlider;
    [SerializeField] SelectDifficulty difficultySlider;
    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] Button finishButton;

    private int maxAngle;
    private int minAngle;

    void Start()
    {
        roundSlider.SetValue(GameManager.Instance.GetNumRounds());
        seriesSlider.SetValue(GameManager.Instance.GetMaxSeries());
        exerciseAngleSlider.SetValue(GameManager.Instance.GetExerciseAngle());
        maxAngle = GameManager.Instance.GetExerciseAngle();
        minExerciseAngleSlider.SetValue(GameManager.Instance.GetMinExerciseAngle());
        minAngle = GameManager.Instance.GetMinExerciseAngle();
        difficultySlider.SetValue(GameManager.Instance.GetDifficulty());
        errorText.gameObject.SetActive(false);
    }
    public void SetNumRound(float round)
    {
        GameManager.Instance.SetNumRound(round);
    }

    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }

    public void SetSeries(float series)
    {
        GameManager.Instance.SetMaxSeries((int)series);
    }

    public void SetExerciseAngle(float angle)
    {
        maxAngle = (int)angle;
        if (minAngle <=( maxAngle - 10))
        {
            GameManager.Instance.SetExerciseAngle(angle);
            errorText.gameObject.SetActive(false);
            finishButton.interactable = true;
        }
        else
        {
            errorText.gameObject.SetActive(true);
            finishButton.interactable = false;
        }


    }
    public void SetMinExerciseAngle(float angle)
    {
        minAngle = (int)angle;
        if (minAngle <= (maxAngle - 10))
        {
            GameManager.Instance.SetMinExerciseAngle(angle);
            errorText.gameObject.SetActive(false);
            finishButton.interactable = true;
        }
        else
        {
            errorText.gameObject.SetActive(true);
            finishButton.interactable = false;
        }
           
    }

    public void SetDifficulty(float dif)
    {
        GameManager.Instance.SetDifficulty(dif);
        if (dif == 0)
        {
            GameManager.Instance.SetGameAngle(4);
        }
        else
        {
            GameManager.Instance.SetGameAngle(dif * 20);
        }
    }

    public void SafeConfig()
    {
        GameManager.Instance.SafeConfig();
    }
}
