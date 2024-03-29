
using RiptideNetworking;
using RiptideNetworking.Utils;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum MessageID
{
    orientationX = 1,
    orientationZ = 2,
    orientationY = 3,
}
public class NetworkManagerClient : MonoBehaviour
{
    private static NetworkManagerClient _instance;

    
    public static NetworkManagerClient Instance { get { return _instance; } }
    public Client Client { get; private set; }
    [SerializeField] private ushort port;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button connectButton;
    [SerializeField] private TMP_InputField codeRoomInputField;

    private string ip;
   

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

        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.Disconnected += DidDisconnect;

        if (connectButton != null)
        {
            connectButton.onClick.AddListener(delegate { StartConnexion(); });
        }
        if (codeRoomInputField != null)
        {
            codeRoomInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }
    }

    private void FixedUpdate()
    {
        Client.Tick();
    }

    void Update()
    {
        if (Client.IsConnected)
        {
            //Obtener la orientación del dispositivo
            Vector3 deviceAcceleration = Input.acceleration;

            // Aplicar la orientación al objeto
            Quaternion orientation = Quaternion.FromToRotation(Vector3.up, deviceAcceleration);
            Debug.Log("La orientacion es: " + orientation);
            SendMessageToPlayer(orientation);
        }
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    public void StartConnexion()
    {
        Client.Connect($"{ip}:{port}");
        text.text = "Conectandose a la ip: " + ip;
    }

    public void ValueChangeCheck()
    {
        try
        {
            ip = codeRoomInputField.text;
        }
        catch
        {
            Debug.Log("Algo ha ido mal introduciendo la IP");
        }
    }

    private void DidConnect(object sender, EventArgs e)
    {
        text.text = "conectado ";
        connectButton.interactable = false;
    }

    private void FailedToConnect(object sender, EventArgs e)
    {
        
        text.text = "Fallo al conectar: Asegurese de que el juego esta en marcha y la IP es correcta";

    }

    private void DidDisconnect(object sender, EventArgs e)
    {
        text.text = "Te has desconectado \n" + " Escribe debajo la ip que sale en tu ordenador:";
        connectButton.interactable = true;
    }

    private void SendMessageToPlayer(Quaternion orientation)
    {
        // Cambiarlo a unreliable
        //Message message = Message.Create(MessageSendMode.reliable, (ushort)MessageID.orientation);
        Message message = Message.Create(MessageSendMode.unreliable, (ushort)MessageID.orientationX);
        message.AddFloat(orientation.eulerAngles.x);
        Client.Send(message);
        message = Message.Create(MessageSendMode.unreliable, (ushort)MessageID.orientationY);
        message.AddFloat(orientation.eulerAngles.y);
        Client.Send(message);
        message = Message.Create(MessageSendMode.unreliable, (ushort)MessageID.orientationZ);
        message.AddFloat(orientation.eulerAngles.z);
        Client.Send(message);

    }


}
