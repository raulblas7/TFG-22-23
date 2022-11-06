using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;
    private CubeController[] cubes;
    [SerializeField]
    private int maxCubes = 4;
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public CubeController[] getCubes() { return cubes; }
}
