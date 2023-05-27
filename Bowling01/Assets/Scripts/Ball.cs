
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    
   // private int maxAngle;
    //private int minAngle;
    private bool thrownBall = false;
    //private float currentAngle = 0;
   // private int dir = 1;
    private bool throwInput = false;

    private Vector3 directionTranslate;
   public float vel = 0.01f;

    private void Start()
    {
        //ponemos el rigidbody dormido
        rb.Sleep();
        directionTranslate = Vector3.right;

        //maxAngle = GameManager.Instance.GetGameAngle();
       // minAngle = GameManager.Instance.GetGameAngle() * -1;
 
        int difficulty = GameManager.Instance.GetDifficulty();
        switch (difficulty)
        {
            case 0:
                vel = 0f;
                break;
            case 1: 
                vel = 1f;
                break;
            case 2:
                vel = 1.2f;
                break;
            case 3:
                vel = 1.5f;
                break;
            case 4:
                vel = 2.3f;
                break;

        }
    }

    void Update()
    {
        //solo actualizamos el juego si esta activo(si aun no ha terminado la partida)
        if (GameManager.Instance.IsGameActive())
        {
            //Movemos la pelota si no se ha hecho el input de la bola
            if (!throwInput && !thrownBall)
            {
                transform.Translate(directionTranslate * vel* Time.deltaTime);
            }

            //-----------------------------------------------------
            if (!thrownBall && PlayerMovement.Instance.GetCurrentState() == Movement.MOVE_DONE)
            {
                PlayerMovement.Instance.SetState( Movement.DOWN);
                throwInput = true;
                thrownBall = true;
                rb.WakeUp();
            }
        }

    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGameActive() && throwInput)
        {
            rb.AddForce(transform.forward * -1 * force, ForceMode.Impulse);
            rb.useGravity = true;
            throwInput = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZoneH") /*|| other.gameObject.CompareTag("DeadZoneV")*/)
        {
            GameManager.Instance.ThrownBall();
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("BallLimit"))
        {
            if(directionTranslate == Vector3.right) 
            {
                directionTranslate = Vector3.left;
            }
            else
            {
                directionTranslate = Vector3.right;
            }
        }
    }


}

