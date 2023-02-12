using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private float speedUp;
    [SerializeField] private float speedDown;
    [SerializeField] private float posYUp;
    [SerializeField] private float posYDown;

    private bool startDiving;
    private Vector3 targetPosUp;
    private Vector3 targetPosDown;


    void Start()
    {
        startDiving = false;
        targetPosUp = new Vector3(transform.position.x, posYUp, transform.position.z);
        targetPosDown = new Vector3(transform.position.x, posYDown, transform.position.z);
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