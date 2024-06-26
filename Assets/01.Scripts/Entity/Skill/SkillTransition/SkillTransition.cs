using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTransition : MonoBehaviour
{
    [SerializeField] private List<SKillDecision> _decisions = new();

    //�ڵ� ȣ����� ���� 
    [HideInInspector] public bool IsReady = false;

    // ��ų ��� �� �߻��ϴ� �̺�Ʈ
    public Action OnSkillUsedEvent = null;

    // ��ų ��� ���� �� �߻��ϴ� �̺�Ʈ
    public Action OnSkillReadyEvent = null;


    private void Awake()
    {
        GetComponents(_decisions);
    }

    public void SetUp(Transform parentRoot)
    {
        foreach (SKillDecision decision in _decisions)
        {
            decision.SetUp(parentRoot);

        }//end foreach

        IsReady = true;
    }

    public bool CheckDecision()
    {
        bool result = false;

        foreach (SKillDecision decision in _decisions)
        {
            result = decision.MakeDecision();

            if (result == false) break;

        }//end foreach

        return result;
    }

    public void OnUsed()
    {
        foreach (SKillDecision decision in _decisions)
        {
            decision.OnUsed();

        }//end foreach
    }

    public void LevelUp()
    {
        foreach (SKillDecision decision in _decisions)
        {
            decision.LevelUp();

        }//end foreach
    }
}
