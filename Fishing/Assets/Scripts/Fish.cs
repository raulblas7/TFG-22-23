using System.Collections;
using System.Collections.Generic;
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

    private float currentTime = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= changeDirTime)
        {
            ChangeDir();
            currentTime = 0;
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(transform.right * vel);
    }

    private void ChangeDir()
    {

    }

    private void UTurn(Sides side)
    {
        Quaternion q;
        Vector3 aux;
        int angle = 0;
        switch (side)
        {
            case Sides.Left:
                angle = Random.RandomRange(-90, 90);
                q = Quaternion.AngleAxis(angle, transform.forward);

               // aux = q.eulerAngles;
               // aux.x = transform.rotation.eulerAngles.x;
               // aux.z = transform.rotation.eulerAngles.z;

                //q.eulerAngles = aux;
                rb.MoveRotation(q);


                break;
            case Sides.Right:
                angle = Random.RandomRange(90, 270);
                q = Quaternion.AngleAxis(angle, transform.forward);

                //aux = q.eulerAngles;
                //aux.x = transform.rotation.eulerAngles.x;
                //aux.z = transform.rotation.eulerAngles.z;

                //q.eulerAngles = aux;
                rb.MoveRotation(q);

                break;
            case Sides.Up:
                angle = Random.RandomRange(-90, 90);
                q = Quaternion.AngleAxis(angle, transform.up);

               // aux = q.eulerAngles;
               // aux.x = transform.rotation.eulerAngles.x;
               // aux.z = transform.rotation.eulerAngles.y;

                //weq.eulerAngles = aux;
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


}
