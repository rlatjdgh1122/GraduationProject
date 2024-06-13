using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ComingObjetMovement : MonoBehaviour
{
    [SerializeField]
    protected float _moveDuration = 5f;

    [SerializeField]
    protected LayerMask _groundLayer;

    protected MeshCollider _meshCollider;

    protected Vector3 _targetPos;
    protected Vector3 _centerPos;

    protected Vector3 RaycastHit_ToCenterPos
    {
        get
        {
            Vector3 closestPointToCenter = GetClosestPointToCenter();

            if (Physics.Raycast(closestPointToCenter, (_centerPos - closestPointToCenter).normalized, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                return hit.point;
            }
            return Vector3.zero;
        }
    }

    protected Transform RaycastHitTrm
    {
        get
        {
            Vector3 closestPointToCenter = GetClosestPointToCenter();

            if (Physics.Raycast(closestPointToCenter, (_centerPos - closestPointToCenter).normalized, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                return hit.transform;
            }
            return null;
        }
    }

    protected virtual void Awake()
    {
        _meshCollider = transform.GetChild(0).GetComponent<MeshCollider>();
    }

    public virtual void Move()
    {
        transform.DOMove(_targetPos, _moveDuration).
            OnComplete(() =>
            {
                Arrived();
                //DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, targetColor, 0.7f).OnComplete(() =>
                //{
                //    WaveManager.Instance.OnIceArrivedEventHanlder();

                //    Debug.Log(_enemies.Length);
                //    foreach (Enemy enemy in _enemies)
                //    {
                //        enemy.IsMove = true;
                //        enemy.NavAgent.enabled = true;
                //    }
                //    SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandle;
                //});
            });
    }

    //private void SetOutline()
    //{
    //    DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, endColor, 0.7f).OnComplete(() =>
    //    {
    //        _outline.enabled = false;
    //        SignalHub.OnBattlePhaseEndEvent -= GroundMove;
    //    });
    //}

    public void SetMoveTarget(Transform trm)
    {
        transform.SetParent(trm);
        _centerPos = trm.position;
    }

    protected virtual Vector3 GetClosestPointToCenter() // �ݵ�� _meshCollider�� convex�� �������
    {
        Vector3 closestPoint = _meshCollider.ClosestPoint(_centerPos);
        return closestPoint;
    }

    public void SetComingObejctPos(Transform parentTransform, Vector3 position)
    {
        transform.SetParent(parentTransform); // �θ� ����
        transform.localPosition = position; // ������ ��ġ ����

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

        // X ��ǥ�� ���� Ÿ�� ���� ���� (�������, ��������, 0����)
        targetVec.x += Mathf.Sign(transform.position.x) * xDistance;
        
        // Z ��ǥ�� ���� Ÿ�� ���� ���� (�������, ��������, 0����)
        targetVec.z += Mathf.Sign(transform.position.z) * zDistance;

        // Ÿ�� ��ġ ����
        _targetPos = targetVec;
    }
    protected abstract void Arrived();
}
