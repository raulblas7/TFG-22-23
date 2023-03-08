using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private PuntuationUIManager _puntuationUIManager;
    [SerializeField] private GameObject finalPanel;
    [SerializeField] private TextMeshProUGUI finalPoints;
    [SerializeField] private GameObject waitingConexionPanel;
    [SerializeField] private GameObject desconexionPanel;
    [SerializeField] private TextMeshProUGUI codeText;


    void Start()
    {
        //desactivamos el finalPanel
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
    }

    public void DesactiveWaitingConexion()
    {
        waitingConexionPanel.SetActive(false);
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
