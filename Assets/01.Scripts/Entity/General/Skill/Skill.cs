using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected General _owner;

    #region events
    public Action OnSkillStart = null;
    public Action OnSkillCompleted = null;
    #endregion

    public bool IsAvaliable = true;
    public bool CanUseSkill = false;

    public virtual void SetOwner(General owner)
    {
        _owner = owner;
    }

    public virtual void PlaySkill()
    {

    }
}
