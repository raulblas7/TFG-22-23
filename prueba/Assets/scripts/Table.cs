using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Table : MonoBehaviour
{
    [SerializeField]
    private float deadZone = -50;
    [SerializeField]
    private Rigidbody rb;

    //para depurar
    public int index;
    public Bridge parent;
    
    private NavigationBaker navigationBaker;

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
            Invoke("ActiveGravity", 0.3f);
        }
    }

    private void ActiveGravity()
    {
        rb.useGravity = true;
        //Invoke("UpdateNavMeshSurface", 0.2f);
    }

    public void SetNavigationBaker(NavigationBaker navBaker)
    {
        navigationBaker = navBaker;
    }
}
