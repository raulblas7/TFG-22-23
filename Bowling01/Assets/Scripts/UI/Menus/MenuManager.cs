using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void SetNumRound(float round)
    {
        GameManager.Instance.SetNumRound(round);
    }

   
    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
