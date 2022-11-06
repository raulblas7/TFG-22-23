using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;

    private List<CubeController> cubes;

    [SerializeField]
    private int maxCubes = 4;

    [SerializeField]
    private Spawner spawner;

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
            InitGame();
            DontDestroyOnLoad(_instance);
        }
    }

    void InitGame()
    {
        cubes = new List<CubeController>();
        // TODO: quitar de aqui cuando haya menus
        for (int i = 0; i < maxCubes; i++)
        {
            spawner.InstantiateNewCube();
        }
    }

    public List<CubeController> getCubes() { return cubes; }

    public CubeController getLastCube() { return cubes[cubes.Count - 1]; }

    public GameObject getFirstCube() { return cubes[1].gameObject; }

    public void addCube(CubeController c) { cubes.Add(c); }

    public int getCubesSize() { return cubes.Count; }

    public void deleteCube()
    {
        CubeController cubeToDestroy = cubes[0];
        cubes.RemoveAt(0);
        Destroy(cubeToDestroy.gameObject);
        spawner.InstantiateNewCube();
    }

    public void LoadScene(string name)
    {
        //SceneManager.LoadScene(name);
    }
}
