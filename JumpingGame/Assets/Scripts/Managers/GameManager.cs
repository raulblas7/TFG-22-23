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

    private int angleToDoIt = 90;

    private UIManager uiManager;

    // Variables de guardado
    ConfigurationSaveManager _configurationSafeManager;
    SaveData _saveData;

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
            _configurationSafeManager = new ConfigurationSaveManager();
            _saveData = new SaveData();
            LoadConfig();
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
        numCurrentJumps = 0;
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
        uiManager.SetJumpsText();
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
    public void QuitPoints(int points)
    {
        if (numPoints - points >= 0)
        {
            numPoints -= points;
            Debug.Log("Llevas " + numPoints + " puntos!");
            if (uiManager != null)
            {
                uiManager.SetPointsText(numPoints);
            }
        }
    }

    public void SetNumJumps(int nJumps)
    {
        Debug.Log("Entro al setNumJumps en GameManager");
        numJumps = nJumps;
        Debug.Log("NumJumps es " + numJumps);
        maxCubes = numJumps / 3;
        if (maxCubes <= 1)
        {
            maxCubes = 2;
        }
    }

    public void ImUiManager(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public UIManager GetUIManager()
    {
        return uiManager;
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

    public void SetAngleToDoIt(int angle)
    {
        angleToDoIt = angle;
    }

    public float GetAngleToDoIt()
    {
        return angleToDoIt;
    }

    private void LoadConfig()
    {
        ConfigurationData data = _configurationSafeManager.Load();
        if (data != null)
        {
            numJumps = data.Jumps;
            angleToDoIt = data.AngleToDo;
            speedDownSetting = data.TimeBetweenReps;
        }
    }

    public void SafeConfig()
    {
        ConfigurationData data = new ConfigurationData();
        data.Jumps = numJumps;
        data.AngleToDo = angleToDoIt;
        data.TimeBetweenReps = speedDownSetting;
        _configurationSafeManager.Safe(data);
    }

    //guardado
    public void InitSave()
    {
        _saveData.InitSave();
    }

    public void WriteData(string data)
    {
        _saveData.WriteData(data);
    }

    public void FinishSave()
    {
        _saveData.FinishSave();
    }

    private void OnApplicationQuit()
    {
        FinishSave();
    }
}
