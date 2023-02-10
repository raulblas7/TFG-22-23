using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelPuntuation : MonoBehaviour
{
    
    [SerializeField] TMPro.TextMeshProUGUI roundText;
    [SerializeField] TMPro.TextMeshProUGUI firstShoot;
    [SerializeField] TMPro.TextMeshProUGUI secondShoot;
    [SerializeField] TMPro.TextMeshProUGUI roundPoints;
    [SerializeField] RectTransform _transform;

    private void Start()
    {
        _transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 11.3274f);
        _transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.width / 11.3274f);
    }
    public void SetFirstShoot(int points)
    {
        if (points == 10)
        {
            firstShoot.text = "X";
        }
        else { firstShoot.text = points.ToString(); }
    }

    public void SetSecondShoot(int points)
    {
        if (points == 10)
        {
            secondShoot.text = "X";
        }
        else { secondShoot.text = points.ToString(); }
    }

   public void SetRoundPoints(int points)
    {
        roundPoints.text = points.ToString();

    }

    public void SetRound(int round)
    {
        roundText.text = (round + 1).ToString();
    }

    public void ResetPanel()
    {
        firstShoot.text = " ";
        secondShoot.text = " ";
        roundPoints.text = " ";
    }
}
