using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public List<NavMeshSurface> movementSurfaces;

    

    void Update()
    {
        //for (int i = 0; i < movementSurfaces.Count; i++)
        //{
        //    movementSurfaces[i].BuildNavMesh();
        //}
    }

    public void AddNewNavMeshMovementSurface(NavMeshSurface navMeshSurface)
    {
        movementSurfaces.Add(navMeshSurface);
    }

    public void DeleteMovementNavSurface(NavMeshSurface navMeshSurface)
    {
        movementSurfaces.Remove(navMeshSurface);
    }

    public void BuildNavMeshSurface(NavMeshSurface navMeshSurface)
    {
        navMeshSurface.BuildNavMesh();
    }
}
