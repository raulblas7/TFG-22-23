using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{

    //esta variable nos indica con que tabla estamos chocando dentro del puente
    private int currentBridgeTable = 0;
    [SerializeField]
    private float deadZone = -50;
    [SerializeField]
    private PlayerControler parent;

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
        parent.SetNewState(CarState.FALLING);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("onCollisionEnter");
        //Time.timeScale = 0;


        float upFace = transform.position.y + (transform.lossyScale.y / 2.0f);
        float downFace = collision.gameObject.transform.position.y - (collision.gameObject.transform.lossyScale.y / 2.0f);

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            parent.SetNewState(CarState.ONCLLISION);
            parent.SetVelocity(0);
            if (upFace + 0.01 >= downFace && upFace - 0.01 <= downFace)
            {

                //Colision por arriba
                Debug.Log("colision por arriba");
                float floorPos = transform.position.y - (transform.lossyScale.y / 2.0f);
                transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
                transform.position = new Vector3(transform.position.x, floorPos + transform.lossyScale.y, transform.position.z);


           
                collision.gameObject.GetComponent<FallingObstacle>().SetObstacleState(ObstacleState.DOWN);

            }
        }

    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    // intentos de detectar la colision por arriba
    //    {

    //        //Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.up);
    //        //RaycastHit hit;

    //        //// si detectamos colision por la parte de arriba
    //        //if (this.gameObject.GetComponent<Collider>().Raycast(ray, out hit, 10.0f))
    //        //{
    //        //    Debug.Log("colision");
    //        //    transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
    //        //}
    //    }
    //    Debug.Log("ontriggerEnter");
    //    //Time.timeScale = 0;


    //    float upFace = transform.position.y + (transform.lossyScale.y / 2.0f);
    //    float downFace = other.gameObject.transform.position.y - (other.gameObject.transform.lossyScale.y / 2.0f);

    //    // if (upFace + 0.01 >= downFace && upFace - 0.01 <= downFace)
    //    if (upFace >= downFace && downFace >= transform.position.y )
    //    {
    //        //Colision por arriba
    //        Debug.Log("colision por arriba");

    //        //float floorPos = transform.position.y - (transform.lossyScale.y / 2.0f);
    //        //transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
    //        //transform.position = new Vector3(transform.position.x, floorPos + transform.lossyScale.y, transform.position.z);
    //    }

    //    if (other.gameObject.CompareTag("Obstacle"))
    //    {
    //        parent.SetNewState(CarState.ONCLLISION);
    //    }



    //}


    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Invoke("StartRun", 2.0f);
        }
    }

    private void StartRun()
    {
        parent.SetNewState(CarState.RUN);
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("OnTriggerExit");
    //    if (other.gameObject.CompareTag("Obstacle"))
    //    {
    //        parent.SetNewState(CarState.RUN);
    //    }
    //}



}
