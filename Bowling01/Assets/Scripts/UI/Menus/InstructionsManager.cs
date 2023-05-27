
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }
   
}
