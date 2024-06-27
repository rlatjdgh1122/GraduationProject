using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AutoSkillCast : MonoBehaviour
{
    public SkillController SkillController = null;
    [SerializeField] private UnityEvent OnSkillUsedEvent = null;

    private void Start()
    {
        if (!SkillController)
        {
            Debug.LogError("��ų ��Ʈ�ѷ��� ���ּ���");

            return;
        }
    }
    private void Update()
    {
        //if (!_skillTranstion.IsReady) return;

        if (SkillController.CheckDecision())
        {
            OnSkillUsedEvent?.Invoke();
            OnUsed();
        }
    }

    /// <summary>
    /// ��ų ��� ���� ���°� �Ǹ� �˾Ƽ� ��ų�� ����
    /// </summary>
    private void OnUsed()
    {
        SkillController.OnUsed();
    }
}
