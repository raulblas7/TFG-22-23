using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private PuntuationUIManager _puntuationUIManager;
    [SerializeField] private TextMeshProUGUI finalPoints;

    //final panel
    [SerializeField] private GameObject finalPanel;
    [SerializeField] private TextMeshProUGUI seriesText;
    [SerializeField] private TextMeshProUGUI seriesCountDownText;
    [SerializeField] private Button returnToMenuButton;


    //Waiting Panel
    [SerializeField] private GameObject waitingConexionPanel;
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private GameObject desconexionPanel;
    [SerializeField] private Button restartButton;

    private int seriesCountDown = 0;


    void Start()
    {
        //desactivamos el finalPanel
        GameManager.Instance.SetGameUIManager(this);
        finalPanel.SetActive(false);
        ActiveWaitingConexion();
        desconexionPanel.SetActive(false);
    }


    void Update()
    {

    }

    public void FirstShootPuntuation(int round, int points)
    {
        _puntuationUIManager.FirstShootPuntuation(round, points);
    }

    public void EndRoundPuntuation(int round, int shootpoints, int totalRoundPoints)
    {
        _puntuationUIManager.EndRoundPuntuation(round, shootpoints, totalRoundPoints);
    }

    public void ActiveFinalPanel(int points, int currentSerie, int maxSerie)
    {
        //activamos el finalPanel
        finalPanel.SetActive(true);
        finalPoints.text = points.ToString();
        seriesText.text = "Serie " +currentSerie.ToString() + "/" + maxSerie.ToString();
        seriesCountDownText.gameObject.SetActive(false);
        returnToMenuButton.gameObject.SetActive(false);
    }

    public void ActiveReturnToMenuButton()
    {
        returnToMenuButton.gameObject.SetActive(true);
    }

    public void StartCountDown(int waitingTime)
    {
        seriesCountDownText.gameObject.SetActive(true);
        seriesCountDown = waitingTime;
        seriesCountDownText.text = "Siguiente serie en " + seriesCountDown.ToString();

        InvokeRepeating("UpdateCountdown", 1.0f, 1.0f);
    }
    private void UpdateCountdown()
    {
        seriesCountDown--;
        seriesCountDownText.text = "Siguiente serie en " + seriesCountDown.ToString();
        if(seriesCountDown <= 0)
        {
            CancelInvoke("UpdateCountdown");
            finalPanel.gameObject.SetActive(false);
            //reiniciamos el juego
            PlayerMovement.Instance.SetState(Movement.DOWN);
            RestartGame();
            //reiniciamos la puntuacion
            _puntuationUIManager.ResetPuntuation();
            //iniciamos de nuevo el juego
            GameManager.Instance.InitGame();
        }
    }

    public void ActiveWaitingConexion()
    {
        waitingConexionPanel.SetActive(true);
        countDownText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    public void DesactiveWaitingConexion()
    {
        waitingConexionPanel.SetActive(false);
    }

    public void StartCountDown()
    {
        infoText.text = "Coloquese en la posicion inicial";
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
            if(aux > 0)
            {
                countDownText.text = aux.ToString();
            }
            else
            {
                //Desactivamos la cuenta atras
                CancelInvoke("UpdateCountDown");
                DesactiveWaitingConexion();
                GameManager.Instance.InitGame();
            }
        }
        catch
        {
            Debug.Log("Error al actualizar la cuenta atras");
        }
    }

    public void ActiveDesconexionPanel()
    {
        desconexionPanel.SetActive(true);
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }

    //Deprecated
    public void SetCodeRoomText(string id)
    {
        codeText.text = id;
    }
    public void SetIPText(string ip)
    {
        codeText.text = ip;
    }

    public void ChangeInfoTest(string info)
    {
        infoText.text = info;
    }

    public void ActiveRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }

    public void DesactiveGame()
    {
        GameManager.Instance.DesactiveGame();
    }

}
