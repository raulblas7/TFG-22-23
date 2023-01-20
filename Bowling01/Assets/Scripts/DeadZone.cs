using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bolo"))
        {

        }
        //si no es uno de los bolos sera la bola
        else
        {

        }
        Destroy(other.gameObject);
    }
}
