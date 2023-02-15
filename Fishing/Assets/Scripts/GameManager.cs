using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;

    //variables
    public int _points = 0;


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


    void Update()
    {
        
    }

    public void AddPoints(int p) { _points += p; }
    public int GetPoints() { return _points; }
}
