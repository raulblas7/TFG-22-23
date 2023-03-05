using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntuationUIManager : MonoBehaviour
{
    [SerializeField] PanelPuntuation prefab;
  

    private List<PanelPuntuation> panels;
    void Start()
    {
      
        //inicializo la lista
        panels = new List<PanelPuntuation>();
        //consulto el numero de rondas
        int rounds = GameManager.Instance.GetNumRounds();
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

 
}
