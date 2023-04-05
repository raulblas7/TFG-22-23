using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private float verticalInput;

    private float currentBreakForce;
    private bool isBreaking = true;

    private float currentSteerAngle;

    private Vector3 currentDir;

    [SerializeField] private FollowPath followPath;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    [SerializeField] private Rigidbody carRb;

    [SerializeField] private float deadzoneAngle;

    private const float INITIAL_DEGREES = 350.0f;

    private bool gameFinished = false;

    private void FixedUpdate()
    {
        //GetInput();
        if (!GameManager.Instance.GetUIManager().IsPanelWaitingEnabled() && !GameManager.Instance.GetUIManager().IsPanelWinningEnabled())
        {
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }
        else if(!gameFinished && GameManager.Instance.GetUIManager().IsPanelWinningEnabled())
        {
            gameFinished = true;
            currentBreakForce= breakForce;
            ApplyBreaking();
        }
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = /*verticalInput * */motorForce;
        frontRightWheelCollider.motorTorque = /*verticalInput **/ motorForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }
    private void HandleSteering()
    {
        Vector3 auxDir = followPath.getDir();
        Vector3 rotation = carRb.rotation.eulerAngles;

        float leftOrRight = AngleDir(transform.forward, auxDir, Vector3.up);
        float angle = Vector3.Angle(followPath.getDir(), transform.forward);
        //Debug.Log("LeftOrRight " + leftOrRight);
       // if (angle >= -deadzoneAngle && angle <= deadzoneAngle) angle = 0;
       // Debug.Log("angulo2 " + angle);

        currentSteerAngle = angle * leftOrRight;
        //Debug.Log("angulo3 " + currentSteerAngle);
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }


    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        if (verticalInput == 0f) isBreaking = true;
        else isBreaking = false;
    }
    private float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f + deadzoneAngle)
        {
            return 1f;
        }
        else if (dir < 0f - deadzoneAngle)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    // Input desde el movil
    public void GetRotationFromDevice(Quaternion mobileOrient)
    {
        Vector3 orient = mobileOrient.eulerAngles;

        Debug.Log("El vector en angulos es: " + orient);

        if (orient.x >= 270.0f && orient.x <= INITIAL_DEGREES - GameManager.Instance.GetAngleToDoIt())
        {
            isBreaking = false;
        }
        else
        {
            isBreaking = true;
        }
        GameManager.Instance.WriteData(orient.ToString());
    }
}
