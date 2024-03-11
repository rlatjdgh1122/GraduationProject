using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected Entity _owner;

    #region events
    public Action OnSkillCompleted;
    public Action OnSkillFailed;
    #endregion

    public bool IsAvaliable = true;

    public virtual void SetOwner(Entity owner)
    {
        _owner = owner;
    }
}
