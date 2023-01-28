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

    private bool goToFishingRod = false;

    void Start()
    {
        InvokeRepeating("ChangeDir", 0f, changeDirTime);
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
        rb.AddForce(transform.right * vel);
    }

    private void ChangeDir()
    {

    }

    private void GoToFishingRod()
    {
        Transform fishingRodPosTR = fishVision.GetFishingRodTr();
        Vector3 dir = fishingRodPosTR.position - transform.position;

        float angle = Vector3.Angle(transform.right, Vector3.right);
        float angleFishRod = Vector3.Angle(transform.right, dir);
        // saber si el pez esta a la derecha o a la izquierda del eje x
        if (AngleDir(Vector3.right, transform.right, Vector3.up) >= 0)
        {
            Debug.Log("Derecha de vector right");
            //invertimos el angulo
            angle = 360 - angle;

        }
        else
        {
            Debug.Log("Izquierda de vector right");
        }
        //mirar si la caña esta a la izquierda o a la derecha de el pez
        if (AngleDir(transform.right, dir, Vector3.up) >= 0)
        {
            Debug.Log("Derecha de el pez");
            angle -= angleFishRod;
        }
        else
        {
            Debug.Log("Izquierda de el pez");
            angle += angleFishRod;
        }


        Debug.Log("Angulo goTo: " + angle);

        Quaternion quaternion = Quaternion.AngleAxis(angle *-1, transform.up);
        Vector3 aux = quaternion.eulerAngles;
        aux.x = transform.rotation.eulerAngles.x;
        aux.z = transform.rotation.eulerAngles.z;

        quaternion.eulerAngles = aux;
        rb.MoveRotation(quaternion);

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

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Right:
                angle = Random.Range(120, 230);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

                q.eulerAngles = aux;
                rb.MoveRotation(q);

                break;
            case Sides.Up:
                angle = Random.Range(-90, 90);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.y;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Down:
                angle = Random.Range(90, 270);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.y;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Forward:
                angle = Random.Range(180, 360);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Back:
                angle = Random.Range(30, 150);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

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
