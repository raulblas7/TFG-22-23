using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement
{
    MOVE_DONE = 0,
    DOWN,
    UP
}

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] UIExerciseSlider slider;
    //Instancia del launcher
    private static PlayerMovement _instance;


    [SerializeField] Transform cube;

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
        currentState = Movement.DOWN;
    }

    public Movement GetCurrentState() { return currentState; }
    public void SetState(Movement state) { currentState = state; }



    public void CheckIfCanThrow(Quaternion mobileOrient)
    {
        if (GameManager.Instance.IsGameActive())
        {

            Vector3 orient = mobileOrient.eulerAngles;
            cube.rotation = mobileOrient;
            Debug.Log("El vector en angulos es: " + orient);

            Debug.Log(cube.right);
            //transformamos la orientacion en un rango de 0 a 180 grados
            float orientZ = (cube.forward.z + 1.0f) / 2.0f * 180.0f;

            // le pasamos el angulo al slider
            if (orient.x >= 270.0f && orient.x < 355.0f)
            {
                slider.UpdateSlider(orientZ);

            }

            Debug.Log("el angulo es " + orientZ);

            if (currentState == Movement.DOWN) Debug.Log("DOWN");
            else if (currentState == Movement.UP) Debug.Log("UP");
            else Debug.Log("MOVE DONE");

            if ((orientZ <= 180.0f - exerciseAngle && (orient.x >= 270.0f && orient.x < 355.0f)) && currentState == Movement.UP)
            {
                currentState = Movement.MOVE_DONE;
                Debug.Log("MOVE_DONE");
            }

            else if ((orientZ > 180.0f - 10.0f && (orient.x >= 270.0f && orient.x < 355.0f)) && currentState == Movement.DOWN)
            {
                currentState = Movement.UP;
                // Debug.Log("UP");
            }

            //guardado de los datos
            string data = orient.ToString();
            GameManager.Instance.WriteData(data);

        }
    }
   
}
