using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{ 
    [SerializeField] Rigidbody rb;
    [SerializeField] float force;

    private void Start()
    {
        rb.AddForce(transform.forward * -1 * force);
        rb.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            GameManager.Instance.AllClean();
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * -1 * force);
    }

}
