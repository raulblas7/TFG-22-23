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

    //final panel
    [SerializeField] private GameObject finalPanel;
    [SerializeField] private TextMeshProUGUI finalPoints;

    //Waiting Panel
    [SerializeField] private GameObject waitingConexionPanel;
    [SerializeField] private TextMeshProUGUI IpText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private Button restartButton;


    private int maxFish;
    private float currentTimeCountDown;


    void Start()
    {
        GameManager.Instance.SetUImanager(this);
        maxFish = GameManager.Instance.GetMaxFish();
        fishCountText.text = "0" + "/" + maxFish.ToString();
        fishCountDownText.gameObject.SetActive(false);
        finalPanel.SetActive(false);
        ActiveWaitingConexion();

    }

    public void ActiveFishCountDown(float startValue)
    {
        fishCountDownText.gameObject.SetActive(true);
        currentTimeCountDown = startValue;
        fishCountDownText.text = currentTimeCountDown.ToString("0.00");
        InvokeRepeating("UpdateFishCountDown", 0.0f, 0.01f);
    }

    private void UpdateFishCountDown()
    {
        currentTimeCountDown -= 0.01f;
        fishCountDownText.text = currentTimeCountDown.ToString("0.00");

        if(currentTimeCountDown <= 0) // si termina la cuenta atras
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

}
