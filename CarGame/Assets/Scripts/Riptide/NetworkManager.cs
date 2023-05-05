using RiptideNetworking;
using RiptideNetworking.Utils;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _instance;
    public static NetworkManager Instance { get { return _instance; } }

    public Server Server { get; private set; }
    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;
    [SerializeField] private CarController carController;
    private static float orientationX = 0;
    private static float orientationY = 0;
    private static float orientationZ = 0;

    private float currentTime = 0.0f;


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
            currentTime += Time.deltaTime;
            if (currentTime >= 0.1f)
            {
                Quaternion q = new Quaternion();
                Vector3 aux = new Vector3(orientationX, orientationY, orientationZ);
                q.eulerAngles = aux;
                carController.GetRotationFromDevice(q);
                currentTime = 0.0f;
            }
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
    [MessageHandler((ushort)MessageID.orientationX)]
    private static void ReceiveOrientationXFromDevice(ushort fromClientId, Message message)
    {
        orientationX = message.GetFloat();
    }

    [MessageHandler((ushort)MessageID.orientationY)]
    private static void ReceiveOrientationYFromDevice(ushort fromClientId, Message message)
    {
        orientationY = message.GetFloat();
    }

    [MessageHandler((ushort)MessageID.orientationZ)]
    private static void ReceiveOrientationZFromDevice(ushort fromClientId, Message message)
    {
        orientationZ = message.GetFloat();
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
