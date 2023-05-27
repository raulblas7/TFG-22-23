using RiptideNetworking;
using RiptideNetworking.Utils;
using System.Net.NetworkInformation;
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
    [SerializeField] private GameUIManager UiManager;
    [SerializeField] private FishingRod rod;
    private static float orientationX = 0;
    private static float orientationY = 0;
    private static float orientationZ = 0;

    private float currentTime = 0;


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
        //GameManager.Instance.SetNetworkManager(this);
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.ClientDisconnected += ClientLeft;
        Server.ClientConnected += ClientConnect;
        string ip = GetIp();
        if (ip != null)
        {
            //Sacamos la ip por pantalla
            UiManager.SetIPText(ip);
        }
    }

    private void Update()
    {
        if (Server.ClientCount == 1 && GameManager.Instance.IsGameActive())
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 0.1f) // 10 veces por segundo
            {
                currentTime = 0;
                Quaternion q = new Quaternion();
                Vector3 aux = new Vector3(orientationX, orientationY, orientationZ);
                q.eulerAngles = aux;
                //PlayerMovement.Instance.CheckIfCanThrow(q);
                rod.CheckIfApplyForce(q);

            }

        }

    }

    private void FixedUpdate()
    {
        Server.Tick();
    }

    private void OnApplicationQuit()
    {
        //Todo esto no tendria que ir aqui, es para hacer pruebas
        Server.Stop();
    }

    public void StopServer()
    {
        Server.Stop();
    }

    private void ClientLeft(object sender, ClientDisconnectedEventArgs e)
    {
        //Paramos el juego
        GameManager.Instance.DesactiveGame();
        //Reiniciamos el juego
        UiManager.ActiveWaitingConexion();
        UiManager.ChangeInfoTest("El móvil se ha desconectado \n Es necesario Reiniciar el juego");
        UiManager.ActiveRestartButton();
        StopServer();

    }

    private void ClientConnect(object sender, ServerClientConnectedEventArgs e)
    {
        //Activar cuenta atras 
        Debug.Log("cliente conectado");
        UiManager.StartCountDown();
    }



    //recibir el mensaje con la orientacion
    [MessageHandler((ushort)MessageID.orientationX)]
    private static void ReceiveOrientationXFromDevice(ushort fromClientId, Message message)
    {
        //Debug.Log("mensaje recibido " + message.GetFloat());
        orientationX = message.GetFloat();
        //Debug.Log("Orientacion en x es: " + message.GetFloat());

    }

    [MessageHandler((ushort)MessageID.orientationY)]
    private static void ReceiveOrientationYFromDevice(ushort fromClientId, Message message)
    {
        //Debug.Log("mensaje recibido " + message.GetFloat());
        orientationY = message.GetFloat();
        //Debug.Log("Orientacion en x es: " + message.GetFloat());

    }

    [MessageHandler((ushort)MessageID.orientationZ)]
    private static void ReceiveOrientationZFromDevice(ushort fromClientId, Message message)
    {
        //Debug.Log("mensaje recibido " + message.GetFloat());
        orientationZ = message.GetFloat();
        //Debug.Log("Orientacion en x es: " + message.GetFloat());

    }


    private string GetIp()
    {
        bool hasGateway = false;

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif
            {
                foreach (GatewayIPAddressInformation gatewayIp in item.GetIPProperties().GatewayAddresses)
                {
                    if (gatewayIp.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        hasGateway = true;
                    }
                }
                if (hasGateway)
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
        }
        return null;
    }
}
