using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    //esta variable nos indica con que tabla estamos chocando dentro del puente
    private int currentBridgeTable = 0;
    [SerializeField]
    private float deadZone = -50;

    private bool falling = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < deadZone)
        {
            //aqui pierdes
            Destroy(this.gameObject);
        }
        if (falling)
        {
            transform.Translate(Vector3.down * 0.02f);
        }

    }

    public void setBridgeTable(int t)
    {
        currentBridgeTable = t;
    }
    public int getBridgeTable()
    {
        return currentBridgeTable;
    }

    public void Fall()
    {
        transform.parent = null;
        falling = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // intentos de detectar la colision por arriba
        /////////////////////////////////////////////////////////////////////
        
        //Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.up);
        //RaycastHit hit;

        //// si detectamos colision por la parte de arriba
        //if (this.gameObject.GetComponent<Collider>().Raycast(ray, out hit, 10.0f))
        //{
        //    Debug.Log("colision");
        //    transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
        //}

        Vector3 dist = other.gameObject.transform.position - transform.position;
        Debug.Log("Distancia " + dist);

        Vector3 point =gameObject.GetComponent<Collider>().ClosestPoint(other.gameObject.transform.position);
        dist = point - transform.position;
      
        Debug.Log("Distancia2 " + dist);

        //if (esta chocando por arriba)
        //{
        //   transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
        //  //Parar al objeto
        //
        //}

    }



}
