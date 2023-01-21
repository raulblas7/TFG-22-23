using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerObstacle : MonoBehaviour
{
    [SerializeField] HingeJoint[] hummersJoints;
    [SerializeField] Vector3[] hummersAnchorValues;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for(int i = 0; i < hummersJoints.Length; i++)
            {
                hummersJoints[i].anchor = new Vector3(hummersAnchorValues[i][0], hummersAnchorValues[i][1], hummersAnchorValues[i][2]);
            }
        }
    }
}
