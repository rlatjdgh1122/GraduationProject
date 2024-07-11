using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SingijeonArrow : Arrow, IParabolicProjectile
{
    public override void Setting(TargetObject owner, LayerMask layer)
    {
        base.Setting(owner, layer);
    }

    public void ExecuteAttack(Vector3 startPosition, Vector3 targetPosition, float maxTime, bool isPool)
    {
        StartCoroutine(Parabola.ParabolaMove(this, startPosition, targetPosition, maxTime, isPool, false));
    }
}
