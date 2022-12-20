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

    public int index;
    public Bridge parent;

    private NavigationBaker navigationBaker;



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
        Debug.Log("oncollision enter");
        if (collision.gameObject.CompareTag("Player"))
        {
            //si choca con la primera tabla, activamos la caida de las demas
            if (index == 0)
            {
                parent.FallTables();
            }
            //le pasamos al player el indice de la tabla con la que esta chocando
            //collision.gameObject.GetComponent<Car>().setBridgeTable(index);
            collision.gameObject.GetComponent<FollowPath>().setBridgeTable(index);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        //si choca con la primera tabla, activamos la caida de las demas
    //        if (index == 0)
    //        {
    //            parent.FallTables();
    //        }
    //        //le pasamos al player el indice de la tabla con la que esta chocando
    //        other.gameObject.GetComponent<Car>().setBridgeTable(index);
    //    }
    //}


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {

    //        //Si salimos de una tabla y no esta tocando la siguente
    //        //es decir si la ultima tabla con la que colisiono es la misma de la que esta saliendo ahora
    //        //quiere decir que ya no esta tocando ninguna tabla, luego el jugador deberia caer;
    //        if (other.gameObject.GetComponent<Car>().getBridgeTable() == index)
    //        {
    //            //si salimos de la ultima tabla estamos saliendo del pueste
    //            //es todos los demas casos caeremos;
    //            if (parent.getNumTables() - 1 != index)
    //            {
    //                other.gameObject.GetComponent<Car>().Fall();

    //            }
    //        }

    //    }
    //}

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            //Si salimos de una tabla y no esta tocando la siguente
            //es decir si la ultima tabla con la que colisiono es la misma de la que esta saliendo ahora
            //quiere decir que ya no esta tocando ninguna tabla, luego el jugador deberia caer;
            // if (collision.gameObject.GetComponent<Car>().getBridgeTable() == index)
            if (collision.gameObject.GetComponent<FollowPath>().getBridgeTable() == index)
            {
                //si salimos de la ultima tabla estamos saliendo del pueste
                //es todos los demas casos caeremos;
                if (parent.getNumTables() - 1 != index)
                {
                    collision.gameObject.GetComponent<FollowPath>().Fall();

                }
            }

        }
    }

    private void ActiveGravity()
    {
        rb.useGravity = true;
        // Invoke("UpdateNavMeshSurface", 0.2f);
    }

    public void FallTable(float time)
    {
        Invoke("ActiveGravity", time);
    }

    public void SetNavigationBaker(NavigationBaker navBaker)
    {
        navigationBaker = navBaker;
    }
}
