using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInMenu : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float impulseV;
    [SerializeField] private Animator animator = null;
    
    private bool canJump;
    private bool isGrounded;
    private bool wasGrounded;

    private List<Collider> collisions = new List<Collider>();

    void Start()
    {
        InvokeRepeating("JumpWithPhysics", 0.0f, 2.0f);
    }

    private void JumpWithPhysics()
    {
        animator.SetBool("Grounded", isGrounded);
        if (canJump)
        {
            JumpingAndLanding();
            rb.AddForce(Vector3.up * impulseV, ForceMode.Impulse);
        }
        wasGrounded = isGrounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
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

    private void OnCollisionExit(Collision collision)
    {
        if (collisions.Contains(collision.collider))
        {
            collisions.Remove(collision.collider);
        }
        if (collisions.Count == 0) { isGrounded = false; }
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
}
