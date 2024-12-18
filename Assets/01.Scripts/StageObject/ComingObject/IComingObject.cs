using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComingObject
{
    public void SetComingObjectInfo(ComingElements groundElements, Transform parentTransform, Vector3 position);
    public void SetEnemies(List<Enemy> enemies);
    public void SetMoveTarget(Transform trm);
}
