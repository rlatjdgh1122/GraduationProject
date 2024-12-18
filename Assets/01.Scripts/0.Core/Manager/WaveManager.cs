﻿using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    #region 사용 변수들

    [Header("Wave Settings")]
    private Transform _tentTrm;

    [Header("테스트 용")]
    public bool isWin;
    [SerializeField]
    Color targetColor;

    private int currentWaveCount = 1;
    public int CurrentWaveCount => currentWaveCount;

    public bool IsBattlePhase = false;
    public bool IsArrived = false;

    #endregion

    #region SingleTon

    public override void Awake()
    {
        base.Awake();

        _tentTrm = GameObject.Find("PenguinSpawner/Building/TentInitPos").transform;

        BattlePhaseSubscribe();
    }

    #endregion

    public bool OnBuildArcherTower = false;
    public bool OnBuildBuffTower = false;

    public bool IsSpawnedUniqueEnemy { get; private set; } = false;

    private void Update()
    {

        //시네 치트키
        if(Input.GetKeyDown(KeyCode.K))
        {
            List<Enemy> gameObjects = FindObjectsOfType<Enemy>().ToList();
            gameObjects.ForEach(obj => obj.GetComponent<Health>().Dead());
        }
    }

    public void BattlePhaseSubscribe()
    {
        SignalHub.OnBattlePhaseStartEvent += OnBattlePhaseStartHandle; // 전투페이즈 시작 이벤트 구독
        SignalHub.OnBattlePhaseEndEvent += OnBattlePhaseEndHandle;     // 전투페이즈 종료 이벤트 
        SignalHub.OnGroundArrivedEvent += OnIceArrivedHandle;
    }

    private void OnIceArrivedHandle()
    {
        IsArrived = true;
    }

    private void OnBattlePhaseStartHandle() // 전투페이즈 시작
    {
        IsBattlePhase = true;
    }

    private void OnBattlePhaseEndHandle() // 전투페이즈 종료
    {
        IsBattlePhase = false;
        IsArrived = false;
        IsSpawnedUniqueEnemy = false;

        if (isWin)
        {
            currentWaveCount++;
            List<Penguin> penguins = FindObjectsOfType<Penguin>().Where(p => p.enabled).ToList();
            foreach (Penguin penguin in penguins)
            {
                penguin.CurrentTarget = null;
            }
        }
        else
        {
            ShowDefeatUI();
        }

        //Debug.Log(currentWaveCount);
    }

    public void ShowDefeatUI()
    {
        UIManager.Instance.ResetPanel();
        UIManager.Instance.ShowPanel("DefeatUI");
    }

    private void GetReward() // 보상 획득 함수
    {
        UIManager.Instance.ResetPanel();
        UIManager.Instance.ShowPanel("VictoryUI");
    }

    public void CloseWinPanel()
    {
        BattlePhaseEndEventHandler(true);

        UIManager.Instance.HidePanel("VictoryUI");
        UIManager.Instance.ResetPanel();
    }

    public void BattlePhaseStartEventHandler() // 전투페이즈 시작 이벤트 실행용 함수
    {
        // 빙하 랜덤 생성 땜에 우선순위 이벤트 먼저 하고 함
        SignalHub.OnBattlePhaseStartPriorityEvent?.Invoke();
        CoroutineUtil.CallWaitForSeconds(0.1f, () => SignalHub.OnBattlePhaseStartEvent?.Invoke());

        if (currentWaveCount == 5)
            UIManager.Instance.ShowBossWarningUI("춘자 등장!");
        if (currentWaveCount == 10)
            UIManager.Instance.ShowBossWarningUI("덕배 등장!"); 
        if (currentWaveCount == 15)
            UIManager.Instance.ShowBossWarningUI("봉무스 등장!");
    }

    public void BattlePhaseEndEventHandler(bool _isWin) // 전투페이즈 종료 이벤트 실행용 함수
    {
        isWin = _isWin;

        SignalHub.OnBattlePhaseEndEvent?.Invoke();

        if (currentWaveCount == 16)
        {
            UIManager.Instance.ResetPanel();
            UIManager.Instance.ShowPanel("CreditUI");
        }
    }

    public bool IsCurrentWaveCountEqualTo(int value)
    {
        return currentWaveCount == value; //TimeLineHolder에서 웨이브 수를 알기 위해서
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= OnBattlePhaseStartHandle;
        SignalHub.OnBattlePhaseEndEvent -= OnBattlePhaseEndHandle;
    }

    public void CheckIsEndBattlePhase()
    {
        if (GameManager.Instance.GetCurrentEnemyCount() <= 0)
        {
            GetReward();
        }
    }

    public void SetSpawnedUniqueEnemy()
    {
        IsSpawnedUniqueEnemy = true;
    }
}
