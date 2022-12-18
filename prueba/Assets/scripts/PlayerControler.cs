using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private Transform[] dest;
    [SerializeField]
    private NavMeshAgent agent;

    private int currentDest;
    public float increment = 1;
    public float errorDist = 1.5f;
    public float maxVelocity = 10;

    // Start is called before the first frame update
    void Start()
    {
        agent.destination = dest[currentDest].position;
    }

    // Update is called once per frame
    void Update()
    {
        // cambiamos la velocidad
        if (Input.GetKey(KeyCode.Space) && agent.speed <= maxVelocity)
        {
            agent.speed += increment;
        }
        else if (agent.speed >= increment)
        {
            agent.speed -= increment;
        }
        else agent.speed = 0;

        // Debug.Log(Vector3.Distance(transform.position, dest[currentDest].position));

        if ((Vector3.Distance(transform.position, dest[currentDest].position)) <= errorDist   /*0.2f*/)
        {
            Debug.Log("Destino al llegar " + currentDest);
            currentDest++;
            Debug.Log("Destino nuevo " + currentDest);
            if(currentDest == dest.Length) { Debug.Log("espero que aqui no entres xD"); currentDest = 0; }
            agent.destination = dest[currentDest].position;
            Debug.Log(agent.hasPath);
        }

        if (!agent.hasPath)
        {
            Debug.Log("No hay camino");
        }
    }

    


}
