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

    //variables del juego
    private bool firstPartCompleted = false;
    private int currentRound = 0;
    private int totalPoints = 0;
    private bool isGameActive = false;

    //variables configurables
    [SerializeField] private int _rounds;   // cada ronda son dos tiradas
    [SerializeField] private int _gameAngle;    //Angulo maximo de desviacion de la bola
    [SerializeField] private int _exerciseAngle;
    [SerializeField] private int _difficulty;  // dificultad del juego que determinara la velocidad a la que se mueve la barra

    //Variable el guardado
    ConfigurationSafeManager _configurationSafeManager;
    


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
    void Start()
    {
        _configurationSafeManager = new ConfigurationSafeManager();
    }

    public void ThrownBall()
    {
        if (!firstPartCompleted)
        {
            //firstPartCompleted = true;

            Invoke("FirstPartPuntuation", 3);
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
        Invoke("StartSecondPart", 3);
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
        _gameUIManager.FirstShootPuntuation(currentRound, points);
        totalPoints += points;
    }

    private void SecondPartPunctuation()
    {
        int points = _bolosManager.CheckPoints(false);
        totalPoints += points;
        _gameUIManager.EndRoundPuntuation(currentRound, points, totalPoints);
        currentRound++;
        if(currentRound == _rounds)
        {
            //finalizamos el juego
            isGameActive = false;
            _gameUIManager.ActiveFinalPanel(totalPoints);

        }
    }

    //metodos Setter
    public void SetBolosManager(BolosManager b) { _bolosManager = b; }
    public void SetSpawnerBall(SpawnerBall s) { _spawnerBall = s; }
    public void SetSpawnerCleaner(SpawnerCleaner s) { _spawnerCleaner = s; }
    public void SetPuntuationUIManager(GameUIManager p) { _gameUIManager = p; }

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
    //public void SetNumRound(int round) { _rounds = round + 1; }
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

    //gestion del juego
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
    }

    

  

}
