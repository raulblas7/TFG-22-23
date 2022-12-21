using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CarState
//{
//    RUN = 0,
//    ONCLLISION,
//    FALLING,


//}
public class FollowPath : MonoBehaviour
{
    [SerializeField]
    private Circuit circuit;
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float errorDist = 1.0f;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float increment = 1.0f;
    [SerializeField]
    private float deadZone = -50;

    private Vector3[] positions;
    private Transform[] dests;

    private int currentDest;
    private Vector3 dest;
    //publico para depurar
    public float vel = 1;
    private CarState state = CarState.RUN;
    //esta variable nos indica con que tabla estamos chocando dentro del puente
    private int currentBridgeTable = 0;

    private Vector3 dir;

    void Start()
    {
        positions = new Vector3[line.positionCount];

        dests = circuit.gertDest();

        int aux = line.GetPositions(positions);
        if (aux != line.positionCount)
        {
            Debug.Log("no se han cogido todos los vertices");
        }
        currentDest = 0;
        //dest = positions[0];
        dest = dests[currentDest].position;
        //calculamos la direccion

    }

    void Update()
    {
        if (state == CarState.RUN)
        {
            //calculamos la direccion
            dir = dest - transform.position;
            //actualizamos la velocidad
           //if (Input.GetKey(KeyCode.Space) && vel <= maxVelocity)
           //{
           //    vel += increment;
           //}
           //else if (vel >= increment)
           //{
           //    vel -= increment;
           //}
           //else vel = 0;

            // si nos acercamos lo suficiente al objetivo cambiamos de objetivo
            //if (dir.magnitude <= errorDist)
            //{
            //    //Debug.Log("cambio de destino");
            //    //actualizamos el destino
            //    currentDest++;
            //    if (currentDest == positions.Length) { currentDest = 0; }
            //    dest = positions[currentDest];
            //
            //}

            if ((Vector3.Distance(transform.position, dest)) <= errorDist   /*0.2f*/)
            {
                //Debug.Log("Destino al llegar " + currentDest);
                currentDest++;
                // Debug.Log("Destino nuevo " + currentDest);
                if (currentDest == dests.Length) { currentDest = 0; }
                dest = dests[currentDest].position;
                //Debug.Log(agent.hasPath);
            }
            //actualizamos la rotacion

            //Vector3 aux = new Vector3(dir.x, 0, dir.z);
            //Quaternion q = Quaternion.LookRotation(aux, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

            // rb.MoveRotation(q);
            //transform.Rotate(Vector3.up, angle);

            //aplicamos la fuerza
            //Vector3 dirN = dir.normalized;
            //rb.AddForce(dirN * vel, ForceMode.Force);
            //rb.velocity = dirN * vel;
            //transform.Translate(dirN * vel * Time.deltaTime, Space.World);

        }
        else if (state == CarState.ONCLLISION)
        {
            vel = 0;
        }
        else if (state == CarState.FALLING)
        {
            transform.Translate(Vector3.down * 0.02f);
        }

        //comprobamos muerte del jugador por caida
        if (transform.position.y < deadZone)
        {
            //aqui pierdes
            Destroy(this.gameObject);
        }
    }

    public Vector3 getDir()
    {
        return dir;
    }

    //comprobamos colisiones
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("onCollisionEnter");
        //Time.timeScale = 0;


        float upFace = transform.position.y + (transform.lossyScale.y / 2.0f);
        float downFace = collision.gameObject.transform.position.y - (collision.gameObject.transform.lossyScale.y / 2.0f);

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            state =CarState.ONCLLISION;

            if (upFace + 0.1 >= downFace && upFace - 0.1 <= downFace)
            {

                //Colision por arriba
                Debug.Log("colision por arriba");
                float floorPos = transform.position.y - (transform.lossyScale.y / 2.0f);
                transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
                transform.position = new Vector3(transform.position.x, floorPos + transform.lossyScale.y, transform.position.z);
                collision.gameObject.GetComponent<FallingObstacle>().SetObstacleState(ObstacleState.DOWN);

            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            state = CarState.RUN;
            //Invoke("StartRun", 2.0f);
        }
    }

    public void Fall()
    {
        state = CarState.FALLING;
    }

    public void setBridgeTable(int t)
    {
        currentBridgeTable = t;
    }
    public int getBridgeTable()
    {
        return currentBridgeTable;
    }
}
