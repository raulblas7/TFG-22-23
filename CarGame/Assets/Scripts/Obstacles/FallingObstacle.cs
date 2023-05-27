
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

    void Start()
    {
        maxY = transform.position.y;
        state = ObstacleState.FALLING;
    }

    void Update()
    {
        if (state == ObstacleState.UP)
        {
            //contamos el tiempo que va a estar arriba
            currentTime += Time.deltaTime;

            if (currentTime >= waitTimeUp)
            {
                state = ObstacleState.FALLING;
              
                currentTime = 0;
            }

        }
        else if(state == ObstacleState.ASCEND)
        {
            //si llegamos a la altura maxima
            if(transform.position.y >= maxY)
            {
                state = ObstacleState.UP;
            }
        }
        else if (state == ObstacleState.DOWN)
        {
            //contamos el tiempo que va a estar abajo
            currentTime += Time.deltaTime;

            if (currentTime >= waitTimeDown)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
                state = ObstacleState.ASCEND;
                addUpForceOnce = true;
                stopForceOnce = true;
                currentTime = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (state == ObstacleState.UP)
        {
            if (stopForceOnce)
            {
                rb.AddForce(Vector3.down * upVel, ForceMode.Force);
                stopForceOnce = false;
            }
            addDownForceOnce = true;
        }
        else if (state == ObstacleState.FALLING)
        {
            if (addDownForceOnce)
            {
                rb.AddForce(Vector3.down * fallVel, ForceMode.Impulse);
                addDownForceOnce = false;
            }
        }
        else if (state == ObstacleState.ASCEND)
        {
            if (addUpForceOnce)
            {
                rb.AddForce(Vector3.up * upVel, ForceMode.Force);
                addUpForceOnce = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            state = ObstacleState.DOWN;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            //Debug.Log("colision");
        }
    
    }

    public void SetObstacleState(ObstacleState o)
    {
        state = o;
    }
}
