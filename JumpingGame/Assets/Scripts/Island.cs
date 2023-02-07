using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{

    [SerializeField] private GameObject jumpDest;

    public GameObject GetJumpDest()
    {
        return jumpDest;
    }
}
