using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private FishInstantiator fishInstantiator;
    [SerializeField] private FishingRod fishingRod;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            // Añadir puntos solo si estoy pescado (falta un if)

            // lo eliminamos de la lista
            fishInstantiator.DeleteFishFromList(other.gameObject.GetComponent<Fish>());
            
        }
    }
}
