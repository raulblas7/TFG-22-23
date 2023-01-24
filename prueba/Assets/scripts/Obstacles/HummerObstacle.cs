using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerObstacle : MonoBehaviour
{
    [SerializeField] Rigidbody[] hummersRbs;
    [SerializeField] float force;
    [SerializeField] Vector3 dir;
    private bool startSwing = false;

    private void FixedUpdate()
    {
        if(startSwing)
        {
            for(int i = 0; i < hummersRbs.Length; i++)
            {
                hummersRbs[i].angularVelocity = dir * force;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Swing();
        }
    }

    private void Swing()
    {
        startSwing= true;
    }
}
