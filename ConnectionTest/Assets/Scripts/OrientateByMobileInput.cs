using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientateByMobileInput : MonoBehaviour
{
    public void UpdateOrientation(Vector3 orient)
    {
        transform.rotation = Quaternion.LookRotation(orient);
    }
}
