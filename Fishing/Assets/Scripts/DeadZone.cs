
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private FishInstantiator fishInstantiator;
    [SerializeField] private FishingRod fishingRod;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            Fish1 fish = other.GetComponent<Fish1>();
            // Añadir puntos solo si estoy pescado 
            if (fish.IsInTheFishingRod())
            {
                GameManager.Instance.AddPoints(fish.GetPoints());
                GameManager.Instance.GetUIManager().CancelFishCountDown();
                GameManager.Instance.GetUIManager().ActiveReturnToInitPos();
                
            }
            // lo eliminamos de la lista
            fishInstantiator.DeleteFishFromList(other.gameObject.GetComponent<Fish1>());
            
        }
    }
}
