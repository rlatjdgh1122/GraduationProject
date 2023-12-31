using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnPenguinButton : MonoBehaviour
{
    private Button _btn;

    [Tooltip("쿨타임을 알려줄 이미지 (자식에 있다)")]
    [SerializeField] private Image _coolingimg;

    [Tooltip("쿨타임")]
    [SerializeField] private float cooltime;

    [Tooltip("무엇을 생성할 것인지. Prefab을 넣어주면 된다.")]
    [SerializeField] private Penguin spawnPenguin;
    
    private PenguinFactory _penguinFactory; // 팩토리

    protected virtual void Awake()
    {
        _penguinFactory = GameObject.Find("PenguinSpawner/PenguinFactory").GetComponent<PenguinFactory>();
        _btn = GetComponent<Button>();
    }

    public void SpawnPenguinEventHandler() //Inspector 버튼 이벤트에서 구독할 함수
    {
        if(WaveManager.Instance.IsPhase)
        {
            UIManager.Instance.InitializeWarningTextSequence();
            UIManager.Instance.WarningTextSequence.Prepend(_penguinFactory.SpawnFailHudText.DOFade(1f, 0.5f))
            .Join(_penguinFactory.SpawnFailHudText.rectTransform.DOMoveY(UIManager.Instance.ScreenCenterVec.y, 0.5f))
            .Append(_penguinFactory.SpawnFailHudText.DOFade(0f, 0.5f))
            .Join(_penguinFactory.SpawnFailHudText.rectTransform.DOMoveY(UIManager.Instance.ScreenCenterVec.y - 50f, 0.5f));

            return;
        }

        if (WaveManager.Instance.RemainingPhaseReadyTime >= cooltime) // 남은 준비시간안에 생성할 수 있다면 생성한다.
        {
            TestLegion.Instance.LegionUIList[0].HeroCnt++;

            ButtonCooldown();
        }
    }

    private void ButtonCooldown() // 버튼 누르면 실행될 함수
    {
        _btn.interactable = false;
        _coolingimg.fillAmount = 1f;

        DOTween.To(() => _coolingimg.fillAmount, f => _coolingimg.fillAmount = f, 0f, cooltime).OnComplete(() => // 생성시간이 다 되었다면
        {
            _btn.interactable = true;
            _penguinFactory.SpawnPenguinHandler(spawnPenguin); // 팩토리에서 생성하는 함수 실행
        });
    }

}
