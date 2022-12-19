using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//public enum CarState
//{
//    RUN = 0,
//    ONCLLISION,
//    FALLING,


//}

public class Player : MonoBehaviour
{
    [SerializeField]
    private Circuit circuit;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private float deadZone = -50;
   

    //esta variable nos indica con que tabla estamos chocando dentro del puente
    private int currentBridgeTable = 0;
    private Transform[] dest;
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
        if (state == CarState.RUN)
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

        else if (state == CarState.ONCLLISION  )
        {
            // Debug.Log("State = ONCOLLISION");
            agent.speed = 0;
        }
        else if(state == CarState.FALLING)
        {
            transform.Translate(Vector3.down * 0.02f);
        }


         // muerte del jugador
        if (transform.position.y < deadZone)
        {
            //aqui pierdes
            Destroy(this.gameObject);
        }
        





        //comprobacion TODO quitar
        if (!agent.hasPath)
        {
            Debug.Log("No hay camino");
        }
    }

    public void setBridgeTable(int t)
    {
        currentBridgeTable = t;
    }
    public int getBridgeTable()
    {
        return currentBridgeTable;
    }

    public void Fall()
    {
        transform.parent = null;
  
        SetNewState(CarState.FALLING);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("onCollisionEnter");
        //Time.timeScale = 0;


       

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SetNewState(CarState.ONCLLISION);
            float upFace = transform.position.y + (transform.lossyScale.y / 2.0f);
            float downFace = collision.gameObject.transform.position.y - (collision.gameObject.transform.lossyScale.y / 2.0f);
            if (upFace + 0.01 >= downFace && upFace - 0.01 <= downFace)
            {

                //Colision por arriba
                Debug.Log("colision por arriba");
                //jugador aplastado
                transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
                //ponemos el obstaculo a down
                collision.gameObject.GetComponent<FallingObstacle>().SetObstacleState(ObstacleState.DOWN);

            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");
        if (collision.gameObject.CompareTag("Obstacle"))
        {
           SetNewState(CarState.RUN);
        }
    }

    public void SetNewState(CarState s)
    {
        state = s;
    }
}
