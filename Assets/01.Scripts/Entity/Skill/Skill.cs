using System;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected Entity _owner;

    public SkillTransition SkillTransition { get; private set; } = null;

    #region events
    public Action OnSkillStart = null;
    public Action OnSkillCompleted = null;
    #endregion

    public bool IsAvaliable = true;
    public bool CanUseSkill = false;

    public virtual void SetOwner(Entity owner)
    {
        _owner = owner;
        SkillTransition = owner.transform.Find("SkillTransition").GetComponent<SkillTransition>();
        SkillTransition.SetUp(owner.transform);
    }

    public virtual void PlaySkill()
    {

    }
}
