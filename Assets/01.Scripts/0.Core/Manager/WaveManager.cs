using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class WaveManager : MonoBehaviour
{

    #region 타임라인에 사용 된 변수
    private PlayableDirector pd;
    public TimelineAsset[] ta;
    #endregion

    #region 사용 변수들

    [Header("Wave Settings")]
    [SerializeField]
    private Transform _tentTrm;


    [Header("UI References")] //일단 임시로 여기에
    [SerializeField]
    private RectTransform _clockHandImgTrm;
    [SerializeField]
    private TextMeshProUGUI _timeText, _waveCntText, _enemyCntText;

    [Header("테스트 용")]
    public bool isWin;
    [SerializeField]
    Color targetColor;

    private int currentWaveCount = 0;
    public int CurrentWaveCount => currentWaveCount;

    public bool IsBattlePhase = false;
    public bool IsArrived = false;
    public bool CanTimer = true;

    public event Action OnBattlePhaseStartEvent = null;
    public event Action OnBattlePhaseEndEvent = null;
    public event Action OnIceArrivedEvent = null;

    private int maxEnemyCnt;

    private List<Penguin> _curPTspawnPenguins = new List<Penguin> ();

    #endregion

    #region UIManager UI
    NormalUI victoryUI
    {
        get
        {
            UIManager.Instance.overlayUIDictionary.TryGetValue(UIType.Victory, out NormalUI victoryUI);
            return victoryUI;
        }
    }

    NormalUI defeatUI
    {
        get
        {
            UIManager.Instance.overlayUIDictionary.TryGetValue(UIType.Defeat, out NormalUI defeatUI);
            return defeatUI;
        }
    }
    #endregion

    #region SingleTon
    private static WaveManager _instance;
    public static WaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WaveManager>();

                if (_instance == null)
                {
                    Debug.LogError("WaveManager is Multiple.");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        BattlePhaseSubscribe();
    }

    #endregion

    public void BattlePhaseSubscribe()
    {
        OnBattlePhaseStartEvent += OnBattlePhaseStartHandle; // 전투페이즈 시작 이벤트 구독
        OnBattlePhaseEndEvent += OnBattlePhaseEndHandle;     // 전투페이즈 종료 이벤트 
        OnIceArrivedEvent += OnIceArrivedHandle;
        OnBattlePhaseStartEvent += () => RotateClockHand(new Vector3(0.0f, 0.0f, 90.0f), 1f, Ease.InOutBack);
        OnBattlePhaseEndEvent += () => RotateClockHand(new Vector3(0.0f, 0.0f, -90.0f), 1f, Ease.InOutBack);
    }

    private void Start()
    {
        maxEnemyCnt = GameManager.Instance.GetCurrentEnemyCount(); // 테스트용
        BattlePhaseEndEventHandler(isWin);
    }

    private void Update()
    {
        if (IsBattlePhase)
        {
            if (GameManager.Instance.GetCurrentEnemyCount() <= 0)
                GetReward();

            if (IsArrived)
            {
                if (GameManager.Instance.GetCurrentPenguinCount() <= 0)
                    ShowDefeatUI();
            }
        }

        if (Input.GetKeyDown(KeyCode.U)) //디버그
        {
            BattlePhaseStartEventHandler();
        }

        if (Input.GetKeyDown(KeyCode.S)) // 테스트Dragon
        {
            currentWaveCount++;
        }
    }

    private void OnIceArrivedHandle()
    {
        IsArrived = true;
    }

    private void OnBattlePhaseStartHandle() // 전투페이즈 시작
    {
        IsBattlePhase = true;
        maxEnemyCnt = GameManager.Instance.GetCurrentEnemyCount();
        _waveCntText.SetText($"Current Wave: {CurrentWaveCount}");

        if(currentWaveCount == 4)
        {
            pd.Play(ta[0]);
        }
    }

    private void OnBattlePhaseEndHandle() // 전투페이즈 종료
    {
        IsBattlePhase = false;
        IsArrived = false;

        if (isWin)
        {
            currentWaveCount++;
            List<Penguin> penguins = FindObjectsOfType<Penguin>().Where(p => p.enabled).ToList();
            foreach (Penguin penguin in penguins)
            {
                penguin.CurrentTarget = null;
            }
            _waveCntText.SetText($"Next Wave: {CurrentWaveCount}");
        }
        else
        {
            ShowDefeatUI();
        }

        //_currentEnemyGround = null;
    }

    private void RotateClockHand(Vector3 vector, float targetTime, Ease ease, params Action[] actions) // 시계 업데이트
    {
        _clockHandImgTrm.DOLocalRotate(vector, targetTime).SetEase(ease).OnComplete(() =>
        {
            foreach (var action in actions) //실행할 함수가 있다면 실행
            {
                action?.Invoke();
            }
        });
    }

    private void ShowDefeatUI()
    {
        defeatUI.EnableUI(1, null);
    }

    private void GetReward() // 보상 획득 함수
    {
        ShowEffect();
        victoryUI.EnableUI(1f, null);
    }

    public void CloseWinPanel()
    {
        IsBattlePhase = false;

        victoryUI.DisableUI(1, OnBattlePhaseEndEvent);
    }

    private void ShowEffect() // 이펙트
    {
        //try // 전투에서 이겼다면 적의 땅을 흡수한다. (색을 바꿈)
        //{
        //    _currentEnemyGround?.EndWave();

        //    var mr = _currentEnemyGround.GetComponent<MeshRenderer>();
        //    Color color = mr.material.color;

        //    DOTween.To(() => color, c => color = c, targetColor, 3f).OnUpdate(() =>
        //    {
        //        mr.material.color = color;
        //    });

        //}
        //catch
        //{
        //    Debug.Log("MeshRenderer is Missing Or Current Wave is First Wave");
        //}
    }

    public void BattlePhaseStartEventHandler() // 전투페이즈 시작 이벤트 실행용 함수
    {
        OnBattlePhaseStartEvent?.Invoke();
    }

    public void BattlePhaseEndEventHandler(bool _isWin) // 전투페이즈 종료 이벤트 실행용 함수
    {
        isWin = _isWin;
        OnBattlePhaseEndEvent?.Invoke();
    }

    public void OnIceArrivedEventHanlder()
    {
        OnIceArrivedEvent?.Invoke();
    }

    public void SetCurPTSpawnPenguins(List<Penguin> penguins)
    {
        _curPTspawnPenguins.Clear();

        for(int i = 0; i < penguins.Count; i++) //넘겨 받은 리스트를 저장하고
        {
            _curPTspawnPenguins.Add(penguins[i]);
        }

        SetPosCurPTSpawnPenguin(); // 생성한 펭귄의 상태에 맞게 위치를 설정한다.
    }

    private void SetPosCurPTSpawnPenguin()
    {
        for (int i = 0; i < _curPTspawnPenguins.Count; i++)
        {
            //if () // 생성된 펭귄이 군단에 들어가있지 않으면 텐트로 돌아가게.
            _curPTspawnPenguins[i].SetCanInitTent(true);
            _curPTspawnPenguins[i].SetTarget(_tentTrm.position);
            //else // 군단에 들어가 있다면 알아서 군단위치로 가게
        }
    }

    private void OnDestroy()
    {
        OnBattlePhaseStartEvent -= OnBattlePhaseStartHandle;
        OnBattlePhaseEndEvent -= OnBattlePhaseEndHandle;
        OnIceArrivedEvent -= OnIceArrivedEventHanlder;
    }

}
