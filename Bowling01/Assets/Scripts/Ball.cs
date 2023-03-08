using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private enum Movement
    {
        MOVE_DONE = 0,
        WAITING,
        RESTART
    }

    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    [SerializeField] float vel = 0.01f;


    private int maxAngle;
    private int minAngle;
    private bool thrownBall = false;
    private float currentAngle = 0;
    private int dir = 1;
    private Movement currentState;
    private bool throwInput = false;
    private int exerciseAngle;


    private void Start()
    {
        currentState = Movement.WAITING;
        Launcher.Instance.SetBall(this);
        maxAngle = GameManager.Instance.GetGameAngle();
        minAngle = GameManager.Instance.GetGameAngle() * -1;
        exerciseAngle = GameManager.Instance.GetExerciseAngle();
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
            if (!thrownBall && currentState == Movement.MOVE_DONE)
            {
                currentState = Movement.WAITING;
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

    public void CheckIfCanThrow(Quaternion mobileOrient)
    {
        Vector3 orient = mobileOrient.eulerAngles;

        Debug.Log("El vector en angulos es: " + orient);

        //levantar el brazo unos 80 grados
        // if ((orient.x >= 350.0f || orient.x < 90.0f) && currentState == Movement.RESTART)
        if ((orient.x >= 180.0f + exerciseAngle || orient.x < 90.0f) && currentState == Movement.RESTART)
        {
            currentState = Movement.MOVE_DONE;
            Debug.Log("MOVE_DONE");
        }
        // else if ((orient.x < 350.0f && orient.x > 180.0f) && currentState == Movement.WAITING)
        else if ((orient.x < 180.0f + exerciseAngle -10.0f && orient.x > 90.0f) && currentState == Movement.WAITING)
        {
            currentState = Movement.RESTART;
            Debug.Log("RESTART");
        }
    }
}

