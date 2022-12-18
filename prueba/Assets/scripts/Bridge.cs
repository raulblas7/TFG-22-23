using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bridge : MonoBehaviour
{
    //[SerializeField]
    // private Table[] tables;
    [SerializeField]
    private Table prefab;

    private List<Table> tables;
    public float fallVel;
    public int numTables;

    public NavigationBaker navigationBaker;
    public NavMeshSurface navMeshSurface;

    void Start()
    {
        tables = new List<Table>();
        float ancho = prefab.gameObject.transform.localScale.z + 0.1f;
        float aux = 0;

        for (int i =0; i < numTables; i++)
        {
            Vector3 pos =  new Vector3( transform.position.x, transform.position.y, transform.position.z + aux );
            Table t = Instantiate(prefab, pos, Quaternion.identity, transform);
            t.SetNavigationBaker(navigationBaker);
            t.parent = this;
            tables.Add(t);
            t.index = i;
            aux += ancho;
        }
        navMeshSurface.BuildNavMesh();
    }

    void Update()
    {
        
    }
}
