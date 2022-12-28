using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Table prefab;
    [SerializeField] private Vector3 bridgeDir = Vector3.zero;

    private List<Table> tables;
    public float fallVel;
    public int numTables;

    void Start()
    {
        tables = new List<Table>();
        InstantiateTables();
    }

    private void InstantiateTables()
    {
        float ancho = prefab.gameObject.transform.localScale.z + 0.2f;
        float aux = 0;

        for (int i = 0; i < numTables; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + aux * bridgeDir.x, transform.position.y, transform.position.z + aux * bridgeDir.z);
            Table t = Instantiate(prefab, pos, transform.rotation, transform);
            t.parent = this;
            tables.Add(t);
            t.index = i;

            if(i == 0 || i == numTables - 1) t.ContraintAll();

            aux += ancho;
        }
    }

    public void FallTables()
    {
        float time = 0.8f;
        for (int i = 1; i < tables.Count - 1; i++)
        {
            tables[i].FallTable(time);
            time += 0.2f;
        }
    }
}
