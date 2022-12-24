using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum RotatingState
{
    ROTATING = 0,
    WAITING,
}
public class RotatingObject : MonoBehaviour
{
    [SerializeField]
    private float vel;
    [SerializeField]
    private float maxDegrees;
    [SerializeField]
    public float time;

    private float currentTime = 0;
    private float currentDegrees = 0;
    private bool returnToInit = false;
    private RotatingState state = RotatingState.WAITING;

    void Update()
    {
        if (state == RotatingState.WAITING)
        {
            //contamos el tiempo que va a estar esperando
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                state = RotatingState.ROTATING;

                currentTime = 0;
            }
        }
        else
        {
            if (returnToInit) transform.Rotate(new Vector3(0, 1, 0), -vel);
            else transform.Rotate(new Vector3(0, 1, 0), vel);

            currentDegrees += vel;
            if (currentDegrees >= maxDegrees)
            {
                currentDegrees = 0;
                state = RotatingState.WAITING;
                returnToInit = !returnToInit;
            }
        }
    }
}
