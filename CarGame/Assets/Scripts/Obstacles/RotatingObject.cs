
using UnityEngine;

enum RotatingState
{
    ROTATING = 0,
    WAITING,
}
public class RotatingObject : MonoBehaviour
{
    [SerializeField] private float vel;
    [SerializeField] private float maxDegrees;
    [SerializeField] private float time;
    


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
                //if (returnToInit) rb.AddTorque(transform.up * vel);
                //else rb.AddTorque(transform.up * -vel);
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
                //if (returnToInit) rb.AddTorque(transform.up * -vel);
                //else rb.AddTorque(Vector3.up * +vel);
                returnToInit = !returnToInit;
            }
        }
    }

  
}
