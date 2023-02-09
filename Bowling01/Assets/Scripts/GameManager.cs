using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;
    //variables para la UI
    private float _angle;
    //variables del juego
    private BolosManager _bolosManager;
    private SpawnerBall _spawnerBall;
    private SpawnerCleaner _spawnerCleaner;
    private PuntuationUIManager _puntuationUIManager;

    [SerializeField] private int _rounds;// cada ronda son dos tiradas
    private bool firstPartCompleted = false;
    private int currentRound = 0;
    private int totalPoints = 0;


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
        _puntuationUIManager.FirstShootPuntuation(currentRound, points);
        totalPoints += points;
    }

    private void SecondPartPunctuation()
    {
        int points = _bolosManager.CheckPoints(false);
        totalPoints += points;
        _puntuationUIManager.EndRoundPuntuation(currentRound, points, totalPoints);
        currentRound++;
    }

    //metodos Setter
    public void SetBolosManager(BolosManager b) { _bolosManager = b; }
    public void SetSpawnerBall(SpawnerBall s) { _spawnerBall = s; }
    public void SetSpawnerCleaner(SpawnerCleaner s) { _spawnerCleaner = s; }
    public void SetPuntuationUIManager(PuntuationUIManager p) { _puntuationUIManager = p; }

    //metodos para la UI
    public void setAngle(float angle)
    {
        _angle = angle;
    }

    public float getAngle() { return _angle; }

    public int getNumRounds() { return _rounds; }

    //metodos para la configuración
    public void SetNumRound(int round) { _rounds = round +1; }
    public void SetNumRound(float round) { _rounds =(int)round ; }

}
