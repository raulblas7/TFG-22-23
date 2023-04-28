using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum Movement
    {
        MOVE_DONE = 0,
        WAITING,
        RESTART
    }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float impulseH;
    [SerializeField] private float impulseV;

    [SerializeField] private int pointsPerChest = 100;

    [SerializeField] private Animator animator = null;

    [SerializeField] private UIExerciseSlider uIExerciseSlider;

    //[SerializeField] private ArduinoConnection arduinoConnection;

    private Vector3 currentForce;
    private GameObject nextDest;
    private bool canJump;
    // private bool movementMade;
    private Movement currentState;

    private bool jumpInput;
    private bool isGrounded;
    private bool wasGrounded;

    private float angle;

    private List<Collider> collisions = new List<Collider>();

    void Start()
    {
        angle = GameManager.Instance.GetAngleToDoIt();
        InitPlayer();
    }

    void Update()
    {
        if (currentState == Movement.MOVE_DONE && canJump && GameManager.Instance.GetNumCurrentJumps() < GameManager.Instance.GetNumJumps())
        {
            canJump = false;
            currentState = Movement.WAITING;
            jumpInput = true;
            GameManager.Instance.AddOneMoreJump();
            Debug.Log("Numero de saltos actuales es " + GameManager.Instance.GetNumCurrentJumps());
            JumpingAndLanding();
        }
    }

    private void FixedUpdate()
    {
        animator.SetBool("Grounded", isGrounded);
        if (jumpInput)
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
        else if (other.gameObject.CompareTag("FinalJump"))
        {
            rb.AddForce(-currentForce, ForceMode.Force);
            GameManager.Instance.deleteCube();
            GameManager.Instance.UpdateCurrentSerie();
            if (!GameManager.Instance.GetUIManager().IsPanelWinningEnabled())
            {
                GameManager.Instance.RestartGame();
                Invoke("ResetPlayer", 1.0f);
            }
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
        else if (collision.gameObject.CompareTag("Water"))
        {
            GameManager.Instance.QuitPoints(50);
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
        impulseH = dir.magnitude - 0.5f;
        Vector3 force = dir.normalized * impulseH;
        currentForce = force;
        rb.AddForce(force, ForceMode.Impulse);
        rb.AddForce(Vector3.up * impulseV, ForceMode.Impulse);
    }

    public void CheckIfCanJump(Quaternion mobileOrient)
    {
        Vector3 orient = mobileOrient.eulerAngles;

        Debug.Log("El vector en angulos es: " + orient);

        if (orient.x >= 270.0f && orient.x < 360.0f)
        {
            uIExerciseSlider.UpdateSlider(orient.x);
        }

        if ((orient.x >= 270.0f + angle || orient.x < 90.0f) && currentState == Movement.RESTART)
        {
            currentState = Movement.MOVE_DONE;
        }
        else if ((orient.x < 287.0f && orient.x > 180.0f) && currentState == Movement.WAITING)
        {
            currentState = Movement.RESTART;
        }

        GameManager.Instance.WriteData(orient.ToString());
    }

    private void InitPlayer()
    {
        isGrounded= true;
        wasGrounded= false;
        transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        nextDest = GameManager.Instance.getFirstCube();
        canJump = true;
        jumpInput = false;
        // movementMade= false;
        currentState = Movement.WAITING;
    }
    private void ResetPlayer()
    {
        InitPlayer();
        Destroy(GameManager.Instance.GetIsland());
    }
}
