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
            Debug.LogError("스킬 컨트롤러를 넣주세요");

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
    /// 스킬 사용 가능 상태가 되면 알아서 스킬을 써줌
    /// </summary>
    private void OnUsed()
    {
        SkillController.OnUsed();
    }
}
