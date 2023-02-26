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
        text.text = text.text + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            //PhotonNetwork.LoadLevel("MainScene");
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
        //message = message.Substring(1, message.Length - 2);
        text.text = "El mensaje despues substring 1 es: " + orient.ToString();
        //string[] splitted = message.Split(',');
        //Vector3 orient = new Vector3(float.Parse(splitted[0]), float.Parse(splitted[1]), float.Parse(splitted[2]));
        object[] content = new object[] { orient };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(RotateEvent, content, raiseEventOptions, SendOptions.SendReliable);

        //PhotonView target = null;
        //Debug.Log("El playerId es: " + playerId);
        //
        ////target = PhotonNetwork.GetPhotonView(playerId);
        //foreach (var view in PhotonNetwork.PhotonViewCollection)
        //{
        //    Debug.Log("PhotonViewID es: " + view.ViewID);
        //    if (view.ViewID == playerId)
        //    {
        //        target = view;
        //        break;
        //    }
        //}
        //if (target != null)
        //{
        //    text.text = "El target no es null";
        //    target.RPC("ReceiveMessage", RpcTarget.AllBuffered, message);
        //}
    }

    //[PunRPC]
    //public void ReceiveMessage(string message)
    //{
    //    text.text = "El mensaje en receive message es: " + message;
    //    message = message.Substring(1, message.Length - 2);
    //    text.text = "El mensaje despues substring 1 es: " + message;
    //    string[] splitted = message.Split(',');
    //    Vector3 orient = new Vector3(float.Parse(splitted[0]), float.Parse(splitted[1]), float.Parse(splitted[2]));
    //    transform.rotation = Quaternion.LookRotation(orient);
    //}

}
