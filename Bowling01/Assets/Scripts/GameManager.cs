using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;

    //variables para la UI
    private float _currentAngle;    //Angulo antual de la barra de la UI
 
    //Managers
    private BolosManager _bolosManager;
    private SpawnerBall _spawnerBall;
    private SpawnerCleaner _spawnerCleaner;
    private GameUIManager _gameUIManager;
    private NetworkManager _networkManager;

    //variables del juego
    private bool firstPartCompleted = false;
    private int currentRound = 0;
    private int totalPoints = 0;
    private bool isGameActive = false;
    private bool pleno = false;

    //variables configurables
    [SerializeField] private int _rounds;   // cada ronda son dos tiradas
    [SerializeField] private int _gameAngle;    //Angulo maximo de desviacion de la bola
    [SerializeField] private int _exerciseAngle;
    [SerializeField] private int _difficulty;  // dificultad del juego que determinara la velocidad a la que se mueve la barra

    //Variable el guardado
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
    void Start()
    {
     
    }

    public void ThrownBall()
    {
        if (!firstPartCompleted)
        {
            //firstPartCompleted = true;

            Invoke("FirstPartPuntuation", 4);
            //iniciamos la segunda parte de la ronda
            SetRoundPartTwo();


        }
        else
        {
            //firstPartCompleted = false;

            Invoke("SecondPartPunctuation", 3);
            NewRound();
        }
    }
    //metodos para cambiar a una nueva ronda
    public void NewRound()
    {
        //CleanBolos();
        Invoke("CleanBolos", 3);

    }

    //metodos para pasar a la segunda parte de la ronda
    public void SetRoundPartTwo()
    {
        Invoke("StartSecondPart", 4);
    }

    private void StartSecondPart()
    {
        _bolosManager.CheckOnthefloor();
        Invoke("CleanBolos", 3);
    }
    private void CleanBolos()
    {
        _spawnerCleaner.SpawnCleaner();

    }
    public void AllClean()
    {

        if (!firstPartCompleted)
        {
            firstPartCompleted = true;
            _bolosManager.LetBolosFall();
            if (pleno)
            {
                _bolosManager.instatiateBolos();
               
            }
        }
        else
        {
            firstPartCompleted = false;
            _bolosManager.instatiateBolos();
        }

        _spawnerBall.SpawnBall();
    }

    //metodos para la puntuacion de la primera ronda
    private void FirstPartPuntuation()
    {
        //actualizamos la puntuacion
        int points = _bolosManager.CheckPoints(true);
        //si se tiran todos los bolos
        if(points == 10)
        {
            pleno = true;
        }
        _gameUIManager.FirstShootPuntuation(currentRound, points);
        totalPoints += points;
    }

    private void SecondPartPunctuation()
    {
        int points;
        if (pleno)
        {
            points = _bolosManager.CheckPoints(true);
            pleno = false;
        }
        else points = _bolosManager.CheckPoints(false);
      
        totalPoints += points;
        _gameUIManager.EndRoundPuntuation(currentRound, points, totalPoints);
        currentRound++;
        if(currentRound == _rounds)
        {
            //finalizamos el juego
            DesactiveGame();
            _gameUIManager.ActiveFinalPanel(totalPoints);
            _networkManager.StopServer();

        }
    }

    //metodos Setter
    public void SetBolosManager(BolosManager b) { _bolosManager = b; }
    public void SetSpawnerBall(SpawnerBall s) { _spawnerBall = s; }
    public void SetSpawnerCleaner(SpawnerCleaner s) { _spawnerCleaner = s; }
    public void SetGameUIManager(GameUIManager p) { _gameUIManager = p; }
    public void SetNetworkManager(NetworkManager n) { _networkManager = n; }

    //metodos getter
    public bool IsGameActive() { return isGameActive; }

    //metodos para la UI

    public void SetAngle(float angle)
    {
        _currentAngle = angle;
    }

    public float GetAngle() { return _currentAngle; }

    public int GetNumRounds() { return _rounds; }

    //metodos para la configuración

    public void SetNumRound(float round) { _rounds = (int)round; }

    public void SetGameAngle(float angle) { _gameAngle = (int)angle; }

    public int GetGameAngle() { return _gameAngle; }

    public void SetExerciseAngle(float angle) { _exerciseAngle = (int)angle; }
    public int GetExerciseAngle() { return _exerciseAngle; }

    public void SetDifficulty(float dif) { _difficulty = (int)dif; }
    public int GetDifficulty() { return _difficulty; }

    public void SafeConfig()
    {
        ConfigurationData data = new ConfigurationData();
        data.Rondas = _rounds;
        data.AnguloDeJuego = _gameAngle;
        data.AnguloDelEjercicio = _exerciseAngle;
        data.Dificultad = _difficulty;
        _configurationSafeManager.Safe(data);
    }

    private void LoadConfig()
    {
        ConfigurationData data = _configurationSafeManager.Load();
        if(data != null)
        {
            _rounds = data.Rondas;
            _gameAngle = data.AnguloDeJuego;
            _exerciseAngle = data.AnguloDelEjercicio;
            _difficulty = data.Dificultad;
        }
    }

    //guardado

    private void InitSave()
    {
        _saveData.InitSave();
    }

    public void WriteData(string data)
    {
        _saveData.WriteData(data);
    }

    private void FinishSave()
    {
        _saveData.FinishSave();
    }

    //gestion del juego -------------------------------------------------------------------------------
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void RestartGame()
    {
        firstPartCompleted = false;
        currentRound = 0;
        totalPoints = 0;
        //isGameActive = true;
    }

    public void InitGame()
    {
        isGameActive = true;
        //iniciamos el guardado
        InitSave();

    }

    public void DesactiveGame()
    {
        isGameActive = false;
        FinishSave();
    }

    

  

}
