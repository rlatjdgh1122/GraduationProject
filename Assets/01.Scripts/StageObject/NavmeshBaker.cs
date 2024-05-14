using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavmeshBaker : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;

    private void Awake()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        NavmeshBake();
        SignalHub.OnIceArrivedEvent += NavmeshBake;
    }

    public void NavmeshBake()
    {
        _navMeshSurface.BuildNavMesh();
    }

    private void OnDisable()
    {
        SignalHub.OnIceArrivedEvent -= NavmeshBake;
    }
}
