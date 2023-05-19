using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//enum Sides
//{
//    Left,
//    Right,
//    Up,
//    Down,
//    Back,
//    Forward

//}
public class Fish1 : MonoBehaviour
{


    [SerializeField] private Rigidbody rb;
    [SerializeField] private float vel;
    [SerializeField] private FishVision fishVision;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private int layerIndexWhenCatched;
    [SerializeField] private int layerIndexWhenNOTCatched;

    private Transform deadZone;
    private Transform[] objetives;
    private Transform currentObjetive;


    private float waitTimeInRod;
    private float lifeTime;
    private bool goToFishingRod = false;
    private bool alreadyAtFishingRod = false;
    private FishingRod fishingRod;
    private Transform fishingBait;
    private int points = 0;

    private bool dead = false;
 

    void Start()
    {
        waitTimeInRod = GameManager.Instance.GetMaxTime();
        Invoke("GoFishingRod", lifeTime - 5f);
        Invoke("DeadFish", lifeTime);
    }

    void Update()
    {

        if (!goToFishingRod && fishVision.IsSawFishingRod())
        {
            Debug.Log("ve hacia la caña");
            // Ir hacia la caña
            GoToObjetive(fishVision.GetFishingRodTr());
            rb.velocity = Vector3.zero;
            goToFishingRod = true;
        }
        //else if( goToFishingRod && !dead && !alreadyAtFishingRod)
        //{
        //    GoToObjetive(fishingBait);
        //}
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spawner") && !goToFishingRod)
        {
            PickNewObjetive();
            rb.velocity = Vector3.zero;
        }
    }

    public void PickNewObjetive()
    {
        int aux = Random.Range(0, objetives.Length);
        currentObjetive = objetives[aux];
        //transform.LookAt(currentObjetive);
        // Obtenemos la dirección hacia el objetivo
        Vector3 direction = (currentObjetive.position - transform.position).normalized;

        // Calculamos la rotación que necesitamos para mirar hacia el objetivo
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Rotamos el Rigidbody hacia la rotación deseada
        rb.MoveRotation(lookRotation);

    }
    private void FixedUpdate()
    {
        if (!alreadyAtFishingRod)
        {
            rb.AddForce(transform.forward * vel);

        }
        if (goToFishingRod && !dead && !alreadyAtFishingRod)
        {
            GoToObjetive(fishingBait);
        }
    }


    private void DeadFish()
    {
        if (!alreadyAtFishingRod)
        {
            this.gameObject.layer = layerIndexWhenCatched;
            GoToObjetive(deadZone);
            rb.velocity = Vector3.zero;
            dead = true;
        }
    }

    private void GoFishingRod()
    {
        if (!fishingRod.HasFishAtBait())
        {
            // GoToObjetive(fishingBait);
            goToFishingRod = true;
        }
    }


    private void GoToObjetive(Transform tr)
    {
        currentObjetive = tr;
        //transform.LookAt(currentObjetive);
        // Obtenemos la dirección hacia el objetivo
        Vector3 direction = (currentObjetive.position - transform.position).normalized;

        // Calculamos la rotación que necesitamos para mirar hacia el objetivo
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Rotamos el Rigidbody hacia la rotación deseada
        rb.MoveRotation(lookRotation);

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("FishingRod") /*&& fishVision.IsSawFishingRod()*/)
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

    public void SetFishingRod(FishingRod fR) { fishingRod = fR; }

    public void SetFishingBait(Transform bait) { fishingBait = bait; }

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

    public void SetObjetivesList(Transform[] list)
    {
        objetives = list;
    }
    public void SetDeadZoneTr(Transform deadTr)
    {
        deadZone = deadTr;
    }

    public bool IsInTheFishingRod() { return alreadyAtFishingRod; }

    private void QuitFromFishingRod()
    {
        if (!fishingRod.IsFishingRodGoingUp())
        {
            alreadyAtFishingRod = false;
            fishingRod.SetFishAtBait(false);
            fishingRod.QuitComponentToBait();
            this.gameObject.layer = layerIndexWhenCatched;

            GoToObjetive(deadZone);

            //fishInstantiator.DeleteFishFromList(this);
        }
    }
}
