using System;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected Entity _owner;

    #region events
    public Action OnSkillStart = null;
    public Action OnSkillCompleted = null;
    #endregion

    public bool IsAvaliable = true;
    public bool CanUseSkill = false;

    public virtual void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    public virtual void PlaySkill()
    {

    }
}
