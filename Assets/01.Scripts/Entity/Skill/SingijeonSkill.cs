using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingijeonSkill : Skill
{
    public UnityEvent OnSingijeonSkillEvent;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
    }
}
