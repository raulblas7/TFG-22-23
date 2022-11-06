using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    public GameObject nextDest;

    [SerializeField]
    private float impulseH;
    [SerializeField]
    private float impulseV;

    private Vector3 currentForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            Vector3 dir =( nextDest.transform.position - transform.position) ;
            impulseH = dir.magnitude;
            Vector3 force = dir.normalized * impulseH;
            currentForce = force;
            rb.AddForce(force,ForceMode.Impulse);
            rb.AddForce(Vector3.up* impulseV, ForceMode.Impulse);
        }

       
    }
    private void OnTriggerEnter(Collider other)
    {
        print("onTriggerEnter");
        rb.AddForce(-currentForce, ForceMode.Force);
        Destroy(other.gameObject);
       
    }


}
