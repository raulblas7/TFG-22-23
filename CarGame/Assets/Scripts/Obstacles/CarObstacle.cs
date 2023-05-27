
using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float vel;
    [SerializeField] float waitTime = 0;

    private bool move = false;
    private float currentTime = 0;


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= waitTime)
        {
            move = true;
        }
    }

    private void FixedUpdate()
    {
        if (move)
        {
            rb.AddForce(transform.forward * vel, ForceMode.Force);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Destroy(this.gameObject);
        }
    }
}
