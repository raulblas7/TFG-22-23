
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private UIManager uiManager;
    private int angleToDoIt = 30;
    private int angleMinToDoIt = 10;
    private int laps = 3;
    private int reps = 10;
    private int currentReps = 0;
    private int currentLaps = 0;
    private float difficulty;

    // Variables de guardado
    ConfigurationSaveManager _configurationSafeManager;
    SaveData _saveData;

    int points = 0;

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

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ImUiManager(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public UIManager GetUIManager()
    {
        return uiManager;
    }

    public void SetAngleToDoIt(int angle)
    {
        angleToDoIt = angle;
    }

    public float GetAngleToDoIt()
    {
        return angleToDoIt;
    }

    public void SetAngleMinToDoIt(int angle)
    {
        angleMinToDoIt = angle;
    }

    public float GetAngleMinToDoIt()
    {
        return angleMinToDoIt;
    }

    public void SetNumReps(int r)
    {
        reps = r;
    }

    public int GetReps()
    {
        return reps;
    }

    public void AddLaps()
    {
        currentLaps++;
        if(currentLaps <= laps) uiManager.SetlapsText();
        if (currentLaps >= laps)
        {
            if (currentReps >= reps)
            {
                uiManager.GameFinished();
            }
        }
    }

    public void AddReps()
    {
        currentReps++;
        if(currentReps <= reps) uiManager.SetRepsText();
        if (currentReps == reps)
        {
            AddLaps();
            if (!uiManager.IsPanelWinningEnabled())
            {
                uiManager.ActivatePanelFinishSerie();
                currentReps = 0;
                uiManager.SetRepsText();
            }
        }
    }

    public void SetNumLaps(int l)
    {
        laps = l;
    }

    public void SetNumCurrentLaps(int l)
    {
        currentLaps = l;
    }

    public void SetCurrentReps(int r)
    {
        currentReps = r;
    }

    public int GetLaps()
    {
        return laps;
    }
    public int GetCurrentLaps()
    {
        return currentLaps;
    }

    public int GetCurrentReps()
    {
        return currentReps;
    }

    public void SetDifficulty(int dif) { difficulty = dif; }
    public float GetDifficulty() { return difficulty; }

    private void LoadConfig()
    {
        ConfigurationData data = _configurationSafeManager.Load();
        if (data != null)
        {
            reps = data.Reps;
            laps = data.Laps;
            angleToDoIt = data.AnguloDeJuego;
            angleMinToDoIt = data.AnguloDeJuegoMin;
            difficulty = data.Dificultad; 
        }
    }

    public void SafeConfig()
    {
        ConfigurationData data = new ConfigurationData();
        data.Reps = reps;
        data.Laps = laps;
        data.AnguloDeJuego = angleToDoIt;
        data.AnguloDeJuegoMin = angleMinToDoIt;
        data.Dificultad = difficulty;
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

    public void AddPoints(int p)
    {
        points += p;
        uiManager.UpdatePointsText(points);
    }

    public void LessPoints(int p)
    {
        if (points - p >= 0)
        {
            points -= p;
            uiManager.UpdatePointsText(points);
        }
    }
}
