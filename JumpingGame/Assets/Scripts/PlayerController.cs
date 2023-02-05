using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float impulseH;
    [SerializeField]
    private float impulseV;

    private Vector3 currentForce;
    private GameObject nextDest;
    private bool canJump;

    void Start()
    {
        transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        nextDest = GameManager.Instance.getFirstCube();
        canJump = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Vector3 dir = nextDest.transform.position - transform.position;
            impulseH = dir.magnitude;
            Vector3 force = dir.normalized * impulseH;
            currentForce = force;
            rb.AddForce(force,ForceMode.Impulse);
            rb.AddForce(Vector3.up* impulseV, ForceMode.Impulse);
            canJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.AddForce(-currentForce, ForceMode.Force);
        GameManager.Instance.deleteCube();
        nextDest = GameManager.Instance.getFirstCube();
        Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NormalCube"))
        {
            CubeController cube = collision.gameObject.GetComponent<CubeController>();
            cube.SetStartDiving();
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            //GameManager.Instance.LoadScene("LoseScene");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!canJump && rb.velocity.x <= 0.1f && rb.velocity.z <= 0.1f) canJump = true;
    }

}
