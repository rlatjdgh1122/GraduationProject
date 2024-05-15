using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavmeshManager : Singleton<NavmeshManager>
{
    private NavMeshSurface _navMeshSurface;

    public override void Awake()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();

        NavmeshBake();
    }

    public void NavmeshBake()
    {
        _navMeshSurface.BuildNavMesh();
    }
}
