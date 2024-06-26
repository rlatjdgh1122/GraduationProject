using SkillSystem;
using System;
using UnityEngine;

/// <summary>
/// �߿� : ��ų ����� ������Ʈ�� ������ ��ų Ʈ������ �ڽ����� �μ���!!
/// </summary>

public abstract class SKillDecision : MonoBehaviour, ISkillDecision
{
    protected EntityActionData _entityActionData = null;
    protected SkillActionData _skillActionData = null;

    public Action OnSkillUsedEvent = null;
    public Action OnSkillReadyEvent = null;

    public virtual void SetUp(Transform parentRoot)
    {
        _entityActionData = parentRoot.GetComponent<EntityActionData>();
        _skillActionData = transform.parent.GetComponent<SkillActionData>(); //��ų Ʈ�����ǿ��� �����
    }

    public abstract bool MakeDecision();

    public virtual void OnUsed() { }   //��ų ����� �� �����

    public virtual void LevelUp() { } //�������ϸ� �������� ���ΰ�

}

