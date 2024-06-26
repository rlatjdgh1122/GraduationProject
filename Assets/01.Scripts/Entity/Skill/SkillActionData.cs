using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActionData : MonoBehaviour
{
    [HideInInspector] public int SkillUsedCount; //스킬이 몇번 실행되었나

    public int AddSkillUsedCount(int value = 1)
    {
        SkillUsedCount += value;

        return SkillUsedCount;
    }
}
