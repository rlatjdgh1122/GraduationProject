using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolTimeSkillUI : SkillUI
{
    public float cooltimeDuration; // 쿨타임 지속 시간
    private float elapsedTime;
    private bool isCoolingDown;

    void Start()
    {
        elapsedTime = 0f;
        isCoolingDown = false;
    }

    void Update()
    {
        if (isCoolingDown)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < cooltimeDuration)
            {
                currentFillAmount = 1f - (elapsedTime / cooltimeDuration); // 남은 시간 비율 계산
                bliendGauge.fillAmount = currentFillAmount; // 게이지 값 업데이트
            }
            else
            {
                // 쿨타임이 끝나면 게이지를 0으로 설정하고 쿨타임 종료
                bliendGauge.fillAmount = 0f;
                currentFillAmount = 0f;
                isCoolingDown = false;

                OnSkillReadyEvent?.Invoke();
            }
        }
    }

    public override void OnSkillUsed()
    {
        base.OnSkillUsed();

        StartCooldown();
    }

    private void StartCooldown()
    {
        elapsedTime = 0f;
        isCoolingDown = true;
    }
}
