using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : TargetObject
{
    public override Vector3 GetClosetPostion(Transform targetTrm)
    {
        //내 위치와 Vector3.zero에서 가장 가까운 포인트의 거리를 비교 => 내 크기의 반지름(원형모형 기준)
        float radius = Vector3.Distance(transform.position, _collider.ClosestPoint(Vector3.zero * 10));
        Vector3 dir = (targetTrm.position - transform.position).normalized;
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
