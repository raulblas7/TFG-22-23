using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Launcher : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public const byte DisconnectEvent = 2;
    [SerializeField] private PhotonView pcClient;
    [SerializeField] private CarController carController;

    void Start()
    {
        // Se conecta a photon usando el appId asignado en el PUN Wizard de Unity
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        PhotonNetwork.JoinRandomRoom();
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
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        Debug.Log("Entro al OnEvent");
        byte eventCode = photonEvent.Code;
        if (eventCode == MobileClient.RotateEvent)
        {
            Debug.Log("El event code coincide");
            object[] data = (object[])photonEvent.CustomData;
            Quaternion rotateOrient = (Quaternion)data[0];
            Debug.Log("Rotate orient es: " + rotateOrient);

            carController.GetRotationFromDevice(rotateOrient);
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
        SendMessageToMobile();
        Invoke("DisconectLauncher", 3);
    }

    public void DisconectLauncher()
    {
        PhotonNetwork.Disconnect();
        GameManager.Instance.LoadScene("MainMenu");
    }

    public void SendMessageToMobile()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        if (!PhotonNetwork.RaiseEvent(DisconnectEvent, null, raiseEventOptions, SendOptions.SendReliable))
        {
            Debug.Log("No he podido mandar el mensaje");
        }
        else Debug.Log("Mensaje enviado");
    }
}