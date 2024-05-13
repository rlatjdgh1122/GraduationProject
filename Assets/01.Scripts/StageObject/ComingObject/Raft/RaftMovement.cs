using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaftMovement : ComingObjetMovement
{
    private Raft _raft;

    protected override void Awake()
    {
        base.Awake();

        _raft = GetComponent<Raft>(); // �� ���� �ð� ����
    }

    public override void Move()
    {
        // ��� �� �ִ� ���ϱ�
        CoroutineUtil.CallWaitForSeconds(5f, null, () => base.Move());
    }

    protected override void Arrived()
    {
        Debug.Log("����ɱ�");
        _raft.Arrived();
    }

    public override void SetComingObejctPos(Transform parentTransform, Vector3 position)
    {
        // �߾Ӱ� ��Ʈ ����Ʈ ������ �Ÿ� ���
        float centerToHitPointX = Mathf.Abs(_meshCollider.transform.position.x - RaycastHit_ToCenterPos.x);
        float centerToHitPointZ = Mathf.Abs(_meshCollider.transform.position.z - RaycastHit_ToCenterPos.z);

        Vector3 closestPointToCenter = GetClosestPointToCenter();

        // ���� ����� ����Ʈ�� ��Ʈ ����Ʈ ������ �Ÿ� ���
        float closestPointToHitPointX = Mathf.Abs(closestPointToCenter.x - RaycastHit_ToCenterPos.x);
        float closestPointToHitPointZ = Mathf.Abs(closestPointToCenter.z - RaycastHit_ToCenterPos.z);

        // X�� Z �Ÿ� ���
        float xDistance = Mathf.Abs(centerToHitPointX - closestPointToHitPointX);
        float zDistance = Mathf.Abs(centerToHitPointZ - closestPointToHitPointZ);

        // Ÿ�� ���� ���
        Vector3 targetVec = new Vector3(RaycastHit_ToCenterPos.x, 0f, RaycastHit_ToCenterPos.z);

        //// X ��ǥ�� ���� Ÿ�� ���� ���� (�������, ��������, 0����)
        targetVec.x += Mathf.Sign(transform.position.x) * xDistance;
        //
        //// Z ��ǥ�� ���� Ÿ�� ���� ���� (�������, ��������, 0����)
        targetVec.z += Mathf.Sign(transform.position.z) * zDistance;

        // Ÿ�� ��ġ ����
        _targetPos = targetVec;
    }

    protected override Vector3 GetClosestPointToCenter()
    {
        Vector3 closestPoint = _meshCollider.ClosestPoint(_centerPos);
        closestPoint.y = _centerPos.y;
        return closestPoint;
    }
}
