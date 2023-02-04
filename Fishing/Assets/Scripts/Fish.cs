using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum Sides
{
    Left,
    Right,
    Up,
    Down,
    Back,
    Forward

}
public class Fish : MonoBehaviour
{

    [SerializeField] private float changeDirTime;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float vel;
    [SerializeField] private FishVision fishVision;
    [SerializeField] private float lifeTime;
    [SerializeField] private float waitTimeInRod;
 

    private bool goToFishingRod = false;
    private bool alreadyAtFishingRod = false;

    void Start()
    {

        Invoke("DeadFish", lifeTime);
        // InvokeRepeating("ChangeDir", 0f, changeDirTime);
    }

    void Update()
    {
        if (!goToFishingRod && fishVision.IsSawFishingRod())
        {
            // Ir hacia la caña
            GoToFishingRod();
        }
    }

    private void FixedUpdate()
    {
        if (!alreadyAtFishingRod) rb.AddForce(transform.right * vel);
    }

    private void DeadFish()
    {
        if (!alreadyAtFishingRod)
        {
            Destroy(this.gameObject);

        }
    }
    private void ChangeDir()
    {
        int side = Random.Range(0, 5);
        UTurn((Sides)side);
    }

    private void GoToFishingRod()
    {
        Transform fishingRodPosTR = fishVision.GetFishingRodTr();
        Vector3 dir = fishingRodPosTR.position - transform.position;


        //float angle = Vector3.Angle(transform.right, Vector3.right);
        //float angleFishRod = Vector3.Angle(transform.right, dir);

        //// saber si el pez esta a la derecha o a la izquierda del eje x
        //if (AngleDir(Vector3.right, transform.right, Vector3.up) >= 0)
        //{
        //    Debug.Log("Derecha de vector right");
        //    //invertimos el angulo
        //    angle = 360 - angle;

        //}
        //else
        //{
        //    Debug.Log("Izquierda de vector right");
        //}


        //Debug.Log("Angulo goTo before: " + angle);
        ////mirar si la caña esta a la izquierda o a la derecha de el pez
        //if (AngleDir(transform.right, dir, Vector3.up) >= 0)
        //{
        //    Debug.Log("Derecha de el pez");
        //    angle -= angleFishRod;
        //}
        //else
        //{
        //    Debug.Log("Izquierda de el pez");
        //    angle += angleFishRod;
        //}

        //float angleUpDown =  angleFishRod;
        //if (AngleDir(Vector3.right, transform.right, Vector3.forward) >= 0)
        //{
        //    Debug.Log("Arriba");
        //}
        //else
        //{
        //    Debug.Log("Abajo");
        //    angleUpDown = 360-angleUpDown;
        //}
        ////mirar si la caña esta a la izquierda o a la derecha de el pez
        //if (AngleDir(transform.right, dir, Vector3.forward) >= 0)
        //{
        //    Debug.Log("Abajo de el pez");
        //    angleUpDown -= angleFishRod;
        //}
        //else
        //{
        //    Debug.Log("Arriba de el pez");
        //    angleUpDown += angleFishRod;
        //}

        //Debug.Log("Angulo goTo: " + angle);
        //Debug.Log("Angulo up down: " + angleUpDown);
        //Debug.Log("Angulo pez,caña: " + angleFishRod);



        //Quaternion q = Quaternion.AngleAxis(-angle, transform.up);
        //Vector3 aux;
        //aux = q.eulerAngles;
        //aux.x = transform.rotation.eulerAngles.x;
        //aux.z = transform.rotation.eulerAngles.z;

        //q = Quaternion.AngleAxis(-angleUpDown, transform.forward);

        //aux.z = q.eulerAngles.z;
        //aux.x = transform.rotation.eulerAngles.x;

        //q.eulerAngles = aux;
        Quaternion q = Quaternion.FromToRotation(transform.right, dir);
        Debug.Log(q.eulerAngles);
        Vector3 aux;
        aux = q.eulerAngles;
        aux.x = transform.rotation.eulerAngles.x;

        aux.y = transform.rotation.eulerAngles.y + aux.y;
        aux.z = transform.rotation.eulerAngles.z - aux.z;
        q.eulerAngles = aux;
        Debug.Log(q.eulerAngles);
        rb.MoveRotation(q);

        goToFishingRod = true;

        EditorApplication.isPaused = true;
    }

    private void UTurn(Sides side)
    {
        Quaternion q;
        Vector3 aux;
        int angle = 0;
        switch (side)
        {
            case Sides.Left:
                angle = Random.Range(-60, 60);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

                angle = Random.Range(-60, 60);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux.z = q.eulerAngles.z;
                aux.x = transform.rotation.eulerAngles.x;


                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Right:
                angle = Random.Range(120, 230);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

                angle = Random.Range(-60, 60);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux.z = q.eulerAngles.z;
                aux.x = transform.rotation.eulerAngles.x;

                q.eulerAngles = aux;
                rb.MoveRotation(q);

                break;
            case Sides.Up:

                angle = Random.Range(10, 60);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.y = transform.rotation.eulerAngles.y;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Down:
                angle = Random.Range(-70, -20);
                Debug.Log(angle);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.y = transform.rotation.eulerAngles.y;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Forward:
                angle = Random.Range(180, 360);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

                angle = Random.Range(-60, 60);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux.z = q.eulerAngles.z;
                aux.x = transform.rotation.eulerAngles.x;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Back:
                angle = Random.Range(30, 150);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

                angle = Random.Range(-60, 60);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux.z = q.eulerAngles.z;
                aux.x = transform.rotation.eulerAngles.x;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Right"))
        {

            UTurn(Sides.Right);
        }
        else if (collision.gameObject.CompareTag("Left"))
        {

            UTurn(Sides.Left);
        }
        else if (collision.gameObject.CompareTag("Up"))
        {

            UTurn(Sides.Up);
        }
        else if (collision.gameObject.CompareTag("Down"))
        {

            UTurn(Sides.Down);
        }
        else if (collision.gameObject.CompareTag("Back"))
        {

            UTurn(Sides.Back);
        }
        else if (collision.gameObject.CompareTag("Forward"))
        {

            UTurn(Sides.Forward);
        }
        else if (collision.gameObject.CompareTag("FishingRod") && fishVision.IsSawFishingRod())
        {
            FishingRod rod = collision.gameObject.GetComponent<FishingRod>();
            if (!rod.HasChild())
            {

                alreadyAtFishingRod = true;
                transform.parent = collision.gameObject.transform;
                rod.SetChild();

            }
        }
    }

    private float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }


}
