using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class ClientConection : MonoBehaviour
{
    [SerializeField] private string host;
    [SerializeField] private int port;

    private TcpClient client;
    private NetworkStream stream;

    private Vector3 orientation;
    private Vector3 accelerometer;
    private Vector3 gyroscope;

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

            string orient = data.Substring(0, 2);
            string accele = data.Substring(3, 5);
            string gyrosc = data.Substring(6, 8);

            //orientation = new Vector3(orient)
        }
        
    }
}
