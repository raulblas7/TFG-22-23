using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCarCollision : MonoBehaviour
{
    [SerializeField] ManageCarCollisions manageCarCollisions;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("floor"))
        {
            manageCarCollisions.SetPositionToLastCheckPoint();
        }
    }
}
