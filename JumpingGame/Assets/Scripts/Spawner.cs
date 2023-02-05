using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubeController prefab;
    [SerializeField] private CubeController firstPrefab;
    [SerializeField] private CubeController prefabBarrel;

    [SerializeField] private GameObject finalIsland;

    [SerializeField] private Transform parent;
    [SerializeField] private Transform limitLeft;
    [SerializeField] private Transform limitRight;

    private int numCubesInstantiated;
    private int randomNum;

    public void InstantiateNewCube()
    {
        randomNum = Random.Range(0, 2);
        CubeController cube;
        // Instanciar el primer cubo
        if(GameManager.Instance.getCubesSize() == 0)
        {
            Vector3 posCubeIni = new Vector3(0.0f, -0.6f, 0.0f);
            cube = Instantiate(firstPrefab, posCubeIni, Quaternion.identity, parent);
            numCubesInstantiated++;
            GameManager.Instance.addCube(cube);
        }
        else if(numCubesInstantiated + 1 < GameManager.Instance.GetNumJumps())
        {
            Vector3 newCubePos = GetNewPos();

            if (randomNum > 0)
            {
                cube = Instantiate(prefab, newCubePos, Quaternion.identity, parent);
            }
            else cube = Instantiate(prefabBarrel, newCubePos, prefabBarrel.gameObject.transform.rotation, parent);

            numCubesInstantiated++;
            GameManager.Instance.addCube(cube);
        }
        else
        {
            Vector3 islandPos = GetNewPos();

            GameObject island = Instantiate(finalIsland, islandPos, finalIsland.transform.rotation, parent);
            GameManager.Instance.SetFinalIslandTR(island.transform);
        }
    }

    private Vector3 GetNewPos()
    {
        CubeController lastCube = GameManager.Instance.getLastCube();

        Vector3 posLastCube = lastCube.transform.position;

        float posX = Random.Range(limitLeft.position.x, limitRight.position.x);
        float posZ = Random.Range(posLastCube.z + 2.0f, posLastCube.z + 4.0f);

        return new Vector3(posX, -0.6f, posZ);
    }
}
