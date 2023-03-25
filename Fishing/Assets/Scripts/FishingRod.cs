using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [SerializeField] private GameObject baitGO;
    [SerializeField] private Rigidbody rbFirstRopePart;
    [SerializeField] private float speed;
    [SerializeField] private MeshRenderer baitRenderer;
    [SerializeField] private ColorFishingRod baitColors;

    private bool fishAtBait = false;
    private bool addForce = false;
    private HingeJoint fixedJoint;

    private void Start()
    {
        baitRenderer.material.color = baitColors.noFishColor;
    }
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
    public void SetFishAtBait(bool b)
    { 
        fishAtBait = b;
        if (fishAtBait)
        {
            baitRenderer.material.color = baitColors.fishColor;
        }
        else baitRenderer.material.color = baitColors.noFishColor;
    }

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
