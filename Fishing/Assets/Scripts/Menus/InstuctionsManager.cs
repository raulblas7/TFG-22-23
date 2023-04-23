using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstuctionsManager : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }
}
