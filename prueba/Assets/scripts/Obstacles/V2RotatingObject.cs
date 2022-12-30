using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//enum RotatingState
//{
//    ROTATING = 0,
//    WAITING,
//}
public class V2RotatingObject : MonoBehaviour
{
    [SerializeField] private float vel;
    [SerializeField] private float maxDegrees;
    [SerializeField] private bool derecha;
    [SerializeField] private float time;
    [SerializeField] private Rigidbody rb;


    private float currentTime = 0;

    private bool returnToInit = false;
    private RotatingState state = RotatingState.WAITING;
    //publicas para depurar
    public float startAngle;
    public float aux = 0;// quitar


    private void Start()
    {
        //siempre positivo
        if (maxDegrees < 0) maxDegrees *= -1f;
        if (maxDegrees > 180) maxDegrees = 180;
        if (derecha) vel = -vel;
    }
    void Update()
    {
        if (state == RotatingState.WAITING)
        {
            //contamos el tiempo que va a estar esperando
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                state = RotatingState.ROTATING;
                //aplicamos la fuerza una unica vez
                if (returnToInit) rb.AddTorque(Vector3.up * vel);
                else rb.AddTorque(Vector3.up * -vel);
                currentTime = 0;
                startAngle = transform.rotation.eulerAngles.y;

            }
        }
        else
        {


            if (derecha)
            {
                aux = startAngle + maxDegrees;
                bool firtRound = false;
                if (aux >= 360)
                {
                    aux -= 360;
                    firtRound = true;
                }

                if (!firtRound)
                {
                    if (transform.rotation.eulerAngles.y >= aux)
                    {
                        StopAndWait();
                    }
                }
                else if (transform.rotation.eulerAngles.y < startAngle)
                {
                    if (transform.rotation.eulerAngles.y >= aux)
                    {
                        StopAndWait();
                    }
                }

            }
            else
            {

                aux = startAngle - maxDegrees;
                bool firtRound = false;
                if (aux <= 0)
                {
                    aux += 360;
                    firtRound = true;
                }

                if (!firtRound)
                {
                    if (transform.rotation.eulerAngles.y <= aux)
                    {
                        StopAndWait();
                    }
                }
                else if (transform.rotation.eulerAngles.y > startAngle)
                {
                    if (transform.rotation.eulerAngles.y <= aux)
                    {
                        StopAndWait();
                    }
                }

            }
        }

    }

    public void StopAndWait()
    {
        //Debug.Log("Pasado el limite");
        state = RotatingState.WAITING;
        //aplicamos la fuerza una unica vez para que pare
        if (returnToInit) rb.AddTorque(Vector3.up * -vel);
        else rb.AddTorque(Vector3.up * vel);
        returnToInit = !returnToInit;
        derecha = !derecha;
    }
}



