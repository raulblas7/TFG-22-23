using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

public class UDPServer
{
    private volatile bool serverActive = true;
    //pruebas
    private string receivedData = "prueba";
    private volatile string localAddress = "----";
    private volatile int localPort = -1;

    private IPAddress a;

    byte[] data;

    public void StartUdpServer(/*string ipAddress , */int port)
    {
        //IPEndPoint IpEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        //UdpClient udpServer = new UdpClient(IpEndPoint);
        localPort= port;
        UdpClient udpServer = new UdpClient(port);
        a = GetAddress();
        IPEndPoint anyIp = new IPEndPoint(a, port);

        do
        {
            data = udpServer.Receive(ref anyIp);
        }
        while (data.Length < 1);

        receivedData = data[0].ToString();

        // Obtener la dirección IP y el puerto local del UDPClient
        //IPEndPoint localEndPoint = (IPEndPoint)udpServer.Client.LocalEndPoint;
        //localAddress = localEndPoint.Address.ToString();
        //localPort = localEndPoint.Port;
        ////IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        ////udpServer.Connect(IPAddress.Any, port);
        //while (serverActive)
        //{
        //    Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
        //    receivedData = Encoding.ASCII.GetString(receiveBytes);
        //    Console.WriteLine("Received data: " + receivedData);
        //}
    }
    //metodo de prueba
    public string GetData() { return receivedData; }
    public IPAddress GetIp() { return a; }
    public int GetPort() { return localPort; }
    public void StopUdpServer()
    {
        serverActive = false;
    }

    private IPAddress GetAddress()
    {

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif
                {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.Address;
                    }
                }
            }
        }
        return null;
    }






}
