using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationManager : MonoBehaviour
{

    [SerializeField] UpdateSlider roundSlider;
    [SerializeField] UpdateSlider gameAngleSlider;
    [SerializeField] UpdateSlider exerciseAngleSlider;

    void Start()
    {
        roundSlider.SetValue( GameManager.Instance.GetNumRounds());
        gameAngleSlider.SetValue(GameManager.Instance.GetGameAngle());
        exerciseAngleSlider.SetValue(GameManager.Instance.GetExerciseAngle());
    }
    public void SetNumRound(float round)
    {
        GameManager.Instance.SetNumRound(round);
    }

    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }

    public void SetGameAngle(float angle)
    {
        GameManager.Instance.SetGameAngle(angle);
    }

    public void SetExerciseAngle(float angle)
    {
        GameManager.Instance.SetExerciseAngle(angle);
    }

    public void SetBarVelocity(float vel)
    {

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
