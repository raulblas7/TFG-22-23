using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    private enum Movement
    {
        MOVE_DONE = 0,
        WAITING,
        RESTART
    }

    private Movement currentState;
    private float verticalInput;

    private float currentBreakForce;
    //private bool isBreaking = true;

    private float currentSteerAngle;

    private Vector3 currentDir;

    private int rep = 0;

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

    [SerializeField] private CheckPointInfo checkPoint1;
   
    [SerializeField] private Rigidbody carRb;

    [SerializeField] private float deadzoneAngle;

    [SerializeField] private UIExerciseSlider uIExerciseSlider;

    private const float INITIAL_DEGREES = 350.0f;

    //private bool alreadyAccelerate = false;


    private void Start()
    {
        transform.position = checkPoint1.gameObject.transform.position;
        currentState = Movement.WAITING;
        if (checkPoint1 != null)
        {
            Debug.Log("No es null");
            followPath.SetDest(checkPoint1.GetNextPointInDest());
        }
        else
        {
            Debug.Log("Por algún motivo es null");
        }
    }

    private void HandleCarFunctionality()
    {
        if (!GameManager.Instance.GetUIManager().IsPanelWaitingEnabled() && !GameManager.Instance.GetUIManager().IsPanelWinningEnabled()
            && !GameManager.Instance.GetUIManager().IsPanelFinishSerieEnabled() && !GameManager.Instance.GetUIManager().IsPanelDisconectingEnabled())
        {
            HandleMotor();
        }
        else if (GameManager.Instance.GetUIManager().IsPanelWinningEnabled()
            || GameManager.Instance.GetUIManager().IsPanelFinishSerieEnabled() || GameManager.Instance.GetUIManager().IsPanelDisconectingEnabled())
        {
            FinishBreaking();
        }
    }

    private void FixedUpdate()
    {
        //GetInput();
        //if (!GameManager.Instance.GetUIManager().IsPanelWaitingEnabled() && !GameManager.Instance.GetUIManager().IsPanelWinningEnabled() 
        //    && !GameManager.Instance.GetUIManager().IsPanelFinishSerieEnabled() && !GameManager.Instance.GetUIManager().IsPanelDisconectingEnabled())
        //{
        //    HandleMotor();
        //    HandleSteering();
        //    UpdateWheels();
        //}
        //else if(GameManager.Instance.GetUIManager().IsPanelWinningEnabled() 
        //    || GameManager.Instance.GetUIManager().IsPanelFinishSerieEnabled() || GameManager.Instance.GetUIManager().IsPanelDisconectingEnabled())
        //{
        //    FinishBreaking();
        //}

        HandleSteering();
        UpdateWheels();
    }

    public void FinishBreaking()
    {
        currentBreakForce = breakForce;
        ApplyBreaking();
    }

    private void HandleMotor()
    {
        //if (!alreadyAccelerate && !isBreaking)
        //{
        //    Debug.Log("Acelero");
        //    frontLeftWheelCollider.motorTorque = motorForce;
        //    frontRightWheelCollider.motorTorque = motorForce;
        //    alreadyAccelerate = true;
        //}
        //else if(alreadyAccelerate && !isBreaking)
        //{
        //    frontLeftWheelCollider.motorTorque = 0.0f;
        //    frontRightWheelCollider.motorTorque = 0.0f;
        //}

        if(frontLeftWheelCollider.brakeTorque > 0)
        {
            frontLeftWheelCollider.brakeTorque = 0.0f;
            frontRightWheelCollider.brakeTorque = 0.0f;
            rearLeftWheelCollider.brakeTorque = 0.0f;
            rearRightWheelCollider.brakeTorque = 0.0f;
        }

        frontLeftWheelCollider.motorTorque = motorForce;
        frontRightWheelCollider.motorTorque = motorForce;

        //if (isBreaking)
        //{
        //    currentBreakForce = breakForce;
        //}
        //else
        //{
        //    currentBreakForce = 0.0f;
        //}
        //ApplyBreaking();
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

        if(orient.x <= 360.0f && orient.x >= 270.0f)
        {
            uIExerciseSlider.UpdateSlider(orient.x);
        }

        if (/*isBreaking &&*/ orient.x >= 270.0f && orient.x <= INITIAL_DEGREES - GameManager.Instance.GetAngleToDoIt() && currentState == Movement.RESTART)
        {
            currentState = Movement.MOVE_DONE;
            //isBreaking = false;
            rep++;
        }
        else if(/*!isBreaking &&*/ orient.x >= INITIAL_DEGREES - 10.0f && currentState == Movement.WAITING)
        {
            Debug.Log("Move restart");
            currentState = Movement.RESTART;
            //isBreaking = true;
            rep++;
        }
        if (rep == 2)
        {
            GameManager.Instance.AddReps();
            rep = 0;
        }
        if (currentState == Movement.MOVE_DONE)
        {
            Debug.Log("Move doneeee");
            HandleCarFunctionality();
            currentState = Movement.WAITING;
        }
        GameManager.Instance.WriteData(orient.ToString());
    }

    public void SetAlreadyAccelerate(bool acc)
    {
        //alreadyAccelerate = acc;
    }

    public void SetIsBreaking(bool ib)
    {
        //isBreaking = ib;
    }

    public bool IsBreaking()
    {
        return /*isBreaking*/ false;
    }

    public void setCurrentStateToWait()
    {
        currentState = Movement.WAITING;
    }
}
