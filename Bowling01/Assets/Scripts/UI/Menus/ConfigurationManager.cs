using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationManager : MonoBehaviour
{

    [SerializeField] UpdateSlider roundSlider;
    [SerializeField] UpdateSlider seriesSlider;
    [SerializeField] UpdateSlider exerciseAngleSlider;
    [SerializeField] SelectDifficulty difficultySlider;

    void Start()
    {
        roundSlider.SetValue(GameManager.Instance.GetNumRounds());
        seriesSlider.SetValue(GameManager.Instance.GetMaxSeries());
        exerciseAngleSlider.SetValue(GameManager.Instance.GetExerciseAngle());
        difficultySlider.SetValue(GameManager.Instance.GetDifficulty());
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
        GameManager.Instance.SetExerciseAngle(angle);
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


    // Update is called once per frame
    void Update()
    {

    }
}
