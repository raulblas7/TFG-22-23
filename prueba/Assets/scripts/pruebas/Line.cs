using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer line;

    private Vector3[] positions;
    // Start is called before the first frame update
    void Start()
    {
        //positions = new Vector3[line.positionCount];

        //int aux =line.GetPositions(positions);
        //if(aux != line.positionCount)
        //{
        //    Debug.Log("no se han cogido todos los vertices");
        //}
        for(int i =0; i< line.positionCount; i++)
        {
            Vector3 newPos = line.GetPosition(i);
            newPos.y = -0.5f;
            line.SetPosition(i, newPos);
        }
    }

    void Update()
    {
        //line.positionCount++;
        //line.SetPosition(0, new Vector3(5f, 0, 0));
        //Vector3 pos0 = line.GetPosition(0);
    }
}
