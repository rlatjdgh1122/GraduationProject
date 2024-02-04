using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSysytem : MonoBehaviour
{
    public void CreateBuildingSilhouette()
    {
        BaseBuilding building = PoolManager.Instance.Push();
    }
}
