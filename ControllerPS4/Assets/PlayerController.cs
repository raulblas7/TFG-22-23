using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Lets make our calls from the Plugin
    [DllImport("PS4Controller")]
    private static extern int getAccelerometerV1(ref float x, ref float y, ref float z);

    [DllImport("PS4Controller")]
    private static extern int getAccelerometerV2();

    [DllImport("PS4Controller")]
    private static extern void getAccelerometerV4(ref float x, ref float y, ref float z);

    [DllImport("PS4Controller")]
    private static extern void closePlugin();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
