using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ServerConection : MonoBehaviour
{
    //Instancia del Server
    private static ServerConection _instance;

    private TcpListener servidor;
    private TcpClient cliente;
    UdpClient udpServer;

    private Vector3 orientation;
    private Vector3 accelerometer;
    private Vector3 gyroscope;

    public static ServerConection Instance { get { return _instance; } }

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

    void Start()
    {
        int puerto = 8888;
        udpServer = new UdpClient(puerto); // puerto es el número de puerto que se va a utilizar para la conexión
        IPAddress direccion = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP = new IPEndPoint(direccion, puerto);
        //servidor = new TcpListener(direccion, puerto);
        //servidor.Start();

        UnityEngine.Debug.Log("address del servidor es " + direccion);
        UnityEngine.Debug.Log("Servidor escuchando en el puerto " + puerto);

        //StartPythonClient();
    }

    void Update()
    {
        if (servidor.Pending())
        {
            cliente = servidor.AcceptTcpClient();
        }
        if (cliente != null && cliente.Connected)
        {
            NetworkStream stream = cliente.GetStream();

            byte[] datos = new byte[1024];
            int bytesRecibidos = stream.Read(datos, 0, datos.Length);
            string data = Encoding.ASCII.GetString(datos, 0, bytesRecibidos);

            string[] magnitudesInfo = data.Split("/");
            string[] orient = magnitudesInfo[0].Split("_");
            string[] accel = magnitudesInfo[1].Split("_");
            string[] gyros = magnitudesInfo[2].Split("_");

            orientation = new Vector3(float.Parse(orient[0]), float.Parse(orient[1]), float.Parse(orient[2]));
            accelerometer = new Vector3(float.Parse(accel[0]), float.Parse(accel[1]), float.Parse(accel[2]));

            //UnityEngine.Debug.Log(orientation);
        }
    }

    private void StartPythonClient()
    {
        ProcessStartInfo psi = new ProcessStartInfo();
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.WindowStyle = ProcessWindowStyle.Hidden;
        psi.FileName = "C:\\Universidad\\TFG-22-23\\PythonPlugin\\dist\\Nicla-Sense-Plugin.exe";
        Process.Start(psi);
    }

    private void SendMessage()
    {
        if (cliente == null)
        {
            return;
        }

        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = cliente.GetStream();
            if (stream.CanWrite)
            {
                string serverMessage = "Disconnect";
                // Convert string message to byte array.                 
                byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage);
                // Write byte array to socketConnection stream.               
                stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                UnityEngine.Debug.Log("Server sent his message - should be received by client");
            }
        }
        catch (SocketException socketException)
        {
            UnityEngine.Debug.Log("Socket exception: " + socketException);
        }
    }

    public Vector3 GetOrientation()
    {
        return orientation;
    }

    public Vector3 GetAccelerometer()
    {
        return accelerometer;
    }

    private void OnApplicationQuit()
    {
        DisconnectServer();
    }

    public void DisconnectServer()
    {
        if (cliente != null)
        {
            SendMessage();
        }
        if (servidor != null)
        {
            servidor.Stop();
        }
    }
}
