using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent carAgent;

    [SerializeField]
    private float speedMax;

    public Transform dest;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && carAgent.speed + 0.2f <= speedMax)
        {
            carAgent.speed += 0.2f;
            carAgent.SetDestination(dest.position);
        }
        else if(carAgent.speed - 0.2f >= 0f)
        {
            carAgent.speed -= 0.2f;
        }
    }
}
