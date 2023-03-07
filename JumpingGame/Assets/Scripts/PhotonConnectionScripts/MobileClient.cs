using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MobileClient : MonoBehaviourPunCallbacks, IOnEventCallback
{
    // If you have multiple custom events, it is recommended to define them in the used class
    public const byte RotateEvent = 1;
    public const byte ValidateEvent = 3;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button connectButton;
    [SerializeField] private TMP_InputField codeRoomInputField;

    private string id;

    void Start()
    {
        if(connectButton!= null)
        {
            connectButton.onClick.AddListener(delegate { StartConnexion(); });
        }

       // PhotonNetwork.ConnectUsingSettings();
       if (codeRoomInputField != null)
       {
            codeRoomInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
       }
    }

    public void ValueChangeCheck()
    {
        try
        {
            id = codeRoomInputField.text;
        }
        catch
        {
            Debug.Log("Algo ha ido mal introduciendo el texto del código de sala");
        }
    }

    public void StartConnexion()
    {
        if (id != null && id != string.Empty)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        //PhotonNetwork.JoinRandomRoom();

        PhotonNetwork.JoinRoom(id);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("There are no clients");
        PhotonNetwork.CreateRoom(null, new RoomOptions { PublishUserId = true, MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        // Para que puedas ser desconectado desde el master client :)
        PhotonNetwork.EnableCloseConnection = true;

        Debug.Log("Joined a room succesfully: " + PhotonNetwork.CurrentRoom.Name);
        text.text = "Escribe el código de validación que se muestra en tu ordenador:";
        //connectButton.interactable = false;
        codeRoomInputField.text = "";
        id = "";

        connectButton.onClick.RemoveAllListeners();
        connectButton.onClick.AddListener(delegate { SendMessageToPC(); });
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        text.text = "Has sido desconectado de la sala, introduce de nuevo el id de la sala";

        codeRoomInputField.text = "";
        id = "";

        connectButton.onClick.RemoveAllListeners();
        connectButton.onClick.AddListener(delegate { StartConnexion(); });
    }

    public void SendMessageToPC()
    {
        if (id != null && id != string.Empty)
        {
            SendMessageToPCClient(id);
        }
    }

    void Update()
    {
        //Obtener la orientación del dispositivo
        Vector3 deviceAcceleration = Input.acceleration;

        // Aplicar la orientación al objeto
        Quaternion orientation = Quaternion.FromToRotation(Vector3.up, deviceAcceleration);
        Debug.Log("La orientacion es: " + orientation);
        SendMessageToPlayer(orientation);
    }

    public void SendMessageToPlayer(Quaternion orient)
    {
        //text.text = "El quaternion de orientacion es: " + orient.ToString();
        object[] content = new object[] { orient };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(RotateEvent, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void SendMessageToPCClient(string id)
    {
        //text.text = "El quaternion de orientacion es: " + orient.ToString();
        object[] content = new object[] { id };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(ValidateEvent, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        text.text = "Entro al onEvent";
        byte eventCode = photonEvent.Code;
        if (eventCode == Launcher.DisconnectEvent)
        {
            text.text = "Me desconecto";
            PhotonNetwork.Disconnect();
            connectButton.interactable = true;
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
