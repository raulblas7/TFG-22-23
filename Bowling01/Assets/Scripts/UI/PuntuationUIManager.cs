using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntuationUIManager : MonoBehaviour
{
    [SerializeField] PanelPuntuation prefab;

    private List<PanelPuntuation> panels;
    void Start()
    {
        //le digo al game manager que yo soy el puntuationManager

        //consulto el numero de rondas
        //instancio un panel por cada ronda
        //inicializo los paneles
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
