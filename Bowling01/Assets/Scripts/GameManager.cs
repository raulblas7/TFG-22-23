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

    [SerializeField] private int _rounds;// cada ronda son dos tiradas
    private bool firstPartCompleted = false;
    private int currentRounds = 0;


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
            SetRoundPartTwo();
        }
        else
        {
            //firstPartCompleted = false;
            NewRound();
        }
    }
    //metodos para cambiar a una nueva ronda
    public void NewRound()
    {
        CleanBolos();
        currentRounds++;
    }

    //metodos para pasar a la segunda parte de la ronda
    public void SetRoundPartTwo()
    {
        Invoke("StartSecondPart", 5);
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

    //metodos Setter
    public void SetBolosManager(BolosManager b) { _bolosManager = b; }
    public void SetSpawnerBall(SpawnerBall s) { _spawnerBall = s; }
    public void SetSpawnerCleaner(SpawnerCleaner s) { _spawnerCleaner = s; }

    //metodos para la UI
    public void setAngle(float angle)
    {
        _angle = angle;
    }

    public float getAngle() { return _angle; }
}
