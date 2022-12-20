using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum CarState
{
    RUN = 0,
    ONCLLISION,
    FALLING,


}

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private Circuit circuit;
    private Transform[] dest;
    [SerializeField]
    private NavMeshAgent agent;
    private CarState state = CarState.RUN;

    private int currentDest;
    public float increment = 1;
    public float errorDist = 1.5f;
    public float maxVelocity = 10;


    

    // Start is called before the first frame update
    void Start()
    {
        dest = circuit.gertDest();
        agent.destination = dest[currentDest].position;
      
    }

    // Update is called once per frame
    void Update()
    {
        if(state == CarState.RUN)
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



            if ((Vector3.Distance(transform.position, dest[currentDest].position)) <= errorDist   /*0.2f*/)
            {
                //Debug.Log("Destino al llegar " + currentDest);
                currentDest++;
               // Debug.Log("Destino nuevo " + currentDest);
                if (currentDest == dest.Length) { currentDest = 0; }
                agent.destination = dest[currentDest].position;
                //Debug.Log(agent.hasPath);
            }
        }

        if (state == CarState.ONCLLISION || state == CarState.FALLING)
        {
           // Debug.Log("Satte = ONCOLLISION");
            agent.speed = 0;
        }
       

   

      
        //comprobacion TODO quitar
        if (!agent.hasPath)
        {
            Debug.Log("No hay camino");
        }
    }

   public void SetNewState(CarState s)
    {
        state = s;
    }

  

    public void StopAgent()
    {
        agent.Stop();
    }

    public void ResumeAgent()
    {
        agent.Resume();
    }

}
