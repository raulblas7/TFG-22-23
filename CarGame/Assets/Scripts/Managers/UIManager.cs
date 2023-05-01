using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Variables Menu Main, solo tendrán valor en dicho menu
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button exitInMenuButton;

    // Variables Menu Settings, solo tendrán valor en dicho menu
    [SerializeField] private TMP_InputField repsField;
    [SerializeField] private TMP_InputField lapsField;
    [SerializeField] private TMP_InputField angleField;
    [SerializeField] private TMP_Dropdown difficultyDropdown;
    [SerializeField] private Button returnToMenu;

    // Variables de UI en CircuitGameScene, solo tendrán valor en dicha escena
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI lapsText;
    [SerializeField] private TextMeshProUGUI repsText;
    [SerializeField] private GameObject panelWaitingMobile;
    [SerializeField] private GameObject panelDisconnecting;
    [SerializeField] private GameObject panelWinning;
    [SerializeField] private GameObject panelFinishSerie;
    [SerializeField] private TextMeshProUGUI countDownSerieText;
    [SerializeField] private TextMeshProUGUI counterSeriesText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI ipAndCountDownText;

    private void Start()
    {
        if (panelDisconnecting != null) panelDisconnecting.SetActive(false);
        if (panelWinning != null) panelWinning.SetActive(false);
        if (lapsText != null)
        {
            lapsText.text = lapsText.text + GameManager.Instance.GetCurrentLaps() + " de " + GameManager.Instance.GetLaps();
        }
        if (repsText != null)
        {
            repsText.text = repsText.text + GameManager.Instance.GetCurrentReps() + " de " + GameManager.Instance.GetReps();
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
            infoButton.onClick.AddListener(delegate { OnClickInfo(); });
        }

        if (exitInMenuButton != null)
        {
            exitInMenuButton.onClick.AddListener(delegate { GameManager.Instance.QuitApplication(); });
        }
    }

    public void OnClickPlay()
    {
        GameManager.Instance.LoadScene("CircuitGameScene");
    }

    public void OnClickSettings()
    {
        GameManager.Instance.LoadScene("SettingsMenu");
    }

    public void OnClickInfo()
    {
        GameManager.Instance.LoadScene("InfoScene");
    }

    public void OnClickReturnToMenu()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }

    public void SetInputFieldListener()
    {
        if (repsField != null)
        {
            repsField.onValueChanged.AddListener(delegate { ValueChangeRepsCheck(); });
            repsField.text = "" + GameManager.Instance.GetReps();
        }
        // Añade un listener onValueChanged al inputField de settings si existe
        if (lapsField != null)
        {
            lapsField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
            lapsField.text = "" + GameManager.Instance.GetLaps();
        }
        // Añade un listener onValueChanged al inputField de la velocidad de las rocas si existe
        if (angleField != null)
        {
            angleField.onValueChanged.AddListener(delegate { ValueChangeCheckAngle(); });
            angleField.text = "" + GameManager.Instance.GetAngleToDoIt();
        }
        if(difficultyDropdown != null)
        {
            difficultyDropdown.onValueChanged.AddListener(delegate { ValueChangeDropdown(); });
            difficultyDropdown.value = (int)GameManager.Instance.GetDifficulty();
        }
        if (returnToMenu != null)
        {
            returnToMenu.onClick.AddListener(delegate { OnClickSafeConfig(); });
            returnToMenu.onClick.AddListener(delegate { OnClickReturnToMenu(); });
        }
    }

    public void SetInfoSceneListeners()
    {
        if (returnToMenu != null)
        {
            returnToMenu.onClick.AddListener(delegate { OnClickReturnToMenu(); });
        }
    }

    public void ValueChangeRepsCheck()
    {
        try
        {
            int n = int.Parse(repsField.text);
            GameManager.Instance.SetNumReps(n);
        }
        catch
        {
            Debug.Log("Valor introducido no valido");
        }
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        try
        {
            int n = int.Parse(lapsField.text);
            GameManager.Instance.SetNumLaps(n);
        }
        catch
        {
            Debug.Log("Valor introducido no valido");
        }
    }

    public void ValueChangeDropdown()
    {
        GameManager.Instance.SetDifficulty(difficultyDropdown.value);
    }

    public void ValueChangeCheckAngle()
    {
        try
        {
            int n = int.Parse(angleField.text);
            GameManager.Instance.SetAngleToDoIt(n);
        }
        catch
        {
            Debug.Log("Valor introducido no valido");
        }
    }

    public void OnClickExit()
    {
        ActivatePanelDisconnecting();
        Invoke("GoToMenuAfterDisconnect", 3.0f);
    }

    public void GoToMenuAfterDisconnect()
    {
        GameManager.Instance.SetNumCurrentLaps(0);
        GameManager.Instance.SetCurrentReps(0);
        GameManager.Instance.LoadScene("MainMenu");
    }

    public void OnClickSafeConfig()
    {
        GameManager.Instance.SafeConfig();
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
            case "InfoScene":
                SetInfoSceneListeners();
                break;
            case "CircuitGameScene":
                SetUIManagerInGameManager();
                break;
            default:
                break;
        }
    }

    public void SetlapsText()
    {
        if (lapsText != null)
        {
            lapsText.text = "serie " + GameManager.Instance.GetCurrentLaps() + " de " + GameManager.Instance.GetLaps();
        }
    }

    public void SetRepsText()
    {
        if (repsText != null)
        {
            repsText.text = "Repeticion " + GameManager.Instance.GetCurrentReps() + " de " + GameManager.Instance.GetReps();
        }
    }

    public void TellGameManagerQuitApp()
    {
        GameManager.Instance.QuitApplication();
    }

    public void DisablePanelWaiting()
    {
        panelWaitingMobile.SetActive(false);
        lapsText.gameObject.SetActive(true);
        repsText.gameObject.SetActive(true);
    }

    public void EnablePanelWaiting()
    {
        panelWaitingMobile.SetActive(true);
        lapsText.gameObject.SetActive(false);
        repsText.gameObject.SetActive(false);
    }

    public bool IsPanelWaitingEnabled()
    {
        return panelWaitingMobile.activeSelf;
    }

    public bool IsPanelWinningEnabled()
    {
        return panelWinning.activeSelf;
    }

    public bool IsPanelDisconectingEnabled()
    {
        return panelDisconnecting.activeSelf;
    }

    public void SetCodeRoomText(string room)
    {
        ipAndCountDownText.text = room;
    }

    public void SetIPText(string ip)
    {
        ipAndCountDownText.text = ip;
    }

    public string GetIPText()
    {
        return ipAndCountDownText.text;
    }

    public void ActivatePanelDisconnecting()
    {
        if (panelWaitingMobile.activeSelf)
        {
            panelWaitingMobile.SetActive(false);
        }
        if (panelWinning.activeSelf)
        {
            panelWinning.SetActive(false);
        }
        panelDisconnecting.SetActive(true);
    }

    public void StartCountDown()
    {
        infoText.text = "Coloquese en la posicion inicial";
        ipAndCountDownText.text = "15";
        //countDownText.gameObject.SetActive(true);
        //ipAndCountDownText.gameObject.SetActive(false);
        InvokeRepeating("UpdateCountDown", 1.0f, 1.0f);
    }

    public void SetTextMobileDisconnected()
    {
        infoText.text = "El movil se ha desconectado \n Es necesario Reiniciar el juego, pulsa el botón de salir";
        ipAndCountDownText.gameObject.SetActive(false);
    }

    public void UpdateCountDown()
    {
        try
        {
            int aux = int.Parse(ipAndCountDownText.text);
            aux--;
            if (aux > 0)
            {
                ipAndCountDownText.text = aux.ToString();
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

    public void GameFinished()
    {
        panelWinning.SetActive(true);
    }

    public void ActivatePanelFinishSerie()
    {
        if(panelFinishSerie != null)
        {
            panelFinishSerie.SetActive(true);
            counterSeriesText.text = "serie " + GameManager.Instance.GetCurrentLaps() + " de " + GameManager.Instance.GetLaps();
            InvokeRepeating("UpdateCountDownSerie", 1.0f, 1.0f);
        }
    }

    public void UpdateCountDownSerie()
    {
        try
        {
            int aux = int.Parse(countDownSerieText.text);
            aux--;
            if (aux > 0)
            {
                countDownSerieText.text = aux.ToString();
            }
            else
            {
                //Desactivamos la cuenta atras
                CancelInvoke("UpdateCountDownSerie");
                panelFinishSerie.SetActive(false);
                countDownSerieText.text = "10";
            }
        }
        catch
        {
            Debug.Log("Error al actualizar la cuenta atras");
        }
    }

    public bool IsPanelFinishSerieEnabled()
    {
        return panelFinishSerie.activeSelf;
    }
}
