using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;

    private List<CubeController> cubes;

    [SerializeField] private int maxCubes = 4;
    [SerializeField] private Spawner spawner;

    // TODO: esto vendra configurable desde el menú pero de momento lo ponemos por el inspector
    [SerializeField] private int numJumps = 20;
    private int numCurrentJumps;

    private Transform finalIslandTR;

    private int numPoints;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }

    public void StartGame()
    {
        LoadScene("JumpScene");
        InitGame();
    }

    void InitGame()
    {
        cubes = new List<CubeController>();
        // TODO: quitar de aqui cuando haya menus
        for (int i = 0; i < maxCubes; i++)
        {
            spawner.InstantiateNewCube();
        }
    }

    public List<CubeController> getCubes() { return cubes; }

    public CubeController getLastCube() { return cubes[cubes.Count - 1]; }

    public GameObject getFirstCube() { return cubes[1].gameObject; }    

    public void addCube(CubeController c) { 
        if(c != null) cubes.Add(c); 
    }

    public int getCubesSize() { return cubes.Count; }

    public void deleteCube()
    {
        CubeController cubeToDestroy = cubes[0];
        cubes.RemoveAt(0);
        Destroy(cubeToDestroy.gameObject);
        spawner.InstantiateNewCube();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public int GetNumJumps()
    {
        return numJumps;
    }

    public int GetNumCurrentJumps()
    {
        return numCurrentJumps;
    }

    public void AddOneMoreJump()
    {
        numCurrentJumps++;
    }

    public void SetFinalIslandTR(Transform tr)
    {
        finalIslandTR = tr;
    }

    public GameObject GetIsland()
    {
        return finalIslandTR.gameObject;
    }

    public void AddPoints(int points)
    {
        numPoints += points;
        Debug.Log("Llevas " + numPoints + " puntos!");
    }
}
