using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : TargetObject
{
    public override Vector3 GetClosetPostion(Vector3 targetPos)
    {
        float radius = Vector3.Distance(transform.position, ColliderCompo.ClosestPoint(Vector3.zero * 10));
        Vector3 dir = (targetPos - transform.position).normalized;
        Vector3 result = dir * radius + transform.position;
        result.y = transform.position.y;

        return result;
    }
    protected override void HandleDie()
    {
        
    }

    protected override void HandleHit()
    {
      
    }
}
