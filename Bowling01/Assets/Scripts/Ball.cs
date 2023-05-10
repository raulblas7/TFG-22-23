using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
 

    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
   // [SerializeField] float vel = 0.01f;

   // private int maxAngle;
    //private int minAngle;
    private bool thrownBall = false;
    //private float currentAngle = 0;
   // private int dir = 1;
    private bool throwInput = false;

    private Vector3 directionTranslate;

    private void Start()
    {
        //ponemos el rigidbody dormido
        rb.Sleep();
        directionTranslate = Vector3.right;

        //maxAngle = GameManager.Instance.GetGameAngle();
       // minAngle = GameManager.Instance.GetGameAngle() * -1;
 
        int difficulty = GameManager.Instance.GetDifficulty();
        //switch (difficulty)
        //{
        //    case 0:
        //        vel = 3.2f;
        //        break;
        //    case 1:
        //        vel = 7.5f;
        //        break;
        //    case 2:
        //        vel = 10.5f;
        //        break;
        //    case 3:
        //        vel = 14.5f;
        //        break;
        //    case 4:
        //        vel = 22;
        //        break;

        //}
    }

    void Update()
    {
        //solo actualizamos el juego si esta activo(si aun no ha terminado la partida)
        if (GameManager.Instance.IsGameActive())
        {
            //Movemos la pelota si no se ha hecho el input de la bola
            if (!throwInput && !thrownBall)
            {
                transform.Translate(directionTranslate * Time.deltaTime);
            }

            //currentAngle += vel * dir * Time.deltaTime;
            //GameManager.Instance.SetAngle(currentAngle);
            //if (currentAngle >= maxAngle)
            //{
            //    dir = -1;
            //}
            //if (currentAngle <= minAngle)
            //{
            //    dir = 1;
            //}
            //---------------------------------------------------
            //if (!thrownBall && Input.GetKeyDown(KeyCode.Space))
            //{
            //    throwInput = true;
            //    thrownBall = true;
            //}
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
            //Quaternion q = new Quaternion();
            //q.eulerAngles = new Vector3(0, 0, 0);
            //transform.rotation = q;
            //
            //transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.up);

            rb.AddForce(transform.forward * -1 * force, ForceMode.Impulse);
            rb.useGravity = true;
            throwInput = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZoneH") || other.gameObject.CompareTag("DeadZoneV"))
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

