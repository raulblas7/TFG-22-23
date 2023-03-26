using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;


    [SerializeField] private int _maxFish;   // indica la catidad maxima de peces que podemos pescar
    //Managers
    private GameUIManager _UIManager;
    //variables
    private int _points = 0;
    private int _currentFish = 0;           //numero de peces que se han pescado


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
            //TODO fin del juego

        }

    }
    public int GetPoints() { return _points; }

    public int GetMaxFish() { return _maxFish; }

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
    }

    //managers
    public void SetUImanager(GameUIManager manager) { _UIManager = manager; }
    public GameUIManager GetUIManager() { return _UIManager; }

    public void ChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
