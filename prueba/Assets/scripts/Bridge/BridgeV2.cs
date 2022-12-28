using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeV2 : MonoBehaviour
{
    [SerializeField] private GameObject bridgeTable;
    [SerializeField] private GameObject cylinderConector;

    [SerializeField] private float numTables;

    void Start()
    {
        float width = bridgeTable.transform.localScale.z + 0.2f;
        float aux = 0;

        for (int i = 0; i < numTables; i++)
        {
            //Vector3 pos = new Vector3(transform.position.x + aux * bridgeDir.x, transform.position.y, transform.position.z + aux * bridgeDir.z);
            //Table t = Instantiate(bridgeTable, pos, transform.rotation, transform);
            //t.parent = this;
            //tables.Add(t);
            //t.index = i;

            //if (i == 0 || i == numTables - 1) t.ContraintAll();

            aux += width;
        }
    }

    void Update()
    {
        
    }
}
