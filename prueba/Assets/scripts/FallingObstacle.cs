using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleState
{
    FALLING = 0,
    DOWN,
    UP,
    ASCEND,

}
public class FallingObstacle : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    public float upVel;
    public float fallVel;
    // tiempo que espera antes de caer
    public float waitTimeUp;
    public float waitTimeDown;

    private float maxY;
    private float currentTime = 0;
    private ObstacleState state;
    bool addUpForceOnce = true;
    bool addDownForceOnce = true;
    bool stopForceOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        maxY = transform.position.y;
        state = ObstacleState.FALLING;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == ObstacleState.UP)
        {
          
            //contamos el tiempo que va a estar arriba
            currentTime += Time.deltaTime;

            if (stopForceOnce)
            {
                rb.AddForce(Vector3.down * upVel, ForceMode.Force);
                stopForceOnce = false;
            }
            if (currentTime >= waitTimeUp)
            {
                state = ObstacleState.FALLING;
              
                currentTime = 0;
            }
            addDownForceOnce = true;

        }
        else if ( state == ObstacleState.FALLING)
        {
            if (addDownForceOnce)
            {
                rb.AddForce(Vector3.down * fallVel, ForceMode.Impulse);
                addDownForceOnce = false;
            }
           
 
        }
        else if(state == ObstacleState.ASCEND)
        {
            //Debug.Log("ASCEND");
            // Debug.Log("ascenso");
            if (addUpForceOnce)
            {
                rb.AddForce(Vector3.up * upVel, ForceMode.Force);
                addUpForceOnce = false;
            }
        
            //si llegamos a la altura maxima
            if(transform.position.y >= maxY)
            {
                //Debug.Log("arriba");
                state = ObstacleState.UP;
               
            }
        }
        else if (state == ObstacleState.DOWN)
        {

            //contamos el tiempo que va a estar abajo
            currentTime += Time.deltaTime;

           // Debug.Log("DOWN");
            if (currentTime >= waitTimeDown)
            {
                state = ObstacleState.ASCEND;
                addUpForceOnce = true;
                stopForceOnce = true;
                currentTime = 0;
            }
           

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
           
            state = ObstacleState.DOWN;
       
            //Debug.Log("colision");
        }
    
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        other.
    //        other.gameObject.transform.localScale = new Vector3(other.gameObject.transform.localScale.x, 0.5f, other.gameObject.transform.localScale.z);
    //    }
    //}


    public void SetObstacleState(ObstacleState o)
    {
        state = o;
    }
}
