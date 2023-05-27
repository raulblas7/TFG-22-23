using System.Collections.Generic;
using UnityEngine;

public class LimitManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> positionsByDiff;

    void Start()
    {
        int diff = GameManager.Instance.GetDifficulty();
        transform.position = positionsByDiff[diff];
    }
}
