using System;
using System.Net.Sockets;
using System.Text;

public class UDPClient
{
    UdpClient udpClient;
    string ipAddress;
    int port;

    public void StartUdpClient(string ipAddress, int port)
    {
        udpClient = new UdpClient();
        this.ipAddress = ipAddress;
        this.port = port;
    }

    public void SendMessageToServer(string message)
    {
        Byte[] sendBytes = Encoding.ASCII.GetBytes(message);
        udpClient.Send(sendBytes, sendBytes.Length, ipAddress, port);
    }
}
