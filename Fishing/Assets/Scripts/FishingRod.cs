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
    private FixedJoint fixedJoint;

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
    public void SetFishAtBait() { fishAtBait = !fishAtBait; }

    public void AddComponentToBait(Rigidbody fishRb)
    {
        fixedJoint = baitGO.AddComponent<FixedJoint>();
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
}
