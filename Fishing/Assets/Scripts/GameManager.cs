using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;


    [SerializeField] private int _maxFish;   // indica la catidad maxima de peces que podemos pescar
    [SerializeField] private float _maxTime;
    [SerializeField] private int _gameAngle;
    [SerializeField] private int _maxSeries;
    //Managers
    private GameUIManager _UIManager;
    private FishInstantiator _instantiator;

    //variables
    private int _points = 0;
    private int _currentFish = 0;           //numero de peces que se han pescado
    private bool _gameActive = false;
    private int _currentSerie = 0;
    private int _serieTime = 30;

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



    //variables del juego
    public void AddPoints(int p)
    {
        //actualizamos los puntos
        _points += p;
        _UIManager.UpdatePoints(_points);
        //actualizamos los peces pescados
        _currentFish++;
        _UIManager.UpdateFishCount(_currentFish);
        if (_currentFish == _maxFish)
        {
            //fin de serie
            DesactiveGame();    //desactivamos el juego y paramos el guardado
            _UIManager.ActiveFinalPanel(_points, _currentSerie + 1, _maxSeries);
            _currentSerie++;
            //si hemos terminado el juego
            if (_currentSerie == _maxSeries)
            {
                NetworkManager.Instance.StopServer();
                _UIManager.ActiveMainMenuButton();
                restartSeries();
            }
            else
            {
                RestartGame();
                _UIManager.UpdatePoints(_points);
                _UIManager.UpdateFishCount(_currentFish);
                _UIManager.ActiveFinalCountDown(_serieTime);
                _instantiator.DeleteAllFish();
            }

        }

    }
    public int GetPoints() { return _points; }



    //public void AddFish()
    //{
    //    _currentFish++;
    //    _UIManager.UpdateFishCount(_currentFish);
    //    if(_currentFish == _maxFish)
    //    {
    //        //TODO fin del juego

    //    }
    //}
    public void RestartGame()
    {
        _points = 0;
        _currentFish = 0;
        //_currentSerie = 0;
    }
    public void restartSeries()
    {
        _currentSerie = 0;
    }

    //managers
    public void SetUImanager(GameUIManager manager) { _UIManager = manager; }
    public GameUIManager GetUIManager() { return _UIManager; }

    // public void SetNetworkManager(NetworkManager n) { _networkManager = n; }

    public void SetInstatiator(FishInstantiator i) { _instantiator = i; }


    //gestion del juego

    public void ChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
    public bool IsGameActive() { return _gameActive; }
    public void InitGame()
    {
        //instanciamos los primeros peces
        _instantiator.StartInstantiate();
        _gameActive = true;
        InitSave();
    }
    public void DesactiveGame()
    {
        _gameActive = false;
        FinishSave();
    }

    //Configuracion
    public int GetMaxFish() { return _maxFish; }
    public void SetMAxFish(int num) { _maxFish = num; }

    public float GetMaxTime() { return _maxTime; }
    public void SetMaxTime(float time) { _maxTime = time; }

    public int GetGameAngle() { return _gameAngle; }
    public void SetGameAngle(int Angle) { _gameAngle = Angle; }

    public int GetMaxSeries() { return _maxSeries; }
    public void SetMaxSeries(int series) { _maxSeries = series; }

    public void SafeConfig()
    {
        ConfigurationData data = new ConfigurationData();
        data.Numerorepeticiones = _maxFish;
        data.AnguloDeJuego = _gameAngle;
        data.TiempoRepeticiones = _maxTime;
        data.Series = _maxSeries;
        _configurationSafeManager.Safe(data);
    }

    private void LoadConfig()
    {
        ConfigurationData data = _configurationSafeManager.Load();
        if (data != null)
        {
            _maxFish = data.Numerorepeticiones;
            _maxTime = data.TiempoRepeticiones;
            _gameAngle = data.AnguloDeJuego;
            _maxSeries = data.Series;
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


}
