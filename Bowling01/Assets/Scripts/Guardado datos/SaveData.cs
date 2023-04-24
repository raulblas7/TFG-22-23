using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData
{
    private string path = Path.Combine(Application.dataPath, "data.txt");
    private StreamWriter writer = null;

    public void InitSave()
    {
        //por el momento solo tendremos un archivo
        if (File.Exists(path))
        {
            //lo borramos
            File.Delete(path);
        }
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
        if(writer != null)
        {
            writer.Close();
        }
    }
}
