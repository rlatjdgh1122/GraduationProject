using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : WorldUI
{
    [SerializeField] private Image _skillBar;
    [SerializeField] private Image _line;

    [SerializeField] private CheckHitDecision _decision;

    private List<Image> _lines = new List<Image>(); // ���� ���ε��� ������ ����Ʈ

    public override void Awake()
    {
        base.Awake();

        SignalHub.OnBattlePhaseStartEvent += BattleStart;
        _decision.OnChangedMaxValueEvent += SetLine;
    }

    private void OnDestroy()
    {
        SignalHub.OnBattlePhaseStartEvent -= BattleStart;
        _decision.OnChangedMaxValueEvent -= SetLine;
    }



    private void BattleStart()
    {
        //��ų ������ Ǯ��
        _skillBar.fillAmount = 1f;
    }
    public void UpdateHpbarUI(float current, float max)
    {
        _skillBar.DOFillAmount(current / max, 0.5f);
    }

    private void SetLine(int count)
    {
        // ������ ������ ���� ����
        foreach (var line in _lines)
        {
            Destroy(line.gameObject);
        }
        _lines.Clear();

        // ������ ���� 1 ������ ���� ó������ ����
        if (count <= 1) return;

        float startX = -33.5f;
        float endX = 33.5f;
        float totalWidth = endX - startX;

        if (count == 2)
        {
            // count�� 2�� ���� �߾ӿ� �ϳ��� ���� ����
            Image newLine = Instantiate(_line, transform);
            _lines.Add(newLine);
            newLine.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        }
        else
        {
            // count�� 3 �̻��� ���� ������ �յ��ϰ� ��ġ
            for (int i = 1; i < count; i++)
            {
                // ���ο� ���� �̹��� ����
                Image newLine = Instantiate(_line, transform);
                _lines.Add(newLine);

                // �� ������ ��ġ ���
                float positionX = startX + totalWidth * (i / (float)(count));
                newLine.rectTransform.anchoredPosition = new Vector3(positionX, 0, 0);
            }
        }
    }
}

