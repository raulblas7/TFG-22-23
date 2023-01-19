using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float force;

    private bool thrownBall = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!thrownBall && Input.GetKeyDown(KeyCode.Space)  )
        {
            rb.AddForce(transform.forward * -1 * force, ForceMode.Impulse);
            rb.useGravity = true;
            thrownBall = true;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
