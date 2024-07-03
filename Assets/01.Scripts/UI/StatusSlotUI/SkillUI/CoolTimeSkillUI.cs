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
        float duration = value; // ��Ÿ�� ���� �ð�
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentFillAmount = 1f - (elapsedTime / duration); // ���� �ð� ���� ���
            bliendGauge.fillAmount = currentFillAmount; // ������ �� ������Ʈ

            yield return null;
        }

        // ��Ÿ���� ������ �������� 0���� ����
        bliendGauge.fillAmount = 0f;
        currentFillAmount = 0f;

        OnSkillReadyEvent?.Invoke();
    }
}
