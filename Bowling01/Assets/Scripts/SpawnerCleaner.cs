
using UnityEngine;

public class SpawnerCleaner : MonoBehaviour
{

    [SerializeField] GameObject prefab;

    private void Start()
    {

        GameManager.Instance.SetSpawnerCleaner(this);

    }
    public void SpawnCleaner()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
