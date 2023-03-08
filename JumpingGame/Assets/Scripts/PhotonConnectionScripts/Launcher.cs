using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Launcher : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public const byte DisconnectEvent = 2;

    [SerializeField] private PhotonView pcClient;
    [SerializeField] private PlayerController playerController;

    private Player playerConnected;
    private const string id_room = "JG-";
    private string id;

    private const string id_validate_room = "VJG-";
    private string idValidate;

    void Start()
    {
        // Se conecta a photon usando el appId asignado en el PUN Wizard de Unity
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        //PhotonNetwork.JoinRandomRoom();
        int n1 = Random.Range(0, 9999);
        int n2 = Random.Range(0, 9999);

        id = id_room + n1 + "-" + n2;

        GameManager.Instance.GetUIManager().SetCodeRoomText(id);
        JoinOrCreatePrivateRoom(id);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("There are no clients");
        PhotonNetwork.CreateRoom(null, new RoomOptions { PublishUserId = true, MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room succesfully: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.Instantiate(pcClient.name, Vector3.zero, Quaternion.identity);
        Debug.Log("Num players " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            int n1 = Random.Range(0, 9999);
            int n2 = Random.Range(0, 9999);

            idValidate = id_validate_room + n1 + "-" + n2;
            // Mostramos el código de validación en el UI
            GameManager.Instance.GetUIManager().SetCodeRoomText(idValidate);

            playerConnected = newPlayer;
        }
    }

    private void JoinOrCreatePrivateRoom(string nameEveryFriendKnows)
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 2 };
        roomOptions.IsVisible = false;
        PhotonNetwork.JoinOrCreateRoom(nameEveryFriendKnows, roomOptions, null);
    }

    public void OnEvent(EventData photonEvent)
    {
        Debug.Log("Entro al OnEvent");
        byte eventCode = photonEvent.Code;
        if (eventCode == MobileClient.RotateEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            Quaternion rotateOrient = (Quaternion)data[0];
            Debug.Log("Rotate orient es: " + rotateOrient);

            playerController.CheckIfCanJump(rotateOrient);
        }
        if (eventCode == MobileClient.ValidateEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            string validateId = (string)data[0];

            if(validateId.Equals(idValidate))
            {
                Debug.Log("Desactivo el panel");
                GameManager.Instance.GetUIManager().DisablePanelWaiting();
            }
            else
            {
                SendMessageToMobile(DisconnectEvent);
                PhotonNetwork.CurrentRoom.IsOpen = true;
                GameManager.Instance.GetUIManager().SetCodeRoomText(id);
            }
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

    public void DisconectOnChangeScene()
    {
        Debug.Log("Disconect Launcher");
        SendMessageToMobile(DisconnectEvent);
        Invoke("DisconectLauncher", 3.0f);
    }

    private void DisconectLauncher()
    {
        PhotonNetwork.Disconnect();
        GameManager.Instance.LoadScene("MainMenu");
    }

    public void SendMessageToMobile(byte eventToSend)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        if (!PhotonNetwork.RaiseEvent(eventToSend, null, raiseEventOptions, SendOptions.SendReliable))
        {
            Debug.Log("No he podido mandar el mensaje");
        }
        else Debug.Log("Mensaje enviado");
    }
}
