using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoConnection : MonoBehaviour
{
    private SerialPort serialPort;

    private Vector3 orientation;

    void Start()
    {

        foreach (string portname in SerialPort.GetPortNames())
        {
            serialPort = new SerialPort(portname, 115200);
            try
            {
                serialPort.Open();
                Debug.Log("Despues open " + portname);
                serialPort.Write("A");
                Debug.Log("Despues escribir");
                serialPort.ReadTimeout = 100;
                Debug.Log("Despues readTimeOut");
                string received = serialPort.ReadLine();
                Debug.Log("Despues readLine = " + received);
                if (received == "Device: NiclaSenseME")
                {
                    Debug.Log("device connected to: " + portname);
                    break;
                }
                else serialPort.Close();
            }
            catch (Exception)
            {
                Debug.Log("device NOT connected to: " + portname);
            }
        }
    }

    void Update()
    {
        //try
        //{
        //    if(serialPort.IsOpen)
        //    {
        //        string lineReceived = serialPort.ReadLine();
        //        Debug.Log("Linea leida es " + lineReceived);
        //        string[] orientationData = lineReceived.Split(",");
        //        Debug.Log("Despues de split mas parse: " + float.Parse(orientationData[0].Replace(".", ",")));
        //
        //        orientation = new Vector3(float.Parse(orientationData[0].Replace(".", ",")), float.Parse(orientationData[1]), float.Parse(orientationData[2]));
        //    }
        //}
        //catch (Exception e)
        //{
        //    Debug.Log("Se ha producido una excepción: " + e.Message);
        //}
    }

    private void OnApplicationQuit()
    {
        CloseSerialPort();
    }

    public void CloseSerialPort()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    public Vector3 GetOrientationFromBoard()
    {
        return orientation;
    }
}
