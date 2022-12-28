using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCarCollisions : MonoBehaviour
{
    [SerializeField] private Collider collider3d;
    [SerializeField] private FollowPath followPath;
    [SerializeField] private Rigidbody rbCar;
    [SerializeField] private float collisionForce;

    private void Start()
    {
        
    }

    //comprobamos colisiones
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("onCollisionEnter");

        //float upFace = transform.position.y + (transform.lossyScale.y / 2.0f);
        //float downFace = collision.gameObject.transform.position.y - (collision.gameObject.transform.lossyScale.y / 2.0f);

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Collider collisionCollider = collision.gameObject.GetComponent<Collider>();
            float upFace = collider3d.bounds.max.y;
            float downFace = collisionCollider.bounds.min.y;

            //float leftOrRight = AngleDir(transform.forward, followPath.getDir(), Vector3.up);

            if (upFace + 0.5f >= downFace && upFace - 0.5f <= downFace)
            {
                //Colision por arriba
                Debug.Log("colision por arriba");
                float floorPos = transform.position.y - (transform.lossyScale.y / 2.0f);
                transform.localScale = new Vector3(transform.localScale.x, 0.3f, transform.localScale.z);
                transform.Translate(Vector3.forward * 2f);
                collision.gameObject.GetComponent<FallingObstacle>().SetObstacleState(ObstacleState.DOWN);
            }
            else
            {
                rbCar.AddForce(Vector3.forward * (-1) * collisionForce, ForceMode.Impulse);
            }
        }
        //if (collision.gameObject.CompareTag("RotatingObstacle"))
        //{
        //    transform.parent = collision.gameObject.transform;
        //}
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");
        //if (collision.gameObject.CompareTag("RotatingObstacle"))
        //{
        //    transform.parent = null;
        //}
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
