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
    [SerializeField] private CarController carController;

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
                carController.SetAlreadyAccelerate(false);
                carController.SetIsBreaking(false);
            }
        }
        if (collision.gameObject.CompareTag("DeadObstacle"))
        {
            SetPositionToLastCheckPoint();
            GameManager.Instance.LessPoints(50);
            GameManager.Instance.AddReps();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadObstacle") || other.gameObject.CompareTag("Wall"))
        { 
            SetPositionToLastCheckPoint();
            GameManager.Instance.LessPoints(50);
            GameManager.Instance.AddReps();
        }
        if (other.gameObject.CompareTag("Tyres"))
        {
            //other.gameObject.SetActive(false);
            GameManager.Instance.AddPoints(100);
            GameManager.Instance.AddReps();
        }
    }

    public void SetPositionToLastCheckPoint()
    {
        rbCar.Sleep();
        transform.position = playerCheckPoints.GetCheckPointInfo().GetTransform().position;
        transform.rotation = playerCheckPoints.GetCheckPointInfo().GetTransform().rotation;
        rbCar.WakeUp();

        carController.setCurrentStateToWait();
        carController.FinishBreaking();
        followPath.SetDest(playerCheckPoints.GetCheckPointInfo().GetNextPointInDest());
    }

    public void SetPositionToNextCheckpoint(CheckPointInfo nextCheck)
    {
        rbCar.Sleep();
        transform.position = nextCheck.gameObject.transform.position;
        transform.rotation = nextCheck.gameObject.transform.rotation;
        rbCar.WakeUp();

        carController.setCurrentStateToWait();
        carController.FinishBreaking();
        followPath.SetDest(nextCheck.GetNextPointInDest());
    }

    private void GoBigAgain()
    {
        transform.localScale = originalScale;
    }
}
