
using UnityEngine;

public class SpawnerBall : MonoBehaviour
{
    [SerializeField] Ball prefab;

    private void Start()
    {      
        GameManager.Instance.SetSpawnerBall(this);
    }
    public void SpawnBall()
    {
        Instantiate<Ball>(prefab, transform.position, transform.rotation);
    }
   
}
