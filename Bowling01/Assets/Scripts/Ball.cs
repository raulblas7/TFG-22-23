using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
 

    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    [SerializeField] float vel = 0.01f;


    private int maxAngle;
    private int minAngle;
    private bool thrownBall = false;
    private float currentAngle = 0;
    private int dir = 1;
    private bool throwInput = false;
 

    private void Start()
    {
     

        maxAngle = GameManager.Instance.GetGameAngle();
        minAngle = GameManager.Instance.GetGameAngle() * -1;
 
        int difficulty = GameManager.Instance.GetDifficulty();
        switch (difficulty)
        {
            case 0:
                vel = 4;
                break;
            case 1:
                vel = 6;
                break;
            case 2:
                vel = 8;
                break;
            case 3:
                vel = 10;
                break;
            case 4:
                vel = 13;
                break;

        }
    }

    void Update()
    {
        //solo actualizamos el juego si esta activo(si aun no ha terminado la partida)
        if (GameManager.Instance.IsGameActive())
        {
            currentAngle += vel * dir * Time.deltaTime;
            GameManager.Instance.SetAngle(currentAngle);
            if (currentAngle >= maxAngle)
            {
                dir = -1;
            }
            if (currentAngle <= minAngle)
            {
                dir = 1;
            }
            //if (!thrownBall && Input.GetKeyDown(KeyCode.Space))
            //{
            //    throwInput = true;
            //    thrownBall = true;
            //}
            if (!thrownBall && PlayerMovement.Instance.GetCurrentState() == Movement.MOVE_DONE)
            {
                PlayerMovement.Instance.SetState( Movement.WAITING);
                throwInput = true;
                thrownBall = true;
            }
        }

    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGameActive() && throwInput)
        {
            Quaternion q = new Quaternion();
            q.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = q;

            transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.up);

            rb.AddForce(transform.forward * -1 * force, ForceMode.Impulse);
            rb.useGravity = true;
            throwInput = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            GameManager.Instance.ThrownBall();
            Destroy(this.gameObject);
        }
    }


}

