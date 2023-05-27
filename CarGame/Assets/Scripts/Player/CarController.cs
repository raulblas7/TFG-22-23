
using UnityEngine;

public enum Movement
{
    MOVE_DONE = 0,
    WAITING,
    RESTART
}

public class CarController : MonoBehaviour
{

    private const float INITIAL_DEGREES = 350.0f;
    private Movement currentState;
    private float currentBreakForce;
    private float currentSteerAngle;

    private Vector3 dir;
    private bool moveTranslate;
    private float moveSpeed;

    //[SerializeField] private FollowPath followPath;

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

    //[SerializeField] private CheckPointInfo checkPoint1;
   
    [SerializeField] private Rigidbody carRb;

    [SerializeField] private float deadzoneAngle;

    [SerializeField] private UIExerciseSlider uIExerciseSlider;

    [SerializeField] private Vector3 posIni;


    private void Start()
    {
        //transform.position = checkPoint1.gameObject.transform.position;
        currentState = Movement.WAITING;

        transform.position = posIni;
        dir = Vector3.right;
        moveTranslate = true;
        carRb.Sleep();

        switch(GameManager.Instance.GetDifficulty())
        {
            case 0:
                moveSpeed = 1.5f;
                break;
            case 1:
                moveSpeed = 2.1f;
                break; 
            case 2:
                moveSpeed = 2.8f;
                break;
        }

    }

    private void Update()
    {
        if (!GameManager.Instance.GetUIManager().IsPanelWaitingEnabled() && !GameManager.Instance.GetUIManager().IsPanelWinningEnabled()
            && !GameManager.Instance.GetUIManager().IsPanelFinishSerieEnabled() && !GameManager.Instance.GetUIManager().IsPanelDisconectingEnabled() 
            && moveTranslate)
        {
            transform.Translate(dir * moveSpeed * Time.deltaTime);  
        }
    }

    private void HandleCarFunctionality()
    {
        carRb.WakeUp();
        carRb.velocity = Vector3.zero;
        moveTranslate = false; 

        if (!GameManager.Instance.GetUIManager().IsPanelWaitingEnabled() && !GameManager.Instance.GetUIManager().IsPanelWinningEnabled()
            && !GameManager.Instance.GetUIManager().IsPanelFinishSerieEnabled() && !GameManager.Instance.GetUIManager().IsPanelDisconectingEnabled())
        {
            HandleMotor();
        }
    }

    private void FixedUpdate()
    {
        if (!moveTranslate)
        {
            //HandleSteering();
            UpdateWheels();
        }
    }

    public void FinishBreaking()
    {
        currentBreakForce = breakForce;
        ApplyBreaking();
    }

    private void HandleMotor()
    {
        if(frontLeftWheelCollider.brakeTorque > 0)
        {
            frontLeftWheelCollider.brakeTorque = 0.0f;
            frontRightWheelCollider.brakeTorque = 0.0f;
            rearLeftWheelCollider.brakeTorque = 0.0f;
            rearRightWheelCollider.brakeTorque = 0.0f;
        }

        frontLeftWheelCollider.motorTorque = motorForce;
        frontRightWheelCollider.motorTorque = motorForce;
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
        //Vector3 auxDir = followPath.getDir();
        //Vector3 rotation = carRb.rotation.eulerAngles;
        //
        //float leftOrRight = AngleDir(transform.forward, auxDir, Vector3.up);
        //float angle = Vector3.Angle(followPath.getDir(), transform.forward);
        //
        //currentSteerAngle = angle * leftOrRight;
        //frontLeftWheelCollider.steerAngle = currentSteerAngle;
        //frontRightWheelCollider.steerAngle = currentSteerAngle;
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
            uIExerciseSlider.UpdateSlider(orient.x, currentState);
        }

        if (orient.x >= 270.0f && orient.x <= INITIAL_DEGREES - GameManager.Instance.GetAngleToDoIt() && currentState == Movement.RESTART)
        {
            currentState = Movement.MOVE_DONE;
        }
        else if(orient.x >= INITIAL_DEGREES - GameManager.Instance.GetAngleMinToDoIt() && currentState == Movement.WAITING)
        {
            currentState = Movement.RESTART;
        }

        if (currentState == Movement.MOVE_DONE)
        {
            HandleCarFunctionality();
            currentState = Movement.WAITING;
        }
        GameManager.Instance.WriteData(orient.ToString());
    }

    public void setCurrentStateToWait()
    {
        currentState = Movement.WAITING;
    }

    public void SetDir(Vector3 v)
    {
        dir = v;
    }

    public Vector3 GetDir()
    {
        return dir;
    }

    public void RestartCar()
    {
        if(GameManager.Instance.GetUIManager().IsParkingTextActivate())
        {
            GameManager.Instance.GetUIManager().ActivateParkingText(false);
        }
        if(!carRb.IsSleeping()) carRb.Sleep();
        transform.position = posIni;
        setCurrentStateToWait();
        FinishBreaking();
        moveTranslate = true;
    }

    public void Parked()
    {
        carRb.Sleep();
        GameManager.Instance.GetUIManager().ActivateParkingText(true);
        Invoke("RestartCar", 1.5f);
    }
}
