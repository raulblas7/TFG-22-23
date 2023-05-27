
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class ClientConection : MonoBehaviour
{
    [SerializeField] private string host;
    [SerializeField] private int port;

    private TcpClient client;
    private NetworkStream stream;


    void Start()
    {
        client = new TcpClient(host, port);
        stream = client.GetStream();
    }

    void Update()
    {
        // Crear un búfer para almacenar los datos recibidos
        byte[] buffer = new byte[1024];

        // Leer los datos enviados desde el servidor
        int bytes = stream.Read(buffer, 0, buffer.Length);

        if(bytes > 0)
        {
            // Convertir los datos a una cadena de texto
            string data = Encoding.ASCII.GetString(buffer, 0, bytes); // 123123123
            Debug.Log("Data es " + data);
            
        }
        
    }
}
