using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    [SerializeField] int MaxAngle = 60;
    [SerializeField] int MinAngle = -60;
    [SerializeField] float vel = 0.01f;

    private bool thrownBall = false;
    private float currentAngle = 0;
    private int dir = 1;




    void Update()
    {

        currentAngle += vel * dir * Time.deltaTime;
        GameManager.Instance.setAngle(currentAngle);
        if (currentAngle >= MaxAngle)
        {
            dir = -1;
        }
        if (currentAngle <= MinAngle)
        {
            dir = 1;
        }
        if (!thrownBall && Input.GetKeyDown(KeyCode.Space))
        {
            Quaternion q = new Quaternion();
            q.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = q;

            transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.up);

            rb.AddForce(transform.forward * -1 * force, ForceMode.Impulse);
            rb.useGravity = true;
            thrownBall = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            GameManager.Instance.ThrownBall();
            Destroy(this.gameObject);
        }
    }
}

