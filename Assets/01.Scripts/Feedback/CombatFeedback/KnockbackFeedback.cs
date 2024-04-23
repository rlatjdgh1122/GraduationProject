using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackFeedback : CombatFeedback
{
    private int groundLayer = 0;

    public override float Value { get; set; }

    private void Awake()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }
    public override bool StartFeedback()
    {
        Vector3 currentPosition = ownerTrm.position;
        //�� ��ġ���� ���� ��ġ�� �˹�
        Vector3 knockbackPosition = currentPosition - new Vector3(actionData.HitNormal.x, 0f, actionData.HitNormal.z) * Value;

        ownerTrm.DOMove(knockbackPosition, 0.5f);

        //�ٴ��� ���� �ƴ϶��
        if (!IsPositionValid(knockbackPosition))
        {
            ownerTrm.DOMoveY(ownerTrm.position.y - 2f, 1.2f);

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
