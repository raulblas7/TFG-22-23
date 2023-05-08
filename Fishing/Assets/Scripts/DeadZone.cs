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
            Fish fish = other.GetComponent<Fish>();
            // Añadir puntos solo si estoy pescado 
            if (fish.IsInTheFishingRod())
            {
                GameManager.Instance.AddPoints(fish.GetPoints());
                GameManager.Instance.GetUIManager().CancelFishCountDown();
                GameManager.Instance.GetUIManager().ActiveReturnToInitPos();
                
            }
            // lo eliminamos de la lista
            fishInstantiator.DeleteFishFromList(other.gameObject.GetComponent<Fish>());
            
        }
    }
}
