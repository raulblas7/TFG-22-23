using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoints : MonoBehaviour
{

    private Vector3 lastCheckPoint;

    void Start()
    {
        InvokeRepeating("InstantiateCheckPoint", 0.0f, 15.0f);
    }

    void Update()
    {
        
    }

    private void InstantiateCheckPoint()
    {
        lastCheckPoint = transform.position;
    }

    public Vector3 GetLastCheckPoint()
    {
        return lastCheckPoint;
    }
}
