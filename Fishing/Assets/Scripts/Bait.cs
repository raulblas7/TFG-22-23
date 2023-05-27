
using UnityEngine;

public class Bait : MonoBehaviour
{
    [SerializeField] private FishingRod rod;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
           rod.SetFishAtBait(false);
        }
    }
}
