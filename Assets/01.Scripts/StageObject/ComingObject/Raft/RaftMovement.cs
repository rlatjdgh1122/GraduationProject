using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaftMovement : ComingObjetMovement
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Move()
    {
        // ��� �� �ִ� ���ϱ�
        CoroutineUtil.CallWaitForSeconds(5f, null, () => base.Move());
    }

    protected override void Arrived()
    {
        Debug.Log("����ɱ�");
    }

    public override void SetComingObejctPos(Transform parentTransform, Vector3 position)
    {
        base.SetComingObejctPos(parentTransform, position);
    }
}
