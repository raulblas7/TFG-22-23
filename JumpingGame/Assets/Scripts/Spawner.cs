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

    [SerializeField] private float limitLeftCubes;
    [SerializeField] private float limitRightCubes;
    [SerializeField] private float limitLeftIsland;
    [SerializeField] private float limitRightIsland;

    private int numCubesInstantiated;
    private int randomNum;

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
                Vector3 newCubePos = GetNewPos(2.0f, 4.0f, -0.6f, false);
                randomNum = Random.Range(0, 2);
                if (randomNum > 0)
                {
                    cube = Instantiate(prefab, newCubePos, Quaternion.identity, parent);
                }
                else cube = Instantiate(prefabBarrel, newCubePos, prefabBarrel.gameObject.transform.rotation, parent);
            }
            else if (numCubesInstantiated + 1 > GameManager.Instance.GetNumJumps())
            {
                Vector3 islandPos = GetNewPos(-11.5f, -11f, -10f, true);

                cube = Instantiate(finalIsland, islandPos, finalIsland.transform.rotation, parent);
                GameManager.Instance.SetFinalIslandTR(cube.transform);
            }
            numCubesInstantiated++;
            GameManager.Instance.addCube(cube);
        }
    }

    private Vector3 GetNewPos(float limitMin, float limitMax, float initPosY, bool island)
    {
        CubeController lastCube = GameManager.Instance.getLastCube();

        Vector3 posLastCube = lastCube.transform.position;

        float posX;
        if(!island) posX = Random.Range(limitLeftCubes, limitRightCubes);
        else posX = Random.Range(limitLeftIsland, limitRightIsland);
        
        float posZ = Random.Range(posLastCube.z + limitMin, posLastCube.z + limitMax);

        return new Vector3(posX, initPosY, posZ);
    }
}
