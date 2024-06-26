using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AutoSkillCast : MonoBehaviour
{
    public SkillTransition _skillTranstion = null;
    [SerializeField] private UnityEvent OnSkillUsedEvent = null;

    private void Start()
    {
        if (!_skillTranstion)
        {
            Debug.LogError("��ų Ʈ�������� ���ּ���");

            return;
        }
    }
    private void Update()
    {
        if (_skillTranstion.CheckDecision())
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
        _skillTranstion.OnUsed();
    }
}
