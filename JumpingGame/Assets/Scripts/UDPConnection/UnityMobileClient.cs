using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UnityMobileClient : MonoBehaviour
{
    private UDPClient udpClient;
    private string ipAddress;
    private int port;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button connectButton;
    [SerializeField] private TMP_InputField ipInputField;
    [SerializeField] private TMP_InputField portInputField;

    void Start()
    {
        if (connectButton != null)
        {
            connectButton.onClick.AddListener(delegate { StartConnexion(); });
        }

        // PhotonNetwork.ConnectUsingSettings();
        if (ipInputField != null)
        {
            ipInputField.onValueChanged.AddListener(delegate { ValueChangeCheckIP(); });
        }

        if (portInputField != null)
        {
            portInputField.onValueChanged.AddListener(delegate { ValueChangeCheckPORT(); });
        }
    }

    void Update()
    {
        if (udpClient != null)
        {
            //Obtener la orientación del dispositivo
            Vector3 deviceAcceleration = Input.acceleration;

            // Aplicar la orientación al objeto
            Quaternion orientation = Quaternion.FromToRotation(Vector3.up, deviceAcceleration);
            Debug.Log("La orientacion es: " + orientation);
            udpClient.SendMessageToServer(orientation.eulerAngles.x.ToString());
        }
    }

    public void ValueChangeCheckIP()
    {
        ipAddress = ipInputField.text;
    }

    public void ValueChangeCheckPORT()
    {
        try
        {
            port = int.Parse(portInputField.text);
        }
        catch
        {
            text.text = "Introduce un numero entero en el campo puerto";
        }
    }

    public void StartConnexion()
    {
        if (ipAddress != null || port != 0)
        {
            udpClient = new UDPClient();
            udpClient.StartUdpClient(ipAddress, port);
        }
    }
}
