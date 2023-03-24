using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI fishCountText;


    private int maxFish;
    private float currentTimeCountDown;
    void Start()
    {
        GameManager.Instance.SetUImanager(this);
       // pointsText.text = "0";
        maxFish = GameManager.Instance.GetMaxFish();
        fishCountText.text = "0" + "/" + maxFish.ToString();
        countDownText.gameObject.SetActive(false);
    }

    public void ActiveCountDown(float startValue)
    {
        countDownText.gameObject.SetActive(true);
        currentTimeCountDown = startValue;
        countDownText.text = currentTimeCountDown.ToString("0.00");
        InvokeRepeating("UpdateCountDown", 0.0f, 0.01f);
    }

    private void UpdateCountDown()
    {
        currentTimeCountDown -= 0.01f;
        countDownText.text = currentTimeCountDown.ToString("0.00");

        if(currentTimeCountDown <= 0) // si termina la cuenta atras
        {
            CancelInvoke("UpdateCountDown");
            countDownText.gameObject.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
