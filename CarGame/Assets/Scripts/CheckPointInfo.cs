using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointInfo : MonoBehaviour
{
    [SerializeField] private int nextPointInDest;

    public int GetNextPointInDest()
    {
        return nextPointInDest;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
