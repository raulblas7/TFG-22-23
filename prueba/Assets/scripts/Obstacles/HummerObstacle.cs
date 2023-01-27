using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerObstacle : MonoBehaviour
{
    [SerializeField] Rigidbody[] hummersRbs;
    [SerializeField] float force;
    [SerializeField] Vector3 dir;

    private void FixedUpdate()
    {
        for (int i = 0; i < hummersRbs.Length; i++)
        {
            hummersRbs[i].angularVelocity = dir * force;
        }
    }
}
