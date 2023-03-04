using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubeController prefab;
    [SerializeField] private CubeController firstPrefab;
    [SerializeField] private CubeController prefabBarrel;

    [SerializeField] private CubeController finalIsland;

    [SerializeField] private Transform parent;
    [SerializeField] private GameObject chestObject;

    [SerializeField] private float limitLeftCubes;
    [SerializeField] private float limitRightCubes;
    [SerializeField] private float limitMinCubesZ;
    [SerializeField] private float limitMaxCubesZ;
    [SerializeField] private float initPosCubeY;

    [SerializeField] private float limitLeftIsland;
    [SerializeField] private float limitRightIsland;
    [SerializeField] private float limitMinIslandZ;
    [SerializeField] private float limitMaxIslandZ;
    [SerializeField] private float initPosIslandY;

    private int numCubesInstantiated;
    private int randomNum;

    private void Awake()
    {
        GameManager.Instance.ImTheSpawner(this);
    }

    public void InstantiateNewCube()
    {
        if (numCubesInstantiated != GameManager.Instance.GetNumJumps() + 1)
        {
            CubeController cube = null;
            // Instanciar el primer cubo
            if (GameManager.Instance.getCubesSize() == 0)
            {
                Vector3 posCubeIni = new Vector3(0.0f, -0.6f, 0.0f);
                cube = Instantiate(firstPrefab, posCubeIni, Quaternion.identity, parent);
            }
            else if (numCubesInstantiated + 1 <= GameManager.Instance.GetNumJumps())
            {
                Vector3 newCubePos = GetNewPos(false);
                randomNum = Random.Range(0, 2);
                if (randomNum > 0)
                {
                    cube = Instantiate(prefab, newCubePos, Quaternion.identity, parent);
                }
                else cube = Instantiate(prefabBarrel, newCubePos, prefabBarrel.gameObject.transform.rotation, parent);

                randomNum= Random.Range(0, 2);
                if(randomNum > 0)
                {
                    Instantiate(chestObject, GetNewChestPos(newCubePos), chestObject.transform.rotation, parent);
                }
            }
            else if (numCubesInstantiated + 1 > GameManager.Instance.GetNumJumps())
            {
                Vector3 islandPos = GetNewPos(true);

                cube = Instantiate(finalIsland, islandPos, finalIsland.transform.rotation, parent);
                GameManager.Instance.SetFinalIslandTR(cube.transform);
            }
            numCubesInstantiated++;
            cube.SetSpeedDown(GameManager.Instance.GetSpeedDown());
            GameManager.Instance.addCube(cube);
        }
    }

    private Vector3 GetNewPos(bool island)
    {
        CubeController lastCube = GameManager.Instance.getLastCube();

        Vector3 posLastCube = lastCube.transform.position;

        float posX, posY, posZ;
        if (!island)
        {
            posX = Random.Range(limitLeftCubes, limitRightCubes);
            posY = initPosCubeY;
            posZ = Random.Range(posLastCube.z + limitMinCubesZ, posLastCube.z + limitMaxCubesZ);
        }
        else
        {
            posX = Random.Range(limitLeftIsland, limitRightIsland);
            posY = initPosIslandY;
            posZ = Random.Range(posLastCube.z + limitMinIslandZ, posLastCube.z + limitMaxIslandZ);
        }
        
        return new Vector3(posX, posY, posZ);
    }

    private Vector3 GetNewChestPos(Vector3 posNewCube)
    {
        CubeController lastCube = GameManager.Instance.getLastCube();

        Vector3 posLastCube = lastCube.transform.position;
        Vector3 jumpDir = posNewCube - posLastCube;

        return new Vector3(posLastCube.x + (jumpDir.x / 2), posLastCube.y + 2.2f, posLastCube.z + (jumpDir.z / 2));
    }
}
