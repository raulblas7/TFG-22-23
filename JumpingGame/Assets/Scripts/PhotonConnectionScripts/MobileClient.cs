using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class MobileClient : MonoBehaviourPunCallbacks
{
    // If you have multiple custom events, it is recommended to define them in the used class
    public const byte RotateEvent = 1;

    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
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
        text.text = text.text + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    void Update()
    {
        //Obtener la orientación del dispositivo
        Vector3 deviceAcceleration = Input.acceleration;

        // Aplicar la orientación al objeto
        Quaternion orientation = Quaternion.FromToRotation(Vector3.up, deviceAcceleration);
        Debug.Log("La orientacion es: " + orientation);
        SendMessageToPlayer(0, orientation);
    }

    public void SendMessageToPlayer(int playerId, Quaternion orient)
    {
        text.text = "El quaternion de orientacion es: " + orient.ToString();
        object[] content = new object[] { orient };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(RotateEvent, content, raiseEventOptions, SendOptions.SendReliable);
    }
}
