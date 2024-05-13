using DG.Tweening;
using UnityEngine;

public class KnockbackFeedback : CombatFeedback
{
    private int _groundLayer = 0;
    private float _knockbackSpeed = 0.2f;
    private float _fallSpeed = 1.2f;

    public override void Awake()
    {
        base.Awake();
        _groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }
    public override bool StartFeedback()
    {
        Vector3 currentPosition = ownerTrm.position;

        //�� ��ġ���� ���� ��ġ�� �˹�
        Vector3 knockbackPosition = currentPosition - new Vector3(actionData.HitNormal.x, 0f, actionData.HitNormal.z) * Value;

        ownerTrm.DOMove(knockbackPosition, _knockbackSpeed);

        //�ٴ��� ���� �ƴ϶��
        if (!IsPositionValid(knockbackPosition))
        {
            ownerTrm.DOMoveY(ownerTrm.position.y - 3f, _fallSpeed);

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
        return Physics.Raycast(position, new Vector3(0, -1, 0), 5f, _groundLayer);
    }
}
