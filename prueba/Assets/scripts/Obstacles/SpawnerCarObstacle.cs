using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCarObstacle : MonoBehaviour
{


    [SerializeField] float spawnTime;
    [SerializeField] GameObject prefab;

    private float currentTime = 0;

    private void Start()
    {
        Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
        currentTime += Time.deltaTime;
        if( currentTime >= spawnTime)
        {
            currentTime = 0;
            Instantiate(prefab, transform.position, Quaternion.identity, transform);
        }
    }
}
