using UnityEngine;
using UnityEngine.AI;

public class AIController2 : MonoBehaviour
{
    // Circuito que va a recorrer el coche
    public Circuit circuit;

    // Referencia al script del coche
    private Cart cart;

    //Sensibilidad de la direccion
    public float steeringSensitivity = 0.01f;
    //Sensibilidad de freno
    public float breakingSensitivity = 1.0f;
    //Sensibilidad de aceleracion
    public float accelerationSensitivity = 0.3f;

    public GameObject trackerPrefab;
    NavMeshAgent agent;

    int currentTrackerWP;
    float lookAhead = 10;

    float lastTimeMoving = 0;

    void Start()
    {
        cart = GetComponent<Cart>();
        GameObject tracker = Instantiate(trackerPrefab, cart.transform.position, cart.transform.rotation);
        agent = tracker.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Actualiza el agente
        ProgressTracker();

        Vector3 localTarget;
        float targetAngle;

        if (cart._rigidbody.velocity.magnitude > 1)
        {
            lastTimeMoving = Time.time;
        }

        // Si el coche no se ha movido durante 4 segundos setea la posicion del coche a la posicion destino 
        if (Time.time > lastTimeMoving + 4)
        {
            Debug.Log("Entra en lastimemoving + 4");
            cart.transform.position = circuit.waypoints[currentTrackerWP].transform.position + Vector3.up * 2;
            agent.transform.position = cart.transform.position;
            //cart.gameObject.layer = 8;

            //Invoke("ResetLayer", 3);
        }
        localTarget = cart.transform.InverseTransformPoint(agent.transform.position);
        targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        Debug.Log("Target angle es: " + targetAngle);

        // Hace el clamp de de targetAngle * la sensibilidad de giro para que este entre [-1, 1] y lo multiplica por el signo de la velocidad (1 o -1)
        float steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1, 1) * Mathf.Sign(cart.current_speed);

        float speedFactor = cart.current_speed / 30;
        float corner = Mathf.Clamp(Mathf.Abs(targetAngle), 0, 90);
        float cornerFactor = corner / 90f;

        float brake = 0;
        //if (corner > 10 && speedFactor > 0.1f)
        //    brake = Mathf.Lerp(0, 1 + speedFactor * breakingSensitivity, cornerFactor);

        float accel = 1f;
        if (corner > 20 && speedFactor > 0.1f && speedFactor > 0.2f)
            accel = Mathf.Lerp(0, 1 * accelerationSensitivity, 1 - cornerFactor);

        cart.AccelerateCart(accel, steer, brake);
    }

    // Mueve al navmesh agent (no al coche, este seguira al agente)
    void ProgressTracker()
    {
        // Si el coche esta separado por mas de 10 unidades el agente se para
        if (Vector3.Distance(agent.transform.position, cart.transform.position) > lookAhead)
        {
            agent.isStopped = true;
            return;
        }
        else
        {
            agent.isStopped = false;
        }

        // Setea el destino del agente
        agent.SetDestination(circuit.waypoints[currentTrackerWP].position);

        //Si hemos llegado al objetivo siguiente aumentamos o damos otra vuelta
        if (Vector3.Distance(agent.transform.position, circuit.waypoints[currentTrackerWP].position) < 4)
        {
            currentTrackerWP++;
            if (currentTrackerWP >= circuit.waypoints.Count)
                currentTrackerWP = 0;
        }
    }

    void ResetLayer()
    {
        cart.gameObject.layer = 10;
    }
}
