using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public class MaxSkillUsedCountDecision : UnvariableSkillDecision
    {
        public int MaxSkillUsedCount = 5;

        public override bool MakeDecision()
        {
            return _skillActionData.SkillUsedCount <= MaxSkillUsedCount;
        }
    }

}

