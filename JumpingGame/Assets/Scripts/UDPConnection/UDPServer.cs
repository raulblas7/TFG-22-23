using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPServer
{
    private volatile bool serverActive = true;

    public void StartUdpServer(int port)
    {
        UdpClient udpServer = new UdpClient(port);
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        //udpServer.Connect(IPAddress.Any, port);
        while (serverActive)
        {
            Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
            string receivedData = Encoding.ASCII.GetString(receiveBytes);
            Console.WriteLine("Received data: " + receivedData);
        }
    }

    public void StopUdpServer()
    {
        serverActive = false;
    }
}
