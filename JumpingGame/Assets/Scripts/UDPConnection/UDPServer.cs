using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPServer
{
    private volatile bool serverActive = true;
    //pruebas
    private string receivedData = "prueba";
    private volatile string localAddress = "----";
    private volatile int localPort = -1;

    public void StartUdpServer(string ipAddress , int port)
    {
        IPEndPoint IpEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        //UdpClient udpServer = new UdpClient(port);
        UdpClient udpServer = new UdpClient(IpEndPoint);

        // Obtener la dirección IP y el puerto local del UDPClient
        IPEndPoint localEndPoint = (IPEndPoint)udpServer.Client.LocalEndPoint;
        localAddress = localEndPoint.Address.ToString();
        localPort = localEndPoint.Port;
        //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        //udpServer.Connect(IPAddress.Any, port);
        while (serverActive)
        {
            Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
            receivedData = Encoding.ASCII.GetString(receiveBytes);
            Console.WriteLine("Received data: " + receivedData);
        }
    }
    //metodo de prueba
    public string GetData() { return receivedData; }
    public string GetIp() { return localAddress; }
    public int GetPort() { return localPort; }
    public void StopUdpServer()
    {
        serverActive = false;
    }








}
