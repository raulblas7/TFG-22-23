using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public void CloseApp()
    {
        Application.Quit();
    }

    public void ChangeScene(string Scene)
    {
        GameManager.Instance.ChangeScene(Scene);
    }
}