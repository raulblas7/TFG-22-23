using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum Movement
    {
        MOVE_DONE =0,
        WAITING,
        RESTART
    }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float impulseH;
    [SerializeField] private float impulseV;

    [SerializeField] private int pointsPerChest = 100;

    [SerializeField] private Animator animator = null;

    //[SerializeField] private ArduinoConnection arduinoConnection;

    private Vector3 currentForce;
    private GameObject nextDest;
    private bool canJump;
    // private bool movementMade;
    private Movement currentState;

    private bool jumpInput;
    private bool isGrounded;
    private bool wasGrounded;

    private List<Collider> collisions = new List<Collider>();

    void Start()
    {
        transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        nextDest = GameManager.Instance.getFirstCube();
        canJump = true;
        jumpInput = false;
        // movementMade= false;
        currentState = Movement.WAITING;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && canJump && GameManager.Instance.GetNumCurrentJumps() < GameManager.Instance.GetNumJumps())
        //{
        //    canJump = false;
        //    jumpInput = true;
        //    GameManager.Instance.AddOneMoreJump();
        //    Debug.Log("Numero de saltos actuales es " + GameManager.Instance.GetNumCurrentJumps());
        //    JumpingAndLanding();
        //}


        //Vector3 boardOrient = arduinoConnection.GetOrientationFromBoard();
        //if(boardOrient.x <= -90.0f && canJump && GameManager.Instance.GetNumCurrentJumps() < GameManager.Instance.GetNumJumps())
        //{
        //    Debug.Log("La orientación en x del board es " + boardOrient.x);
        //    canJump = false;
        //    jumpInput = true;
        //    GameManager.Instance.AddOneMoreJump();
        //    Debug.Log("Numero de saltos actuales es " + GameManager.Instance.GetNumCurrentJumps());
        //    JumpingAndLanding();
        //}

        if (currentState == Movement.MOVE_DONE && canJump && GameManager.Instance.GetNumCurrentJumps() < GameManager.Instance.GetNumJumps())
        {
            
            canJump = false;
            currentState = Movement.WAITING;
            Debug.Log("WAITING");
            jumpInput = true;
            GameManager.Instance.AddOneMoreJump();
            //Debug.Log("Numero de saltos actuales es " + GameManager.Instance.GetNumCurrentJumps());
            JumpingAndLanding();
        }
    }

    private void FixedUpdate()
    {
        animator.SetBool("Grounded", isGrounded);
        if(jumpInput)
        {
            JumpWithPhysics();
            jumpInput = false;
        }
        wasGrounded = isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chest"))
        {
            GameManager.Instance.AddPoints(pointsPerChest);
            Destroy(other.gameObject);
        }
        else
        {
            rb.AddForce(-currentForce, ForceMode.Force);
            GameManager.Instance.deleteCube();

            if (GameManager.Instance.GetNumCurrentJumps() + 1 < GameManager.Instance.GetNumJumps())
            {
                nextDest = GameManager.Instance.getFirstCube();
                Destroy(other.gameObject);
            }
            else if (GameManager.Instance.GetNumCurrentJumps() + 1 == GameManager.Instance.GetNumJumps())
            {
                GameObject island = GameManager.Instance.GetIsland();
                nextDest = island.GetComponent<Island>().GetJumpDest();
            }
        }
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

        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!collisions.Contains(collision.collider))
                {
                    collisions.Add(collision.collider);
                }
                isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!canJump && rb.velocity.x <= 0.1f && rb.velocity.z <= 0.1f) canJump = true;

        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            isGrounded = true;
            if (!collisions.Contains(collision.collider))
            {
                collisions.Add(collision.collider);
            }
        }
        else
        {
            if (collisions.Contains(collision.collider))
            {
                collisions.Remove(collision.collider);
            }
            if (collisions.Count == 0) { isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collisions.Contains(collision.collider))
        {
            collisions.Remove(collision.collider);
        }
        if (collisions.Count == 0) { isGrounded = false; }
    }

    private void JumpingAndLanding()
    {
        if (!wasGrounded && isGrounded)
        {
            animator.SetTrigger("Land");
        }

        if (!isGrounded && wasGrounded)
        {
            animator.SetTrigger("Jump");
        }
    }

    private void JumpWithPhysics()
    {
        Vector3 dir = nextDest.transform.position - transform.position;
        impulseH = dir.magnitude;
        Vector3 force = dir.normalized * impulseH;
        currentForce = force;
        rb.AddForce(force, ForceMode.Impulse);
        rb.AddForce(Vector3.up * impulseV, ForceMode.Impulse);
    }

    public void CheckIfCanJump(Quaternion mobileOrient)
    {
        Vector3 orient = mobileOrient.eulerAngles;

        Debug.Log("El vector en angulos es: " + orient);
      
        if ((orient.x >= 350.0f || orient.x < 90.0f) && currentState == Movement.RESTART)
        {
            currentState = Movement.MOVE_DONE;
            Debug.Log("MOVE_DONE");
        }
        else if((orient.x < (350.0f -30.0f) && orient.x >180.0f)&& currentState == Movement.WAITING)
        {
            currentState = Movement.RESTART;
            Debug.Log("RESTART");
        }
    }
}
