using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPath : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float errorDist = 1.0f;
    [SerializeField] private float deadZone = -50;

    private int currentDest;
    private Vector3[] positions;
    private Vector3 dest;
    private Vector3 dir;

    void Start()
    {
        positions = new Vector3[line.positionCount];

        int aux = line.GetPositions(positions);
        if (aux != line.positionCount)
        {
            Debug.Log("no se han cogido todos los vertices");
        }
        currentDest = 0;
        dest = positions[0];
    }

    void Update()
    {
        //calculamos la direccion
        dir = dest - transform.position;

        // si nos acercamos lo suficiente al objetivo cambiamos de objetivo
        if (dir.magnitude <= errorDist)
        {
            Debug.Log("Destino nuevo " + currentDest);
            //actualizamos el destino
            currentDest++;
            if (currentDest == positions.Length) { currentDest = 0; }
            dest = positions[currentDest];
        }

        //comprobamos muerte del jugador por caida
        if (transform.position.y < deadZone)
        {
            //aqui pierdes
            Destroy(this.gameObject);
        }
    }

    public Vector3 getDir()
    {
        return dir;
    }
}
