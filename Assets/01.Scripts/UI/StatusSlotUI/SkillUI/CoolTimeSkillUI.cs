using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolTimeSkillUI : SkillUI
{

    public override void OnSkillUsed()
    {
        base.OnSkillUsed();

        StartCoroutine(CooltimeCoroutine());
    }

    private IEnumerator CooltimeCoroutine()
    {
        float duration = value; // 쿨타임 지속 시간
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentFillAmount = 1f - (elapsedTime / duration); // 남은 시간 비율 계산
            bliendGauge.fillAmount = currentFillAmount; // 게이지 값 업데이트

            yield return null;
        }

        // 쿨타임이 끝나면 게이지를 0으로 설정
        bliendGauge.fillAmount = 0f;
        currentFillAmount = 0f;

        OnSkillReadyEvent?.Invoke();
    }
}
