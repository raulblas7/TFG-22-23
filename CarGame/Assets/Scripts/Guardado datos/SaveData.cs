
using UnityEngine;
using System.IO;
using System;

public class SaveData
{
    private string path;
    private StreamWriter writer = null;

    public void InitSave()
    {
        DateTime date = DateTime.Now;
        string format = "dd_MM_yyyy hh-mm-ss";
        string dateS = date.ToString(format);

        path = Path.Combine(Application.dataPath, "data_" + dateS + ".txt");
        writer = new StreamWriter(path, true); // crea un nuevo archivo
    }

    public void WriteData(string data)
    {
        if(writer != null)
        {
            writer.WriteLine(data);
        }
    }

    public void FinishSave()
    {
        if (writer != null) writer.Close();
    }
}
