using RiptideNetworking;
using RiptideNetworking.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _instance;
    //public static NetworkManager Instance
    //{
    //    get => instance;
    //    private set
    //    {
    //        if(instance != null)
    //        {
    //            instance = value;
    //        }
    //        else if(instance != value)
    //        {
    //            Debug.Log($"{nameof(NetworkManager)} instance already exists");
    //            Destroy(value);
    //        }
    //    }
    //}

    public static NetworkManager Instance { get { return _instance; } }

    public Server Server { get; private set; }
    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;

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
    }

    private void FixedUpdate()
    {
        Server.Tick();
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    private void ClientLeft(object sender, ClientDisconnectedEventArgs e)
    {
        //hacer algo
    }

    [MessageHandler((ushort)MessageID.orientation)]
    private static void ReceiveOrientationFromDevice(ushort fromClientId, Message message)
    {
        Debug.Log("Orientacion en x es: " + message.GetFloat());
    }
}
