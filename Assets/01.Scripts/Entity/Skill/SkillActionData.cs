using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActionData : MonoBehaviour
{
    [HideInInspector] public int SkillUsedCount; //��ų�� ��� ����Ǿ���

    public int AddSkillUsedCount(int value = 1)
    {
        SkillUsedCount += value;

        return SkillUsedCount;
    }
}
