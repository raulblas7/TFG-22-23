using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Variables Menu Main, solo tendrán valor en dicho menu
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button infoButton;

    // Variables Menu Settings, solo tendrán valor en dicho menu
    [SerializeField] private TextMeshProUGUI textInvalidInput;
    [SerializeField] private TextMeshProUGUI textInvalidInput2;
    [SerializeField] private TextMeshProUGUI textInvalidInput3;
    [SerializeField] private TextMeshProUGUI textInvalidInput4;
    [SerializeField] private TextMeshProUGUI textInvalidInput5;
    [SerializeField] private TMP_InputField movementInputField;
    [SerializeField] private TMP_InputField seriesInputField;
    [SerializeField] private TMP_InputField speedField;
    [SerializeField] private TMP_InputField angleMinField;
    [SerializeField] private TMP_InputField angleField;
    [SerializeField] private Button returnButton;

    // Variables de UI en JumpScene, solo tendrán valor en dicha escena
    [SerializeField] private Button exitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI jumpsText;
    [SerializeField] private TextMeshProUGUI seriesText;
    [SerializeField] private GameObject panelWaitingMobile;
    [SerializeField] private GameObject panelDisconnecting;
    [SerializeField] private GameObject panelWinning;
    [SerializeField] private TextMeshProUGUI waitingText;
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private TextMeshProUGUI countDownText;    

    private void Start()
    {
        if(panelDisconnecting != null) panelDisconnecting.SetActive(false);
        if (countDownText != null) countDownText.gameObject.SetActive(false);
        if(panelWinning != null) panelWinning.SetActive(false);
        if(jumpsText != null)
        {
            SetJumpsText();
        }
        if (seriesText != null)
        {
            SetSeriesText();
        }
    }

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

        if (infoButton != null)
        {
            infoButton.onClick.AddListener(delegate { GameManager.Instance.LoadScene("InfoScene"); });
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
        if (movementInputField != null)
        {
            movementInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
            movementInputField.text = "" + GameManager.Instance.GetNumJumps();
        }
        if (seriesInputField != null)
        {
            seriesInputField.onValueChanged.AddListener(delegate { ValueChangeCheckSeries(); });
            seriesInputField.text = "" + GameManager.Instance.GetNumSeries();
        }
        // Añade un listener onValueChanged al inputField de la velocidad de las rocas si existe
        if (speedField != null)
        {
            speedField.onValueChanged.AddListener(delegate { ValueChangeCheckSpeed(); });
            speedField.text = "" + GameManager.Instance.GetSpeedDown();
        }
        if (angleMinField != null)
        {
            angleMinField.onValueChanged.AddListener(delegate { ValueChangeCheckAngleMin(); });
            angleMinField.text = "" + GameManager.Instance.GetAngleMinToDoIt();
        }
        if (angleField != null)
        {
            angleField.onValueChanged.AddListener(delegate { ValueChangeCheckAngle(); });
            angleField.text = "" + GameManager.Instance.GetAngleToDoIt();
        }
    }

    public void CallSaveConfig()
    {
        GameManager.Instance.SafeConfig();
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        try
        {
            int n = int.Parse(movementInputField.text);
            GameManager.Instance.SetNumJumps(n);
            if (n <= 0)
            {
                textInvalidInput.enabled = true;
                returnButton.interactable = false;
            }
            else
            {
                textInvalidInput.enabled = false;
                if (!textInvalidInput2.enabled && !textInvalidInput3.enabled && !textInvalidInput4.enabled && !textInvalidInput5.enabled)
                {
                    returnButton.interactable = true;
                }
            }
        }
        catch
        {
            textInvalidInput.enabled = true;
            returnButton.interactable = false;
        }
    }

    public void ValueChangeCheckSeries()
    {
        try
        {
            int n = int.Parse(seriesInputField.text);
            GameManager.Instance.SetNumSeries(n);
            if (n <= 0)
            {
                textInvalidInput2.enabled = true;
                returnButton.interactable = false;
            }
            else
            {
                textInvalidInput2.enabled = false;
                if (!textInvalidInput.enabled && !textInvalidInput3.enabled && !textInvalidInput4.enabled && !textInvalidInput5.enabled)
                {
                    returnButton.interactable = true;
                }
            }
        }
        catch
        {
            textInvalidInput2.enabled = true;
            returnButton.interactable = false;
        }
    }

    public void ValueChangeCheckSpeed()
    {
        try
        {
            float n = float.Parse(speedField.text);
            GameManager.Instance.SetSpeedDownCubes(n);
            if (n <= 0)
            {
                textInvalidInput3.enabled = true;
                returnButton.interactable = false;
            }
            else
            {
                textInvalidInput3.enabled = false;
                if (!textInvalidInput.enabled && !textInvalidInput2.enabled && !textInvalidInput4.enabled && !textInvalidInput5.enabled)
                {
                    returnButton.interactable = true;
                }
            }
        }
        catch
        {
            textInvalidInput3.enabled = true;
            returnButton.interactable = false;
        }
    }

    public void ValueChangeCheckAngleMin()
    {
        try
        {
            int n = int.Parse(angleMinField.text);
            GameManager.Instance.SetAngleMinToDoIt(n);
            if (n <= 0 || GameManager.Instance.GetAngleToDoIt() - n < 15)
            {
                textInvalidInput4.enabled = true;
                returnButton.interactable = false;
            }
            else
            {
                textInvalidInput4.enabled = false;
                if (!textInvalidInput.enabled && !textInvalidInput2.enabled && !textInvalidInput3.enabled && !textInvalidInput5.enabled)
                {
                    returnButton.interactable = true;
                }
            }
        }
        catch
        {
            textInvalidInput4.enabled = true;
            returnButton.interactable = false;
        }
    }

    public void ValueChangeCheckAngle()
    {
        try
        {
            int n = int.Parse(angleField.text);
            GameManager.Instance.SetAngleToDoIt(n);
            if (n <= 0 || n - GameManager.Instance.GetAngleMinToDoIt() < 15)
            {
                textInvalidInput5.enabled = true;
                returnButton.interactable = false;
            }
            else
            {
                textInvalidInput5.enabled = false;
                if (!textInvalidInput.enabled && !textInvalidInput2.enabled && !textInvalidInput3.enabled && !textInvalidInput4.enabled)
                {
                    returnButton.interactable = true;
                }
            } 
        }
        catch
        {
            textInvalidInput5.enabled = true;
            returnButton.interactable = false;
        }
    }

    public void SetJumpsText()
    {
        jumpsText.text = "Saltos " + GameManager.Instance.GetNumCurrentJumps() + " de " + GameManager.Instance.GetNumJumps();
    }

    public void SetSeriesText()
    {
        seriesText.text = "Series " + GameManager.Instance.GetCurrentSerie() + " de " + GameManager.Instance.GetNumSeries();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }

    public void OnClickExit()
    {
        ActivatePanelDisconnecting();
        Invoke("GoToMenuAfterDisconnect", 3.0f);
    }

    public void GoToMenuAfterDisconnect()
    {
        GameManager.Instance.RestartCurrentSerie();
        GameManager.Instance.ResetPoints();
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

    public void EnablePanelWaiting()
    {
        panelWaitingMobile.SetActive(true);
    }

    public bool IsPanelWaitingEnabled()
    {
        return panelWaitingMobile.activeSelf;
    }

    public void ActivatePanelWinning()
    {
        panelWinning.SetActive(true);
    }

    public bool IsPanelWinningEnabled()
    {
        return panelWinning.activeSelf;
    }

    public void SetCodeRoomText(string room)
    {
        codeText.text = room;
    }

    public void SetIPText(string ip)
    {
        codeText.text = ip;
    }

    public string GetIPText()
    {
        return codeText.text;
    }

    public void ActivatePanelDisconnecting()
    {
        if(panelWinning.activeSelf)
        {
            panelWinning.SetActive(false);
        }
        panelDisconnecting.SetActive(true);
    }

    public void StartCountDown()
    {
        waitingText.text = "Coloquese en la posicion inicial";
        countDownText.gameObject.SetActive(true);
        codeText.gameObject.SetActive(false);
        InvokeRepeating("UpdateCountDown", 1.0f, 1.0f);

    }

    public void UpdateCountDown()
    {
        try
        {
            int aux = int.Parse(countDownText.text);
            aux--;
            if (aux > 0)
            {
                countDownText.text = aux.ToString();
            }
            else
            {
                //Desactivamos la cuenta atras
                CancelInvoke("UpdateCountDown");
                //desactivamos el panel de espera
                DisablePanelWaiting();
                GameManager.Instance.InitSave();
            }
        }
        catch
        {
            Debug.Log("Error al actualizar la cuenta atras");
        }
    }

    public void SetMobileDisconnectedText()
    {
        waitingText.text = "El móvil se ha desconectado \n Es necesario Reiniciar el juego";
        countDownText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        restartButton.onClick.AddListener(delegate { OnClickExit(); });
    }
}
