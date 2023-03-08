using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Variables Menu Main, solo tendrán valor en dicho menu
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;

    // Variables Menu Settings, solo tendrán valor en dicho menu
    [SerializeField] private TextMeshProUGUI textInvalidInput;
    [SerializeField] private TextMeshProUGUI textInvalidInput2;
    [SerializeField] private TMP_InputField mainInputField;
    [SerializeField] private TMP_InputField speedField;

    // Variables de UI en JumpScene, solo tendrán valor en dicha escena
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private GameObject panelWaitingMobile;
    [SerializeField] private TextMeshProUGUI textCodeRoom;

    private void SetButtonsMainMenuListeners()
    {
        // Añade un listener onClick al boton play si existe
        if (playButton != null)
        {
            playButton.onClick.AddListener(delegate { OnClickPlay(); });
        }

        // Añade un listener onClick al boton settings si existe
        if (settingsButton != null)
        {
            settingsButton.onClick.AddListener(delegate { OnClickSettings(); });
        }
    }

    public void OnClickPlay()
    {
        GameManager.Instance.LoadScene("JumpScene");
    }

    public void OnClickSettings()
    {
        GameManager.Instance.LoadScene("SettingsMenu");
    }

    public void SetInputFieldListener()
    {
        // Añade un listener onValueChanged al inputField de settings si existe
        if (mainInputField != null)
        {
            mainInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }
        // Añade un listener onValueChanged al inputField de la velocidad de las rocas si existe
        if (speedField != null)
        {
            speedField.onValueChanged.AddListener(delegate { ValueChangeCheckSpeed(); });
        }
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        try
        {
            int n = int.Parse(mainInputField.text);
            GameManager.Instance.SetNumJumps(n);
        }
        catch
        {
            textInvalidInput.enabled = true;
        }
    }

    public void ValueChangeCheckSpeed()
    {
        try
        {
            float n = float.Parse(speedField.text);
            GameManager.Instance.SetSpeedDownCubes(n);
        }
        catch
        {
            textInvalidInput2.enabled = true;
        }
    }

    public void ReturnToMenu()
    {
        try
        {
            //TODO: cambiar
            int n = int.Parse(mainInputField.text);
            GameManager.Instance.LoadScene("MainMenu");
        }
        catch
        {
            textInvalidInput.text = "Has introducido un valor que no es válido, introduce un número correcto y regresa al menú para jugar";
        }
    }

    private void SetButtonExitListener()
    {
        // Añade un listener onClick al boton exit si existe
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(delegate { OnClickExit(); });
        }
    }

    public void OnClickExit()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }

    private void SetUIManagerInGameManager()
    {
        GameManager.Instance.ImUiManager(this);
    }

    private void OnEnable()
    {
        // Registra el callback SceneManager.sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desregistra el callback SceneManager.sceneLoaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Escena cargada es " + scene.name);
        switch (scene.name) 
        {
            case "MainMenu":
                SetButtonsMainMenuListeners();
                break;
            case "SettingsMenu":
                SetInputFieldListener();
                break;
            case "JumpScene":
                SetUIManagerInGameManager();
                //SetButtonExitListener();
                break;
            default:
                break;
        }
    }

    public void SetPointsText(int points)
    {
        if (pointsText != null)
        {
            pointsText.text = "" + points;
        }
    }

    public void TellGameManagerQuitApp()
    {
        GameManager.Instance.QuitApplication();
    }

    public void DisablePanelWaiting()
    {
        panelWaitingMobile.SetActive(false);
    }

    public bool IsPanelWaitingEnabled()
    {
        return panelWaitingMobile.activeSelf;
    }

    public void SetCodeRoomText(string room)
    {
        textCodeRoom.text = room;
    }
}
