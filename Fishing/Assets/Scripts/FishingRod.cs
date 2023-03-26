using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    private enum Movement
    {
        UP = 0,
        DOWN,
        MOVEDONE,
    }

    [SerializeField] private GameObject baitGO;
    [SerializeField] private Rigidbody rbFirstRopePart;
    [SerializeField] private float speed;
    [SerializeField] private MeshRenderer baitRenderer;
    [SerializeField] private ColorFishingRod baitColors;

    private bool fishAtBait = false;
    private bool addForce = false;
    private HingeJoint fixedJoint;
    private Movement state = Movement.UP;
    private float playerAngle;

    private void Start()
    {
        playerAngle = GameManager.Instance.GetGameAngle();
        baitRenderer.material.color = baitColors.noFishColor;
    }
    void Update()
    {
        if (GameManager.Instance.IsGameActive())
        {
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    addForce = true;
            //}
            //else addForce = false;

            if(state == Movement.MOVEDONE)
            {
                addForce = true;
            }
            else addForce = false;
        }

    }

    private void FixedUpdate()
    {
        if (addForce)
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

    public void CheckIfApplyForce(Quaternion orientQuaternion)
    {
        Vector3 orient = orientQuaternion.eulerAngles;

        if (state == Movement.UP) Debug.Log("UP");
        else if (state == Movement.MOVEDONE) Debug.Log("MOVE DONE");
        else Debug.Log("DOWN");

        if ((orient.x >= 270.0f + playerAngle || orient.x < 90) && state == Movement.UP)
        {
            state = Movement.MOVEDONE;
        }
        if((orient.x <= 280.0f && orient.x > 90) && state == Movement.MOVEDONE)
        {
            state = Movement.UP;
        }

        GameManager.Instance.WriteData(orient.ToString());
    }
}
