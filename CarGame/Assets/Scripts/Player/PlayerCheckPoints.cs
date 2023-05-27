
using UnityEngine;

public class PlayerCheckPoints : MonoBehaviour
{

    private CheckPointInfo lastCheckPoint;

    public CheckPointInfo GetCheckPointInfo()
    {
        return lastCheckPoint;
    }
}
