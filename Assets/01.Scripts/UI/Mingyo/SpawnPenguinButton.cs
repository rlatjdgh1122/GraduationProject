using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpawnPenguinButton : MonoBehaviour
{
    private Button _btn;
    private Sprite _face;
    private TextMeshProUGUI _name;

    [SerializeField] private int _price;
    [Tooltip("무엇을 생성할 것인지. Prefab을 넣어주면 된다.")]
    [SerializeField] private PenguinStat _penguinStat;
    [SerializeField] private Penguin spawnPenguin;
    
    //private PenguinFactory _penguinFactory; // 팩토리
    private PenguinStoreUI _penguinStore;

    protected virtual void Awake()
    {
        _penguinStore = transform.parent.parent.parent.GetComponent<PenguinStoreUI>();
        _face = transform.Find("PenguinImg").GetComponent<Sprite>();
        _name = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
        //_penguinFactory = GameObject.Find("PenguinSpawner/PenguinFactory").GetComponent<PenguinFactory>();
        //_btn = GetComponent<Button>();
    }

    public void SlotUpdate()
    {
        _face = _penguinStat.PenguinIcon;
        _name.text = _penguinStat.PenguinName;
    }

    public void SpawnPenguinEventHandler() //Inspector 버튼 이벤트에서 구독할 함수
    {
        _penguinStore.PenguinInformataion(_penguinStat, _price);
        _penguinStore.OnEnableBuyPanel();


        //if(WaveManager.Instance.IsBattlePhase)
        //{
        //    UIManager.Instance.InitializHudTextSequence();
        //    _penguinFactory.SetSpawnFailHudText("전투 페이즈에는 생성할 수 없습니다");

        //    UIManager.Instance.SpawnHudText(_penguinFactory.FailHudText);
        //    return;
        //}

        //if (!WaveManager.Instance.IsBattlePhase) // 남은 준비시간안에 생성할 수 있다면 생성한다.
        //{

        //}
    }

    private void ButtonCooldown() // 버튼 누르면 실행될 함수
    {
        //_btn.interactable = false;
        //_coolingimg.fillAmount = 1f;

        //UIManager.Instance.InitializHudTextSequence();
        //UIManager.Instance.SpawnHudText(_penguinFactory.SuccesHudText);

        //LegionInventory.Instance.AddPenguin(spawnPenguin.ReturnGenericStat<PenguinStat>());

        //DOTween.To(() => _coolingimg.fillAmount, f => _coolingimg.fillAmount = f, 0f, cooltime).OnComplete(() => // 생성시간이 다 되었다면
        //{
        //    _btn.interactable = true;
        //    _penguinFactory.SpawnPenguinHandler(spawnPenguin); // 팩토리에서 생성하는 함수 실행
        //});
    }

}
