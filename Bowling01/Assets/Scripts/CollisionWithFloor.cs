using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithFloor : MonoBehaviour
{
    [SerializeField] Bolo parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            parent.OnThefloor();
        }
    }
}
