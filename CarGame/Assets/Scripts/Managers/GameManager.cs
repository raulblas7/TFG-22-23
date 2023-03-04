using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;

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

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
