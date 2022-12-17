using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private float deadZone = -50;
    [SerializeField]
    private Rigidbody rb;

    //para depurar
    public int index;
    public float fallVel;
    public Bridge parent;

    void Start()
    {
       
    }

    void Update()
    {
        if(transform.position.y < deadZone)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("colision con " + index);
            rb.AddForce(Vector3.down * fallVel, ForceMode.Impulse);
        }
    }
}
