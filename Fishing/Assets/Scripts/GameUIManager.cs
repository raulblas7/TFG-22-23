using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI fishCountDownText;
    [SerializeField] private TextMeshProUGUI fishCountText;
    [SerializeField] private TextMeshProUGUI returnToinitPosText;

    //final panel
    [SerializeField] private GameObject finalPanel;
    [SerializeField] private TextMeshProUGUI finalPoints;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI seriesText;
    [SerializeField] private TextMeshProUGUI finalCountDownText;

    //Waiting Panel
    [SerializeField] private GameObject waitingConexionPanel;
    [SerializeField] private TextMeshProUGUI IpText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private Button restartButton;


    private int maxFish;
    private float currentTimeCountDown;
    private int currentFinalCountDown;


    void Start()
    {
        GameManager.Instance.SetUImanager(this);
        maxFish = GameManager.Instance.GetMaxFish();
        fishCountText.text = "0" + "/" + maxFish.ToString();
        fishCountDownText.gameObject.SetActive(false);
        finalPanel.SetActive(false);
        returnToinitPosText.gameObject.SetActive(false);
        ActiveWaitingConexion();

    }

    public void ActiveFishCountDown(float startValue)
    {
        fishCountDownText.gameObject.SetActive(true);
        currentTimeCountDown = startValue;
        fishCountDownText.text = currentTimeCountDown.ToString("0.0");
        InvokeRepeating("UpdateFishCountDown", 0.0f, 0.01f);
    }

    private void UpdateFishCountDown()
    {
        currentTimeCountDown -= 0.01f;
        fishCountDownText.text = currentTimeCountDown.ToString("0.0");

        if(currentTimeCountDown <= 0) // si termina la cuenta atras
        {
            CancelInvoke("UpdateFishCountDown");
            fishCountDownText.gameObject.SetActive(false);
        }
    }
    public void CancelFishCountDown()
    {
        
        if (fishCountDownText.gameObject.activeSelf) // si sigue activo
        {
            CancelInvoke("UpdateFishCountDown");
            fishCountDownText.gameObject.SetActive(false);
        }
    }

    public void UpdatePoints(int points)
    {
        pointsText.text = points.ToString();
    }

    public void UpdateFishCount(int count)
    {
        fishCountText.text = count.ToString() + "/" + maxFish.ToString();
    }

    public void ActiveFinalPanel(int points, int currentSerie, int totalSerie)
    {
        //activamos el finalPanel
        finalPanel.SetActive(true);
        finalPoints.text = points.ToString();
        mainMenuButton.gameObject.SetActive(false);
        finalCountDownText.gameObject.SetActive(false);
        seriesText.text = currentSerie.ToString() + "/" + totalSerie.ToString();
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
        IpText.gameObject.SetActive(false);
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
                DesactiveWaitingConexion();
                GameManager.Instance.InitGame();
            }
        }
        catch
        {
            Debug.Log("Error al actualizar la cuenta atras");
        }
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
        GameManager.Instance.restartSeries();
    }

    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }

    public void SetIPText(string ip)
    {
        IpText.text = ip;
    }

    public void ChangeInfoTest(string info)
    {
        infoText.text = info;
    }

    public void ActiveRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }
    public void ActiveMainMenuButton()
    {
        mainMenuButton.gameObject.SetActive(true);
    }

    public void ActiveFinalCountDown(int startValue)
    {
        finalCountDownText.gameObject.SetActive(true);
        currentFinalCountDown = startValue;
        finalCountDownText.text = "Siguente Serie en " + currentFinalCountDown.ToString();
        InvokeRepeating("UpdateFinalCountDown", 0.0f, 1.0f);
    }

    private void UpdateFinalCountDown()
    {
        currentFinalCountDown --;
        finalCountDownText.text = "Siguente Serie en " + currentFinalCountDown.ToString();

        if (currentFinalCountDown <= 0) // si termina la cuenta atras
        {
            CancelInvoke("UpdateFinalCountDown");
            finalPanel.SetActive(false);
            GameManager.Instance.InitGame();
        }
    }

    public void ActiveReturnToInitPos()
    {
        returnToinitPosText.gameObject.SetActive(true);
        Invoke("DesactiveReturnToInitPos", 3);
    }

    private void DesactiveReturnToInitPos()
    {
        returnToinitPosText.gameObject.SetActive(false);
    }

    public void DesactiveGame()
    {
        GameManager.Instance.DesactiveGame();
    }
}
