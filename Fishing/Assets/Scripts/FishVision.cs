using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishVision : MonoBehaviour
{
    [Range(0f, 360f)]
    [SerializeField] private float visionAngle = 30f;
    [SerializeField] private float visionDistance = 10f;

    [SerializeField] Transform fishingRodTr;

    private bool sawFishingRod = false;

    void Update()
    {
        if(!sawFishingRod) FishVisionMethod();
    }

    void OnDrawGizmos()
    {
        DrawVision();
    }

    void DrawVision()
    {
#if UNITY_EDITOR
        var color = Color.green;
        color.a = 0.1f;
        UnityEditor.Handles.color = color;
        var halfFOV = visionAngle * 0.5f;
        var beginDirection = Quaternion.AngleAxis(-halfFOV, Vector3.up) * transform.right;
        UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, beginDirection, visionAngle, visionDistance);
#endif
    }

    private void FishVisionMethod()
    {
        Vector3 fishVector = fishingRodTr.position - transform.position;
        if (Vector3.Angle(fishVector, transform.right) < visionAngle / 2)
        {
            CheckDistance(fishVector);
        }
        //else if (Vector3.Angle(fishVector.normalized, fishingRodTr.forward) < visionAngle)
        //{
        //    CheckDistance(fishVector);
        //}
        //else if (Vector3.Angle(fishVector.normalized, fishingRodTr.up) < visionAngle)
        //{
        //    CheckDistance(fishVector);
        //}
    }

    private void CheckDistance(Vector3 fishVector)
    {
        if (fishVector.magnitude < visionDistance)
        {
            sawFishingRod = true;
            Debug.Log("Caña detectada");
        }
    }

    public bool IsSawFishingRod()
    {
        return sawFishingRod;
    }

    public Transform GetFishingRodTr()
    {
        return fishingRodTr;
    }
}
