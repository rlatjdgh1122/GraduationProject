using DG.Tweening;
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

    //public event Action OnDummyPenguinInitTentFinEvent = null;

    #endregion

    #region SingleTon
    //private static WaveManager _instance;
    //public static WaveManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<WaveManager>();

    //            if (_instance == null)
    //            {
    //                Debug.LogError("WaveManager is Multiple.");
    //            }
    //        }
    //        return _instance;
    //    }
    //}

    public override void Awake()
    {
        //if (_instance != this)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    _instance = this;
        //}
        base.Awake();

        _tentTrm = GameObject.Find("PenguinSpawner/Building/TentInitPos").transform;

        BattlePhaseSubscribe();
    }

    #endregion

    private bool isFirst = true;

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

        if (isWin)
        {
            currentWaveCount++;
            List<Penguin> penguins = FindObjectsOfType<Penguin>().Where(p => p.enabled).ToList(); // 이거 나중에 바꿔야함
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

        if (currentWaveCount == 11)
        {
            UIManager.Instance.ShowPanel("CreditUI");
        }

        if(currentWaveCount < 4)
        {
            SignalHub.OnLockButtonEvent?.Invoke();
        }
    }

    public void ShowDefeatUI()
    {
        UIManager.Instance.ShowPanel("DefeatUI");
    }

    private void GetReward() // 보상 획득 함수
    {
        UIManager.Instance.ShowPanel("VictoryUI", true);
    }

    public void CloseWinPanel()
    {
        BattlePhaseEndEventHandler(true);

        UIManager.Instance.HidePanel("VictoryUI");
    }

    public void BattlePhaseStartEventHandler() // 전투페이즈 시작 이벤트 실행용 함수
    {
        // 빙하 랜덤 생성 땜에 우선순위 이벤트 먼저 하고 함
        SignalHub.OnBattlePhaseStartPriorityEvent?.Invoke();
        CoroutineUtil.CallWaitForSeconds(0.1f, null, () => SignalHub.OnBattlePhaseStartEvent?.Invoke());

        if (currentWaveCount == 5)
            UIManager.Instance.ShowBossWarningUI("춘자 등장!");
        if (currentWaveCount == 10)
            UIManager.Instance.ShowBossWarningUI("보스 등장!");

        if (isFirst)
        {
            UIManager.Instance.GifController.ShowGif(GifType.PenguinFight);
            isFirst = false;
        }
    }

    public void BattlePhaseEndEventHandler(bool _isWin) // 전투페이즈 종료 이벤트 실행용 함수
    {
        isWin = _isWin;

        int questIdx = TutorialManager.Instance.CurTutoQuestIdx;
        bool isDone = questIdx == 0 || questIdx == 1 || questIdx == 2 || questIdx == 3 || questIdx == 4 || questIdx == 5;

        if (isDone) // 일단 퀘스트
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.Second);
        }
        else if (questIdx == 5)
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
        }

        SignalHub.OnBattlePhaseEndEvent?.Invoke();
    }

    public bool IsCurrentWaveCountEqualTo(int value)
    {
        return currentWaveCount == value; //TimeLineHolder에서 웨이브 수를 알기 위해서
    }

    /*public void DummyPenguinInitTentFinHandle()
    {
        OnDummyPenguinInitTentFinEvent?.Invoke();
    }*/

    //private void OnDestroy()
    //{
    //    OnBattlePhaseStartEvent -= OnBattlePhaseStartHandle;
    //    OnBattlePhaseEndEvent -= OnBattlePhaseEndHandle;
    //    OnIceArrivedEvent -= OnIceArrivedEventHanlder;
    //}

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
}
