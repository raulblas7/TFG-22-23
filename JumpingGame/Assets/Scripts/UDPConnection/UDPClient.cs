using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPClient
{
    UdpClient udpClient;
    IPEndPoint reciverEndPoint;
    string ipAddress;
    int port;

    public void StartUdpClient(string ipAddress, int port)
    {
        reciverEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port); // direccion y puerto del destinatario
        udpClient = new UdpClient(port);
        this.ipAddress = ipAddress;
        this.port = port;
    }

    public void SendMessageToServer(string message)
    {
        byte[] sendBytes = Encoding.ASCII.GetBytes(message);
        //udpClient.Send(sendBytes, sendBytes.Length, ipAddress, port);
        udpClient.Send(sendBytes, sendBytes.Length, reciverEndPoint);
    }
}
