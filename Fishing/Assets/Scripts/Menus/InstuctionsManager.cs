
using UnityEngine;

public class InstuctionsManager : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }
}
