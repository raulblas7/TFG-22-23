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
        ipAddress = "127.0.0.1";
        GameManager.Instance.GetUIManager().SetIPText(ipAddress);
        GameManager.Instance.GetUIManager().SetPORTText(port.ToString());
        // Create a new thread for the server
        Thread serverThread = new Thread(() => udpServer.StartUdpServer(ipAddress, port));
        serverThread.Start();

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
