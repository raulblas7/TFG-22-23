
using UnityEngine;

public class FishVision : MonoBehaviour
{

    private Transform fishingRodTr;
    private bool sawFishingRod = false;

    public bool IsSawFishingRod()
    {
        return sawFishingRod;
    }

    public Transform GetFishingRodTr()
    {
        return fishingRodTr;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FishingRod"))
        {
            Debug.Log("ca�a detectada");
            sawFishingRod = true;
            fishingRodTr = other.gameObject.transform;
        }
    }
}
