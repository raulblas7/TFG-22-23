using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPath : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float errorDist = 1.0f;
    [SerializeField] private float deadZone = -50;

    [SerializeField] private PlayerCheckPoints playerCheckPoints;

    private Vector3[] positions;
    private Vector3 dest;
    private Vector3 dir;
    private int currentDest;

    void Start()
    {
        if(positions == null) InitPositions();
    }

    void Update()
    {
        //calculamos la direccion
        dir = dest - transform.position;

        // si nos acercamos lo suficiente al objetivo cambiamos de objetivo
        if (dir.magnitude <= errorDist)
        {
            //actualizamos el destino
            currentDest++;
            if (currentDest == positions.Length) { currentDest = 0; }
            dest = positions[currentDest];
        }
    }

    public Vector3 getDir()
    {
        return dir;
    }

    public void SetDest(int nextPos)
    {
        if(positions == null)
        {
            InitPositions();
        }
        dest = positions[nextPos];
        currentDest = nextPos;
    }

    private void InitPositions()
    {
        positions = new Vector3[line.positionCount];

        int aux = line.GetPositions(positions);
        if (aux != line.positionCount)
        {
            Debug.Log("no se han cogido todos los vertices");
        }
        dest = Vector3.zero;
    }
}
