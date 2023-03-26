using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum Sides
{
    Left,
    Right,
    Up,
    Down,
    Back,
    Forward

}
public class Fish : MonoBehaviour
{

    // [SerializeField] private float changeDirTime;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float vel;
    [SerializeField] private FishVision fishVision;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private int layerIndexWhenCatched;
    [SerializeField] private int layerIndexWhenNOTCatched;

    private float waitTimeInRod;
    private float lifeTime;
    private bool goToFishingRod = false;
    private bool alreadyAtFishingRod = false;
    private FishingRod fishingRod;
    private FishInstantiator fishInstantiator;
    private Vector3 dir;
    private int points = 0;

    void Start()
    {
        waitTimeInRod = GameManager.Instance.GetMaxTime();
        Invoke("DeadFish", lifeTime);
        // InvokeRepeating("ChangeDir", 0f, changeDirTime);
    }

    void Update()
    {
        //if (GameManager.Instance.IsGameActive())
        {

            if (!goToFishingRod && fishVision.IsSawFishingRod())
            {
                // Ir hacia la ca�a
                GoToFishingRod();
            }
        }
    }

    private void FixedUpdate()
    {
        //if (GameManager.Instance.IsGameActive())
        {

            if (!alreadyAtFishingRod) rb.AddForce(transform.right * vel);
        }
    }

    private void DeadFish()
    {
        if (!alreadyAtFishingRod)
        {
            this.gameObject.layer = layerIndexWhenCatched;
            //fishInstantiator.DeleteFishFromList(this);
        }
    }


    private void GoToFishingRod()
    {
        Transform fishingRodPosTR = fishVision.GetFishingRodTr();
        dir = fishingRodPosTR.position - transform.position;

        MakeRotationInDir(dir);
        goToFishingRod = true;

        //EditorApplication.isPaused = true;
    }

    private void MakeRotationInDir(Vector3 d)
    {
        Quaternion q = Quaternion.FromToRotation(transform.right, d);
        Vector3 aux;
        aux = q.eulerAngles;
        aux.x = transform.rotation.eulerAngles.x;

        aux.y = transform.rotation.eulerAngles.y + aux.y;
        aux.z = transform.rotation.eulerAngles.z - aux.z;
        q.eulerAngles = aux;
        rb.MoveRotation(q);
    }

    private void UTurn(Sides side)
    {
        Quaternion q;
        Vector3 aux;
        int angle = 0;
        switch (side)
        {
            case Sides.Left:
                angle = Random.Range(-60, 60);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

                angle = Random.Range(-60, 60);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux.z = q.eulerAngles.z;
                aux.x = transform.rotation.eulerAngles.x;


                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Right:
                angle = Random.Range(120, 230);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

                angle = Random.Range(-60, 60);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux.z = q.eulerAngles.z;
                aux.x = transform.rotation.eulerAngles.x;

                q.eulerAngles = aux;
                rb.MoveRotation(q);

                break;
            case Sides.Up:

                angle = Random.Range(10, 60);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.y = transform.rotation.eulerAngles.y;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Down:
                angle = Random.Range(-70, -20);

                q = Quaternion.AngleAxis(angle, transform.forward);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.y = transform.rotation.eulerAngles.y;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Forward:
                angle = Random.Range(180, 360);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

                angle = Random.Range(-60, 60);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux.z = q.eulerAngles.z;
                aux.x = transform.rotation.eulerAngles.x;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
            case Sides.Back:
                angle = Random.Range(30, 150);
                q = Quaternion.AngleAxis(angle, transform.up);

                aux = q.eulerAngles;
                aux.x = transform.rotation.eulerAngles.x;
                aux.z = transform.rotation.eulerAngles.z;

                angle = Random.Range(-60, 60);
                q = Quaternion.AngleAxis(angle, transform.forward);

                aux.z = q.eulerAngles.z;
                aux.x = transform.rotation.eulerAngles.x;

                q.eulerAngles = aux;
                rb.MoveRotation(q);
                break;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Right"))
        {
            UTurn(Sides.Right);
        }
        else if (collision.gameObject.CompareTag("Left"))
        {
            UTurn(Sides.Left);
        }
        else if (collision.gameObject.CompareTag("Up"))
        {
            UTurn(Sides.Up);
        }
        else if (collision.gameObject.CompareTag("Down"))
        {
            UTurn(Sides.Down);
        }
        else if (collision.gameObject.CompareTag("Back"))
        {
            UTurn(Sides.Back);
        }
        else if (collision.gameObject.CompareTag("Forward"))
        {
            UTurn(Sides.Forward);
        }
        else if (collision.gameObject.CompareTag("FishingRod") && fishVision.IsSawFishingRod())
        {
            if (!fishingRod.HasFishAtBait() && gameObject.layer != layerIndexWhenCatched)
            {
                Debug.Log("pescado");
                alreadyAtFishingRod = true;
                fishingRod.SetFishAtBait(true);
                fishingRod.AddComponentToBait(rb);
                this.gameObject.layer = layerIndexWhenCatched;

                Invoke("QuitFromFishingRod", waitTimeInRod);
                GameManager.Instance.GetUIManager().ActiveFishCountDown(waitTimeInRod);
            }
        }
    }

    private float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    public void SetFishingRod(FishingRod fR) { fishingRod = fR; }

    public void SetFishInstantiator(FishInstantiator fI)
    {
        fishInstantiator = fI;
    }

    public void SetMaterialToMeshRenderer(Material mat)
    {
        meshRenderer.material = mat;
    }

    public void SetLifeTime(float lT)
    {
        lifeTime = lT;
    }

    public void SetPoints(int p) { points = p; }

    public int GetPoints() { return points; }

    public bool IsInTheFishingRod() { return alreadyAtFishingRod; }

    private void QuitFromFishingRod()
    {
        if (!fishingRod.IsFishingRodGoingUp())
        {
            alreadyAtFishingRod = false;
            fishingRod.SetFishAtBait(false);
            fishingRod.QuitComponentToBait();
            this.gameObject.layer = layerIndexWhenCatched;

            MakeRotationInDir(-dir);

            //fishInstantiator.DeleteFishFromList(this);
        }
    }
}
