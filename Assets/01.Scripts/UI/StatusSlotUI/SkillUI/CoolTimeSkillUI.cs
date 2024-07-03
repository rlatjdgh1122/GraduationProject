using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolTimeSkillUI : SkillUI
{
    public float cooltimeDuration; // ��Ÿ�� ���� �ð�
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
                currentFillAmount = 1f - (elapsedTime / cooltimeDuration); // ���� �ð� ���� ���
                bliendGauge.fillAmount = currentFillAmount; // ������ �� ������Ʈ
            }
            else
            {
                // ��Ÿ���� ������ �������� 0���� �����ϰ� ��Ÿ�� ����
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
