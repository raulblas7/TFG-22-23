using System.Collections;
using System.Collections.Generic;
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
            sawFishingRod = true;
            fishingRodTr = other.gameObject.transform;
        }
    }
}
