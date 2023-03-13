using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    private UDPServer udpServer;
    private string ipAddress;
    private int port;

    void Start()
    {
        udpServer = new UDPServer();

        port = 12345;
        //ipAddress = "127.0.0.1";
        // Create a new thread for the server
        Thread serverThread = new Thread(() => udpServer.StartUdpServer(/*ipAddress,*/ port));
        serverThread.Start();

        Invoke("AddTexts", 3.0f);
    }

    private void AddTexts()
    {
        GameManager.Instance.GetUIManager().SetIPText(udpServer.GetIp().ToString());
        GameManager.Instance.GetUIManager().SetPORTText(port.ToString());
    }

    private void Update()
    {
        //pruebas
        Debug.Log(udpServer.GetData());
        Debug.Log(udpServer.GetIp());
        Debug.Log(udpServer.GetPort());
    }
    public string GetIpAddress()
    {
        return ipAddress;
    }

    public int GetPort()
    {
        return port;
    }
}
