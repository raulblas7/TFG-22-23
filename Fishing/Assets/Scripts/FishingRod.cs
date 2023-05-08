using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    private enum Movement
    {
        UP = 0,
        DOWN,
        MOVE_DONE,
    }

    [SerializeField] private GameObject baitGO;
    [SerializeField] private Rigidbody rbFirstRopePart;
    [SerializeField] private float speed;
    [SerializeField] private MeshRenderer baitRenderer;
    [SerializeField] private ColorFishingRod baitColors;
    [SerializeField] private UIExerciseSlider slider;

    [SerializeField] Transform cube;

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

            if(state == Movement.MOVE_DONE)
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

 

    public void CheckIfApplyForce(Quaternion orientQuaternion)
    {
        Vector3 orient = orientQuaternion.eulerAngles;
        cube.rotation = orientQuaternion;

        //transformamos la orientacion en un rango de 0 a 180 grados
        float orientZ = (cube.forward.z + 1.0f) / 2.0f * 180.0f;

        if (state == Movement.UP) Debug.Log("UP");
        else if (state == Movement.MOVE_DONE) Debug.Log("MOVE DONE");
        else Debug.Log("DOWN");


        // le pasamos el angulo al slider
        if (orient.x >= 270.0f && orient.x < 355.0f)
        {
            slider.UpdateSlider(orientZ);

        }

        if ((orientZ <= 90.0f - playerAngle && (orient.x >= 270.0f && orient.x < 355.0f)) && state == Movement.UP)
        {
           state = Movement.MOVE_DONE;
            
        }

        else if ((orientZ > 90.0f - 10.0f && (orient.x >= 270.0f && orient.x < 355.0f)) && state == Movement.MOVE_DONE)
        {
            state = Movement.UP;
            // Debug.Log("UP");
        }

        GameManager.Instance.WriteData(orient.ToString());
    }


    //original
    //public void CheckIfApplyForce(Quaternion orientQuaternion)
    //{
    //    Vector3 orient = orientQuaternion.eulerAngles;

    //    if (state == Movement.UP) Debug.Log("UP");
    //    else if (state == Movement.MOVEDONE) Debug.Log("MOVE DONE");
    //    else Debug.Log("DOWN");

    //    //le pasamos la info al slider
    //    if (orient.x >= 270 && orient.x <= 360)
    //    {
    //        slider.UpdateSlider(orient.x);
    //    }

    //    if ((orient.x >= 270.0f + playerAngle || orient.x < 90) && state == Movement.UP)
    //    {
    //        state = Movement.MOVEDONE;
    //    }
    //    if ((orient.x <= 280.0f && orient.x > 90) && state == Movement.MOVEDONE)
    //    {
    //        state = Movement.UP;
    //    }

    //    GameManager.Instance.WriteData(orient.ToString());
    //}
}
