using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private PuntuationUIManager _puntuationUIManager;
    [SerializeField] private GameObject finalPanel;
    [SerializeField] private TextMeshProUGUI finalPoints;
    //Waiting Panel
    [SerializeField] private GameObject waitingConexionPanel;
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private GameObject desconexionPanel;


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

    public void ActiveFinalPanel(int points)
    {
        //activamos el finalPanel
        finalPanel.SetActive(true);
        finalPoints.text = points.ToString();
    }

    public void ActiveWaitingConexion()
    {
        waitingConexionPanel.SetActive(true);
        countDownText.gameObject.SetActive(false);
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

    public void SetCodeRoomText(string id)
    {
        codeText.text = id;
    }

}
