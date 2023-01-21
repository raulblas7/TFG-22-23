using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;
    //variables del juego
    private float _angle;
  
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAngle(float angle)
    {
        _angle = angle;
    }

    public float getAngle() { return _angle; }

        
}
