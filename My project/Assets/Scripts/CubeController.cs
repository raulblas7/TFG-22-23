using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    Vector3 targetPosUp;
    Vector3 targetPosDown;

    [SerializeField]
    private float speedUp;

    [SerializeField]
    private float speedDown;

    private bool startDiving;

    void Start()
    {
        startDiving = false;
        targetPosUp = new Vector3(transform.position.x, 0.0f, transform.position.z);
        targetPosDown = new Vector3(transform.position.x, -0.6f, transform.position.z);
    }

    void Update()
    {
        if (startDiving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosDown, speedDown * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosUp, speedUp * Time.deltaTime);
        }
    }

    public void SetStartDiving()
    {
        startDiving = true;
    }
}
