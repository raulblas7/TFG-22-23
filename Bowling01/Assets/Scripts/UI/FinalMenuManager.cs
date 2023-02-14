using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMenuManager : MonoBehaviour
{
    public void SetNumRound(float round)
    {
        GameManager.Instance.SetNumRound(round);
    }


    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }
}
