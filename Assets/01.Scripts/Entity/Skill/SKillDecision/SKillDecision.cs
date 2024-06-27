using SkillSystem;
using System;
using UnityEngine;

/// <summary>
/// 중요 : 스킬 디시젼 오브젝트는 무조건 스킬 트렌지션 자식으로 두세요!!
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
        _skillActionData = transform.parent.GetComponent<SkillActionData>(); //스킬 트렌지션에서 갖고옴
    }

    public abstract bool MakeDecision();

    public virtual void OnUsed() { }   //스킬 사용한 후 실행됨

    public virtual void LevelUp() { } //레벨업하면 어케해줄 것인가

}

