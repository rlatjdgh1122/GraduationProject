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

    private List<Image> _lines = new List<Image>(); // 기존 라인들을 저장할 리스트

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
        //스킬 게이지 풀로
        _skillBar.fillAmount = 1f;
    }
    public void UpdateHpbarUI(float current, float max)
    {
        _skillBar.DOFillAmount(current / max, 0.5f);
    }

    private void SetLine(int count)
    {
        // 기존에 생성된 라인 제거
        foreach (var line in _lines)
        {
            Destroy(line.gameObject);
        }
        _lines.Clear();

        // 라인의 수가 1 이하인 경우는 처리하지 않음
        if (count <= 1) return;

        float startX = -33.5f;
        float endX = 33.5f;
        float totalWidth = endX - startX;

        if (count == 2)
        {
            // count가 2일 때는 중앙에 하나의 라인 생성
            Image newLine = Instantiate(_line, transform);
            _lines.Add(newLine);
            newLine.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        }
        else
        {
            // count가 3 이상일 때는 라인을 균등하게 배치
            for (int i = 1; i < count; i++)
            {
                // 새로운 라인 이미지 생성
                Image newLine = Instantiate(_line, transform);
                _lines.Add(newLine);

                // 각 라인의 위치 계산
                float positionX = startX + totalWidth * (i / (float)(count));
                newLine.rectTransform.anchoredPosition = new Vector3(positionX, 0, 0);
            }
        }
    }
}

