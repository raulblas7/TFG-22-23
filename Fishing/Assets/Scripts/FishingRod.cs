using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [SerializeField] private GameObject baitGO;
    [SerializeField] private Rigidbody rbFirstRopePart;
    [SerializeField] private float speed;

    private bool fishAtBait = false;
    private bool addForce = false;
    private HingeJoint fixedJoint;

    void Update()
    {
        if(Input.GetKey(KeyCode.Space)) {
            addForce= true;
        }
        else addForce= false;
    }

    private void FixedUpdate()
    {
        if(addForce)
        {
            rbFirstRopePart.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            rbFirstRopePart.AddForce(Vector3.up * speed, ForceMode.Force);
        }
    }

    public bool HasFishAtBait() { return fishAtBait; }
    public void SetFishAtBait(bool b) { fishAtBait = b; }

    public void AddComponentToBait(Rigidbody fishRb)
    {
        fixedJoint = baitGO.AddComponent<HingeJoint>();
        fixedJoint.connectedBody = fishRb;
    }

    public void QuitComponentToBait()
    {
        Destroy(fixedJoint);
    }

    public bool IsFishingRodGoingUp()
    {
        return addForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            SetFishAtBait(false);
        }
    }
}
