using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Instancia del launcher
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] private OrientateByMobileInput orientateByMobile;

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

    public void SendMessageToPlayer(int playerId, string message)
    {
        PhotonView target = null;
        Debug.Log("El playerId es: " + playerId);
        Debug.Log("PhotonViewCollection es: " + PhotonNetwork.PhotonViewCollection);

        target = PhotonNetwork.GetPhotonView(playerId);
        //foreach (var view in PhotonNetwork.PhotonViewCollection)
        //{
        //    Debug.Log("PhotonViewID es: " + view.ViewID);
        //    if (view.ViewID == playerId)
        //    {
        //        target = view;
        //        break;
        //    }
        //}
        if (target != null)
        {
            Debug.Log("El target no es null");
            target.RPC("ReceiveMessage", RpcTarget.AllBuffered, message);
        }
    }

    [PunRPC]
    public void ReceiveMessage(string message)
    {
        Debug.Log("El mensaje es: " + message);
        message = message.Substring(1, message.Length - 2);
        Debug.Log("El mensaje despues substring 1 es: " + message);
        string[] splitted = message.Split(',');
        orientateByMobile.UpdateOrientation(new Vector3(float.Parse(splitted[0]), float.Parse(splitted[1]), float.Parse(splitted[2])));
    }
}
