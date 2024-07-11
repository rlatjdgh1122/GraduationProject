using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParabolicProjectile
{
    void ExecuteAttack(Vector3 startPosition, Vector3 targetPosition, float maxTime, bool isPool);
}
