using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private FishInstantiator fishInstantiator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            // Añadir puntos

            // lo eliminamos de la lista
            fishInstantiator.DeleteFishFromList(collision.gameObject.GetComponent<Fish>());
        }
    }
}
