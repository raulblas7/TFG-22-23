using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private float deadZone = -50;
    [SerializeField]
    private Rigidbody rb;

    public int index;
    public Bridge parent;

    private void Start()
    {
        rb.Sleep();
    }

    void Update()
    {
        if (transform.position.y < deadZone)
        {
            Destroy(this.gameObject);
        }
        // if (rb.useGravity == true) Debug.Log("Gravedad activada en " + index);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wheel"))
        {
            //si choca con la primera tabla, activamos la caida de las demas
            if (index != 0 && index != parent.numTables - 1)
            {
                //parent.FallTables();
                Invoke("ActiveGravity", 5f);
            }
        }
    }

    private void ActiveGravity()
    {
        Debug.Log("Hola");
        rb.WakeUp();
        //rb.useGravity = true;
    }

    public void FallTable(float time)
    {
        Invoke("ActiveGravity", time);
    }

    public void ContraintAll()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
