using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ServerConection : MonoBehaviour
{
    TcpListener servidor;
    TcpClient cliente;

    void Start()
    {
        int puerto = 8888;
        IPAddress direccion = IPAddress.Parse("127.0.0.1");
        servidor = new TcpListener(direccion, puerto);
        servidor.Start();

        Debug.Log("address del servidor es " + direccion);
        Debug.Log("Servidor escuchando en el puerto " + puerto);
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

            Debug.Log("Mensaje recibido: " + data);
            string[] magnitudesInfo = data.Split("/");
            string[] orient = magnitudesInfo[0].Split("-");
            string[] accel = magnitudesInfo[1].Split("-");
            string[] gyros = magnitudesInfo[2].Split("-");

            Debug.Log("MagnitudesInfo es " + magnitudesInfo);
            Debug.Log("orient es " + orient);
            Debug.Log("accel es " + accel);
            Debug.Log("gyros es " + gyros);
        }
    }

    private void OnApplicationQuit()
    {
        if(cliente!= null)
        {
            cliente.Close();
        }
        if (servidor != null)
        {
            servidor.Stop();
        }
    }
}
