using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{

    [SerializeField] private CheckPointInfo nextCheckPoint;
    [SerializeField] private ManageCarCollisions manageCar;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        manageCar.SetPositionToNextCheckpoint(nextCheckPoint);
    //    }
    //}
}
