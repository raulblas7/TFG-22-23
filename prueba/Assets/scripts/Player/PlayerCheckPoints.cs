using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoints : MonoBehaviour
{

    private CheckPointInfo lastCheckPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            lastCheckPoint = other.gameObject.GetComponent<CheckPointInfo>();
        }
    }

    public CheckPointInfo GetCheckPointInfo()
    {
        return lastCheckPoint;
    }
}
