using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComingObject
{
    public void SetComingObjectInfo(Transform parentTransform, Vector3 position, ComingElements groundElements);
    public void SetEnemies(Enemy[] enemies);
    public void SetMoveTarget(Transform trm);
}
