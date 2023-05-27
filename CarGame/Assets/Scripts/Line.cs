
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer line;
    private Vector3[] positions;

    void Start()
    {
        SetLineOnTheFloor();
    }

    private void SetLineOnTheFloor()
    {
        for (int i = 0; i < line.positionCount; i++)
        {
            Vector3 newPos = line.GetPosition(i);
            newPos.y = -0.5f;
            line.SetPosition(i, newPos);
        }
    }
}
