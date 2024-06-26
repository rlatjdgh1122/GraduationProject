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
            Debug.LogError("스킬 트렌지션을 넣주세요");

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
    /// 스킬 사용 가능 상태가 되면 알아서 스킬을 써줌
    /// </summary>
    private void OnUsed()
    {
        _skillTranstion.OnUsed();
    }
}
