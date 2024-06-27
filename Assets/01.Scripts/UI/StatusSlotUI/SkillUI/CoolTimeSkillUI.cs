using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolTimeSkillUI : SkillUI
{
    private void Start()
    {
        OnChangedMaxValue(5);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) OnChangedMaxValue(value - 1);
        if (Input.GetKeyDown(KeyCode.K)) OnSkillUsed();
    }

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
            blinedGauge.fillAmount = currentFillAmount; // ������ �� ������Ʈ

            yield return null;
        }

        // ��Ÿ���� ������ �������� 0���� ����
        blinedGauge.fillAmount = 0f;
        currentFillAmount = 0f;

        OnSkillReadyEvent?.Invoke();
    }
}
