using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackFeedback : CombatFeedback
{
    private readonly int groundLayer = LayerMask.GetMask("Ground");

    public override float Value { get; set; }
    public override bool StartFeedback()
    {
        Vector3 currentPosition = transform.position;
        //내 위치에서 맞은 위치로 넉백
        Vector3 knockbackPosition = currentPosition - new Vector3(actionData.HitNormal.x, 0f, actionData.HitNormal.z) * Value;

        transform.DOMove(knockbackPosition, 0.5f);

        //바닥이 땅이 아니라면
        if (!IsPositionValid(knockbackPosition))
        {
            transform.DOMoveY(transform.position.y - 2f, 1.2f);

            return false;
        }
        return true;
    }

    public override bool FinishFeedback()
    {
        return true;
    }

    private bool IsPositionValid(Vector3 position)
    {
        return Physics.Raycast(position, new Vector3(0, -1, 0), 5f, groundLayer);
    }
}
