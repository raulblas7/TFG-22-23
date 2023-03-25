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

    // Variables Menu Settings, solo tendrán valor en dicho menu
    [SerializeField] private TextMeshProUGUI textInvalidInput;
    [SerializeField] private TextMeshProUGUI textInvalidInput2;
    [SerializeField] private TextMeshProUGUI textInvalidInput3;
    [SerializeField] private TMP_InputField mainInputField;
    [SerializeField] private TMP_InputField speedField;
    [SerializeField] private TMP_InputField angleField;

    // Variables de UI en CircuitGameScene, solo tendrán valor en dicha escena
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI lapsText;
    [SerializeField] private GameObject panelWaitingMobile;
    [SerializeField] private GameObject panelDisconnecting;
    [SerializeField] private GameObject panelWinning;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI ipAndCountDownText;

    private void Start()
    {
        if (panelDisconnecting != null) panelDisconnecting.SetActive(false);
        if (panelWinning != null) panelWinning.SetActive(false);
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
    }

    public void OnClickPlay()
    {
        GameManager.Instance.LoadScene("CircuitGameScene");
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
        if (angleField != null)
        {
            angleField.onValueChanged.AddListener(delegate { ValueChangeCheckAngle(); });
        }
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        try
        {
            int n = int.Parse(mainInputField.text);
            GameManager.Instance.SetNumLaps(n);
        }
        catch
        {
            textInvalidInput.enabled = true;
        }
    }

    public void ValueChangeCheckAngle()
    {
        try
        {
            float n = float.Parse(angleField.text);
            GameManager.Instance.SetAngleToDoIt(n);
        }
        catch
        {
            textInvalidInput3.enabled = true;
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
            case "CircuitGameScene":
                SetUIManagerInGameManager();
                break;
            default:
                break;
        }
    }

    public void SetlapsText(int points)
    {
        if (lapsText != null)
        {
            lapsText.text = "" + points;
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
            }
        }
        catch
        {
            Debug.Log("Error al actualizar la cuenta atras");
        }
    }
}
