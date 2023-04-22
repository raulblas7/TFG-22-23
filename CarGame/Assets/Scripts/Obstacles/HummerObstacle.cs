using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerObstacle : MonoBehaviour
{
    [SerializeField] Rigidbody[] hummersRbs;
    [SerializeField] float force;
    [SerializeField] Vector3 dir;

    private void Start()
    {
        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                force = 0.3f;
                break;
            case 1:
                force = 1f;
                break;
            case 2:
                force = 1.5f;
                break;
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < hummersRbs.Length; i++)
        {
            hummersRbs[i].angularVelocity = dir * force;
        }
    }
}
