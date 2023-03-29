using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationSaveManager 
{
    string path = Path.Combine(Application.dataPath, "configuracion.json");
    //si estamos en el editor de unity lo guardara en la carpeta del proyecto
    //si estamos en Windows lo guardara en la misma ubicacion que el ejecutable

    public void Safe(ConfigurationData config)
    {
        string json = JsonUtility.ToJson(config);
        //si el archivo ya existe
        if (File.Exists(path))
        {
            //lo borramos
            File.Delete(path);
        }
        //volvemos a crear el archivo con la informacion de json
        File.WriteAllText(path, json);
    }

    public ConfigurationData Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ConfigurationData data = JsonUtility.FromJson<ConfigurationData>(json);
            return data;
        }
        else
        {
            Debug.Log("No se pudo cargar el archivo: No se encontró o no existe");
            return null;
        }
    }
}
