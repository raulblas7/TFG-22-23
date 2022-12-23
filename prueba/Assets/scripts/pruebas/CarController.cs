using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float verticalInput;

    private float currentBreakForce;
    private bool isBreaking;

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

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
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
        //float angle = Vector3.Angle(followPath.getDir());
        Vector3 auxDir = followPath.getDir();

        float angle = Vector3.Angle(followPath.getDir(), carRb.rotation * Vector3.forward);
        
        Vector3 dirComb = Vector3.zero;

        //Vector3 vec = Quaternion.Euler(0f, transform.rotation.y, 0f) * Vector3.left;
        Vector3 vec = Quaternion.AngleAxis(transform.rotation.y, Vector3.up) * Vector3.left;

        //Vector3 v = Quaternion.LookRotation(followPath.getDir()).eulerAngles;


        Debug.Log(angle);
        //if(auxDir.x < 0f)
        //{
        //    dirComb += Vector3.right;
        //}
        //else if (auxDir.x > 0f)
        //{
        //    dirComb += Vector3.left;
        //}
        //if(auxDir.z < 0f)
        //{
        //    dirComb += Vector3.back;
        //}
        //else if(auxDir.z > 0f)
        //{
        //    dirComb += Vector3.forward;
        //}
        //
        //angle = Vector3.Angle(followPath.getDir(), dirComb);
        //
        ////transform.rotation.y = angle;
        ////currentDir = followPath.getDir();
        ////currentDir = currentDir.normalized;
        ////Debug.Log(angle);
        ////if (currentDir.x <= -0.5f || currentDir.x >= 0.5f) currentSteerAngle = maxSteerAngle * currentDir.x;
        ////currentSteerAngle = maxSteerAngle * ((currentDir.x + currentDir.z) / 2);
        currentSteerAngle = angle;
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

}
