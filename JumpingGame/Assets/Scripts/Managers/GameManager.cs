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

    private int maxCubes = 4;

    private int numJumps = 20;
    private int numCurrentJumps;

    private float speedDownSetting = 4f;

    private Transform finalIslandTR;
    private Spawner spawner;

    private int numPoints;

    private UIManager uiManager;

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

    public void ImTheSpawner(Spawner spawner)
    {
        this.spawner = spawner;
        if(this.spawner != null)
        {
            Debug.Log("Spawner no es null");
            InitGame();
        }
    }

    void InitGame()
    {
        cubes = new List<CubeController>();
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
        if(uiManager != null)
        {
            uiManager.SetPointsText(numPoints);
        }
    }

    public void SetNumJumps(int nJumps)
    {
        Debug.Log("Entro al setNumJumps en GameManager");
        numJumps = nJumps;
        Debug.Log("NumJumps es " + numJumps);
        maxCubes = numJumps / 3;
        if (maxCubes <= 0)
        {
            maxCubes = 1;
        }
    }

    public void ImUiManager(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public void SetSpeedDownCubes(float s)
    {
        speedDownSetting = s;
    }

    public float GetSpeedDown()
    {
        return speedDownSetting;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
