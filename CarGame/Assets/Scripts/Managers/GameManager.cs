using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instancia del gameManager
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private UIManager uiManager;
    private float angleToDoIt = 30.0f;
    private int laps = 3;
    private int currentLaps = 0;

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

    public void ImUiManager(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public UIManager GetUIManager()
    {
        return uiManager;
    }

    public void SetAngleToDoIt(float angle)
    {
        angleToDoIt = angle;
    }

    public float GetAngleToDoIt()
    {
        return angleToDoIt;
    }

    public void AddLaps()
    {
        if (currentLaps + 1 <= laps)
        {
            currentLaps++;
            uiManager.SetlapsText();
        }
    }

    public void SetNumLaps(int l)
    {
        laps = l;
    }

    public int GetLaps()
    {
        return laps;
    }
    public int GetCurrentLaps()
    {
        return currentLaps;
    }
}
