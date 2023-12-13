using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] private PenguinSpawner _penguinSpawner;
    public PenguinSpawner PenguinSpawnerComPo => _penguinSpawner;
    public VictoryUI victoryUI;

    public void UIMoveDot(RectTransform transform, Vector3 targetVec, float duration,
                          Ease ease = Ease.Linear, params Action[] actions)
    {
        transform.DOMove(targetVec, duration).SetEase(ease).OnComplete(() =>
        {
            foreach (Action action in actions)
            {
                action?.Invoke();
            }
        });
    }

    public void ButtonCooldown(SpawnPenguinBtnInfo btnInfo, Action spawnAction)
    {
        btnInfo.Btn.interactable = false;
        btnInfo.CoolingImg.fillAmount = 1f;

        GameManager.Instance.PlusDummyPenguinCount();

        DOTween.To(() => btnInfo.CoolingImg.fillAmount, f => btnInfo.CoolingImg.fillAmount = f, 0f, btnInfo.CoolTime).OnUpdate(() => Debug.Log(btnInfo.CoolingImg.fillAmount)).OnComplete(() =>
        {
            spawnAction?.Invoke();
            btnInfo.Btn.interactable = true;
        });
    }

    public override void Init()
    {
    }
    
}
