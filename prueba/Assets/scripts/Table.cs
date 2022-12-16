using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
 
    public float fallVel;
    [SerializeField]
    private float deadZone = -50;
    [SerializeField]
    private Rigidbody rb;
    //para depurar
    public int index;

    public Bridge parent;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
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
