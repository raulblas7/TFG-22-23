using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Launcher : MonoBehaviourPunCallbacks, IOnEventCallback
{
    //Instancia del launcher
    private static Launcher _instance;

    [SerializeField] private PhotonView pcClient;
    //[SerializeField] private PhotonView mobileClient;
    [SerializeField] private string roomName;

    private int pcClientId;

    private string masterServerAddress = "127.0.0.1";
    private int masterServerPort = 7777;
    private string appId = "38d0c2bc-8685-4a8c-bace-27623c2d6352";

    public static Launcher Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }

    void Start()
    {
        // try to connect
        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.ConnectToMaster(masterServerAddress, masterServerPort, appId);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        //CreateRoom();
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

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(pcClient.name, Vector3.zero, Quaternion.identity);

            pcClientId = pcClient.ViewID;
            Debug.Log("PcClient id: " + pcClientId);
            Debug.Log("Num players " + PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { PublishUserId = true, MaxPlayers = 2 });
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
            //PhotonNetwork.LoadLevel("MainScene");
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public string GetRoomName()
    {
        return roomName;
    }

    public int GetPcClientId()
    {
        return pcClientId;
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

            transform.rotation = rotateOrient;
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
