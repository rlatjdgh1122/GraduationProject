using System;
using UnityEngine;

public abstract class SKillDecision : MonoBehaviour
{
    protected EntityActionData _actionData;

    public Action OnSkillUsedEvent = null;
    public Action OnSkillReadyEvent = null;

    public virtual void SetUp(Transform parentRoot)
    {
        _actionData = parentRoot.GetComponent<EntityActionData>();
    }


    public abstract bool MakeDecision();

    public abstract void OnUsed(); //��ų ����� �� �����

    public abstract void LevelUp(); //�������ϸ� �������� ���ΰ�

}

