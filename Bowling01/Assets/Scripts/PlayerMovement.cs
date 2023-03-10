using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement
{
    MOVE_DONE = 0,
    WAITING,
    RESTART
}

public class PlayerMovement : MonoBehaviour
{


    //Instancia del launcher
    private static PlayerMovement _instance;

    private int exerciseAngle;
    private bool pass270 = false;
    private Movement currentState;



    public static PlayerMovement Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;


        }
    }
    void Start()
    {
        exerciseAngle = GameManager.Instance.GetExerciseAngle();
        currentState = Movement.RESTART;
    }

    public Movement GetCurrentState() { return currentState; }
    public void SetState(Movement state) { currentState = state; }



    public void CheckIfCanThrow(Quaternion mobileOrient)
    {
        if (GameManager.Instance.IsGameActive())
        {

            Vector3 orient = mobileOrient.eulerAngles;

            Debug.Log("El vector en angulos es: " + orient);

            //levantar el brazo unos 80 grados
            // if ((orient.x >= 350.0f || orient.x < 90.0f) && currentState == Movement.RESTART)

            if (currentState == Movement.WAITING) Debug.Log("WAITING");
            else if (currentState == Movement.RESTART) Debug.Log("RESTART");
            else Debug.Log("MOVE DONE");

            if ((orient.x >= 277.0f && currentState == Movement.WAITING) || (orient.x <= 277.0f && currentState == Movement.RESTART))
            {
                Debug.Log("pass270 ");

                pass270 = true;
            }

            //Si vamos haci arriba y no hemos pasado el 270    || si vamos hacia abajo y pasamos el 270
            if ((orient.x > 100.0f) && ((!pass270 && currentState == Movement.RESTART) || (pass270 && currentState == Movement.WAITING)))
            {
                //hacemos la conversion
                float aux = orient.x - 270.0f;
                orient.x = 270.0f - aux;
            }

            Debug.Log("Angulos convertidos: " + orient);

            if ((orient.x >= 180.0f + exerciseAngle) && currentState == Movement.RESTART)
            {
                currentState = Movement.MOVE_DONE;
                pass270 = false;
                Debug.Log("MOVE_DONE");
            }
            // else if ((orient.x < 350.0f && orient.x > 180.0f) && currentState == Movement.WAITING)
            else if ((orient.x < 180.0f + exerciseAngle - 10.0f || orient.x < 90.0f) && currentState == Movement.WAITING)
            {
                currentState = Movement.RESTART;
                pass270 = false;
                Debug.Log("RESTART");
            }
        }
    }
}
