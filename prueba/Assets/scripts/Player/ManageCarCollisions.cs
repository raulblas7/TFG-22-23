using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCarCollisions : MonoBehaviour
{
    [SerializeField] private Collider collider3d;
    [SerializeField] private FollowPath followPath;
    [SerializeField] private Rigidbody rbCar;
    [SerializeField] private float collisionForce;
    [SerializeField] private PlayerCheckPoints playerCheckPoints;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    //comprobamos colisiones
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("onCollisionEnter");

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Collider collisionCollider = collision.gameObject.GetComponent<Collider>();
            float upFace = collider3d.bounds.max.y;
            float downFace = collisionCollider.bounds.min.y;

            if (upFace + 0.5f >= downFace && upFace - 0.5f <= downFace)
            {
                //Colision por arriba
                Debug.Log("colision por arriba");
                transform.localScale = new Vector3(transform.localScale.x, 0.3f, transform.localScale.z);
                transform.Translate(Vector3.forward * 2f);
                collision.gameObject.GetComponent<FallingObstacle>().SetObstacleState(ObstacleState.DOWN);

                Invoke("GoBigAgain", 4.0f);
            }
            else
            {
                rbCar.AddForce(Vector3.forward * (-1) * collisionForce, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadObstacle"))
        { 
            SetPositionToLastCheckPoint();
        }
    }

    public void SetPositionToLastCheckPoint()
    {
        rbCar.Sleep();
        transform.position = playerCheckPoints.GetCheckPointInfo().GetTransform().position;
        transform.rotation = playerCheckPoints.GetCheckPointInfo().GetTransform().rotation;
        rbCar.WakeUp();
    }

    private void GoBigAgain()
    {
        transform.localScale = originalScale;
    }
}
