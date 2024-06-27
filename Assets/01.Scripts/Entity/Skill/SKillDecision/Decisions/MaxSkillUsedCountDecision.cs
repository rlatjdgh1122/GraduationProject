using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSkillUsedCountDecision : SKillDecision
{
    public int MaxSkillUsedCount = 5;

    public override bool MakeDecision()
    {
        return _skillActionData.SkillUsedCount <= MaxSkillUsedCount;
    }
}
