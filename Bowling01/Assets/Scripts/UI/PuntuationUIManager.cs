using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntuationUIManager : MonoBehaviour
{
    [SerializeField] PanelPuntuation prefab;
    [SerializeField] GameObject finalPanel;
    [SerializeField] TextMeshProUGUI FinalPoints;

    private List<PanelPuntuation> panels;
    void Start()
    {
        //desactivamos el finalPanel
        finalPanel.SetActive(false);
        //inicializo la lista
        panels = new List<PanelPuntuation>();
        //le digo al game manager que yo soy el puntuationManager
        GameManager.Instance.SetPuntuationUIManager(this);
        //consulto el numero de rondas
        int rounds = GameManager.Instance.getNumRounds();
        //instancio un panel por cada ronda
        for (int i = 0; i < rounds; i++)
        {
            PanelPuntuation panel = Instantiate<PanelPuntuation>(prefab, transform);
            //inicializo los paneles
            panel.SetRound(i);
            panel.ResetPanel();
            //añado el panel a la lista
            panels.Add(panel);

        }
    }

    public void FirstShootPuntuation(int round, int points)
    {
        panels[round].SetFirstShoot(points);
    }

    public void EndRoundPuntuation(int round, int shootpoints, int totalRoundPoints)
    {
        PanelPuntuation panel = panels[round];
        panel.SetSecondShoot(shootpoints);
        panel.SetRoundPoints(totalRoundPoints);
    }

    public void ActiveFinalPanel(int points)
    {
        //activamos el finalPanel
        finalPanel.SetActive(true);
        FinalPoints.text = points.ToString();
    }
}
