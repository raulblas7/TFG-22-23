using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Launcher : MonoBehaviourPunCallbacks, IOnEventCallback
{

    //Instancia del launcher
    private static Launcher _instance;

    public const byte DisconnectEvent = 2;
    [SerializeField] private PhotonView pcClient;
    [SerializeField] private Ball ball;
    [SerializeField] GameUIManager UIManager;



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

           // DontDestroyOnLoad(_instance);
        }
    }
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
            //PhotonNetwork.LoadLevel("MainScene");
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

            //transform.rotation = rotateOrient;
            ball.CheckIfCanThrow(rotateOrient);
        }
        else if(eventCode == MobileClient.ConnectEvent)
        {
            //cuando el movil nos avise de que se ha conectado
            //quitamos el panel de esperando conexion
            UIManager.DesactiveWaitingConexion();
            //iniciamos el juego
            GameManager.Instance.InitGame();
        }
    }


    public void DisconectOnChanceScene()
    {
        Debug.Log("Disconect Launcher");
        SendMessageToMobile();
        //PhotonNetwork.Disconnect();
        Invoke("DisconectLauncher", 3);
    }


    public void DisconectLauncher()
    {
        PhotonNetwork.Disconnect();
        GameManager.Instance.ChangeScene("MainMenu");
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
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void SetBall(Ball b)
    {
        ball = b;
    }

 
}
