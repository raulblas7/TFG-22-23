using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerObstacle : MonoBehaviour
{
    [SerializeField] Rigidbody[] hummersRbs;
    [SerializeField] int force;
    [SerializeField] Vector3 dir;

    private void Start()
    {
        force = GameManager.Instance.GetDifficulty() + 1;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < hummersRbs.Length; i++)
        {
            hummersRbs[i].angularVelocity = dir * force;
        }
    }
}
