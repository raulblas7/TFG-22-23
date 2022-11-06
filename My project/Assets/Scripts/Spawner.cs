using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private CubeController prefab;
    [SerializeField]
    private CubeController firstPrefab;

    [SerializeField]
    private Transform parent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InstantiateNewCube()
    {
        CubeController cube;
        // Instanciar el primer cubo
        if(GameManager.Instance.getCubesSize() == 0)
        {
            Vector3 posCubeIni = new Vector3(0.0f, -0.6f, 0.0f);
            cube = Instantiate(firstPrefab, posCubeIni, Quaternion.identity, parent);
        }
        else
        {
            CubeController lastCube = GameManager.Instance.getLastCube();

            Vector3 posLastCube = lastCube.transform.position;

            float posX = Random.Range(posLastCube.x - 3.0f, posLastCube.x + 3.0f);
            float posZ = Random.Range(posLastCube.z + 2.0f, posLastCube.z + 5.0f);

            Vector3 newCubePos = new Vector3(posX, -0.6f, posZ);

            cube = Instantiate(prefab, newCubePos, Quaternion.identity, parent);
        }
        GameManager.Instance.addCube(cube);

    }
}
