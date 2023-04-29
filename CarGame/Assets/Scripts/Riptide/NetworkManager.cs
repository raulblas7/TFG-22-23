using RiptideNetworking;
using RiptideNetworking.Utils;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using System.Net.Sockets;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _instance;
    public static NetworkManager Instance { get { return _instance; } }

    public Server Server { get; private set; }
    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;
    [SerializeField] private CarController carController;
    private static float orientationX = 0;


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.ClientDisconnected += ClientLeft;
        Server.ClientConnected += ClientConnect;
        string ip = GetIp();
        if(ip != null)
        {
            //Sacamos la ip por pantalla
            GameManager.Instance.GetUIManager().SetIPText(ip);
        }
    }

    private void Update()
    {
        if(!GameManager.Instance.GetUIManager().IsPanelWaitingEnabled() 
            && !GameManager.Instance.GetUIManager().IsPanelWinningEnabled() && Server.ClientCount == 1)
        {
            Quaternion q = new Quaternion();
            Vector3 aux = new Vector3(orientationX, 0, 0);
            q.eulerAngles = aux;
            carController.GetRotationFromDevice(q);
        }
    }

    private void FixedUpdate()
    {
        Server.Tick();
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    public void StopServer()
    {
        Server.Stop();
    }

    private void ClientLeft(object sender, ClientDisconnectedEventArgs e)
    {
        GameManager.Instance.GetUIManager().EnablePanelWaiting();
        GameManager.Instance.GetUIManager().SetTextMobileDisconnected();
        GameManager.Instance.FinishSave();
    }

    private void ClientConnect(object sender, ServerClientConnectedEventArgs e)
    {
        //Activar cuenta atras 
        Debug.Log("cliente conectado");
        GameManager.Instance.GetUIManager().StartCountDown();
    }

    //recibir el mensaje con la orientacion
    [MessageHandler((ushort)MessageID.orientation)]
    private static void ReceiveOrientationFromDevice(ushort fromClientId, Message message)
    {
        //Debug.Log("mensaje recibido " + message.GetFloat());
        orientationX = message.GetFloat();
        //Debug.Log("Orientacion en x es: " + message.GetFloat());
     
    }


    private string GetIp()
    {

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif
            {
                //foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                //{
                //    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                //    {
                //        string a = ip.Address.ToString();
                //        return a;
                //    }
                //}
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        string ipAddress = ip.Address.ToString();
                        IPAddress subnetMask = ip.IPv4Mask;

                        // Comprobar si la dirección IP es una dirección IP privada de clase C
                        if (ipAddress.StartsWith("192.168.") && subnetMask.ToString() == "255.255.255.0")
                        {
                            return ipAddress;
                        }
                    }
                }
            }
        }
        return null;
    }
}
