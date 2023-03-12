using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Lets make our calls from the Plugin
    [DllImport("PS4Controller")]
    private static extern int InitPlugin();
    [DllImport("PS4Controller")]
    private static extern int getAccelerometerV1(ref float x, ref float y, ref float z);

    [DllImport("PS4Controller")]
    private static extern int getAccelerometerV2();

    [DllImport("PS4Controller")]
    private static extern void getAccelerometerV4(ref float x, ref float y, ref float z);

    [DllImport("PS4Controller")]
    private static extern void closePlugin();

    private float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        if(InitPlugin() == 0)
        {
            Debug.Log("inicializado SDL");
        }
        else Debug.Log("Error inicio SDL");
    }

    // Update is called once per frame
    void Update()
    {
        //getAccelerometerV1(ref x, ref y, ref z);
        //Debug.Log("X: " + x + "Y: " + y + "Z: " + z);

        //Debug.Log(getAccelerometerV2());

        getAccelerometerV4(ref x, ref y, ref z);
        Debug.Log("X: " + x + "Y: " + y + "Z: " + z);
    }
}
