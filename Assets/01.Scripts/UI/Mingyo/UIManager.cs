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
        btnInfo.CoolingImg.fillAmount = 1.0f;

        GameManager.Instance.PlusDummyPenguinCount();

        Tween tween = DOTween.To(() => btnInfo.CoolingImg.fillAmount, f => btnInfo.CoolingImg.fillAmount = f, .0f, btnInfo.CoolTime);
        tween.OnKill(() =>
        {
            btnInfo.Btn.interactable = true;
            spawnAction?.Invoke();
            Debug.Log("qnpfer");
        });
    }


    public override void Init()
    {
    }
    
}
