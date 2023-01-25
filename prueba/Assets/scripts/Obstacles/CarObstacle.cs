using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float vel;
    [SerializeField] float waitTime = 0;

    private bool move = false;
    private float currentTime = 0;


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= waitTime)
        {
            move = true;
        }
    }

    private void FixedUpdate()
    {
        if (move)
        {
            Debug.Log(transform.forward);
            rb.AddForce(transform.forward * vel, ForceMode.Force);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ManageCarCollisions>().SetPositionToLastCheckPoint();
        }
    }
}
