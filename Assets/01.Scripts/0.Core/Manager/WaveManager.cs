using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    #region 사용 변수들

    [Header("Wave Settings")]
    [SerializeField]
    private int maxPhaseReadyTime;
    private int remainingPhaseReadyTime;
    public int RemainingPhaseReadyTime => remainingPhaseReadyTime;
    [SerializeField]
    private Transform _tentTrm;


    [Header("UI References")] //일단 임시로 여기에
    [SerializeField]
    private RectTransform clockHandImgTrm;
    [SerializeField]
    private TextMeshProUGUI timeText, wavCntText, enemyCntText;

    [Header("테스트 용")]
    public bool isWin;
    [SerializeField]
    Color targetColor;

    public int CurrentStage = 0;

    public bool IsPhase = false;
    public bool IsArrived = false;
    public bool CanTimer = true;

    public event Action OnPhaseStartEvent = null;
    public event Action OnPhaseEndEvent = null;
    public event Action OnIceArrivedEvent = null;

    private int maxEnemyCnt;

    private bool isFirst = true;
    private List<Penguin> _curPTspawnPenguins = new List<Penguin> ();

    #endregion

    #region UIManager UI
    PopupUI victoryUI
    {
        get
        {
            UIManager.Instance.uiDictionary.TryGetValue(UI.Victory, out PopupUI victoryUI);
            return victoryUI;
        }
    }

    PopupUI defeatUI
    {
        get
        {
            UIManager.Instance.uiDictionary.TryGetValue(UI.Defeat, out PopupUI defeatUI);
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
    }

    #endregion

    public void OnEnable()
    {
        OnPhaseStartEvent += OnPhaseStartHandle; // 전투페이즈 시작 이벤트 구독
        OnPhaseEndEvent += OnPhaseEndHandle;     // 전투페이즈 종료 이벤트 
        OnIceArrivedEvent += OnIceArrivedHandle;
    }

    private void Start()
    {
        maxEnemyCnt = GameManager.Instance.GetCurrentEnemyCount(); // 테스트용
        SetReadyTime(); // 시간 초기화
        InvokePhaseEndEvent(isWin);
    }

    private void Update()
    {
        if (IsPhase)
        {
            if (GameManager.Instance.GetCurrentEnemyCount() <= 0)
                GetReward();

            if (IsArrived)
            {
                if (GameManager.Instance.GetCurrentPenguinCount() <= 0)
                    ShowDefeatUI();
            }
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            remainingPhaseReadyTime -= 3;
        }
    }

    private void SetReadyTime() // 준비 시간을 초기화한다.
    {
        remainingPhaseReadyTime = maxPhaseReadyTime;
    }

    private void OnIceArrivedHandle()
    {
        IsArrived = true;
    }

    private void OnPhaseStartHandle() // 전투페이즈 시작
    {
        IsPhase = true;
        maxEnemyCnt = GameManager.Instance.GetCurrentEnemyCount();
        wavCntText.SetText($"Current Wave: {CurrentStage}");
        UpdateTimeText();
    }

    private void OnPhaseEndHandle() // 전투페이즈 종료
    {
        IsPhase = false;
        IsArrived = false;

        if (isWin)
        {
            CurrentStage++;
            wavCntText.SetText($"Next Wave: {CurrentStage}");
            UpdateUIOnEnemyCount();
        }
        else
        {
            ShowDefeatUI();
        }

        //_currentEnemyGround = null;
    }

    private void ShowDefeatUI()
    {
        defeatUI.EnableUI(1);
    }

    private void GetReward() // 보상 획득 함수
    {
        ShowEffect();
        
        victoryUI.EnableUI(1f);
    }

    public void CloseWinPanel()
    {
        IsPhase = false;

        victoryUI.DisableUI(1, OnPhaseEndEvent);
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

    private void StartPhaseReadyRoutine() // 준비시간 계산 코루틴 실행용 함수
    {
        StartCoroutine(PhaseReadyRoutine());
    }

    private IEnumerator PhaseReadyRoutine() // 준비시간 계산 코루틴
    {
        while (remainingPhaseReadyTime >= 0) // 현재 남은 준비 시간이 0보다 크다면
        {
            if (CanTimer)
            {
                UpdateClockHandRotation(); // 시계를 업데이트
                UpdateTimeText(); // 시간 텍스트 업데이트

                yield return new WaitForSeconds(1.0f); // 1초후
                remainingPhaseReadyTime--; // 남은 준비시간 -1
            }
            else
                yield return null;  
        }

        // 준비시간이 끝났다면

        SetReadyTime(); // 남은 준비시간 초기화
        InvokePhaseStartEvent(); // 전투 페이즈 시작
    }

    private void UpdateClockHandRotation() // 시계 업데이트
    {
        // 회전할 값
        float rotationAngle = Mathf.Lerp(0,
                                         -180,
                                         1f - (remainingPhaseReadyTime / (float)maxPhaseReadyTime));
        RotateClockHand(new Vector3(0, 0, rotationAngle), 1f, Ease.Linear); // 계산된 값으로 회전
    }

    private void UpdateTimeText() // 시간 텍스트 업데이트
    {
        int minutes = remainingPhaseReadyTime / 60;          // 분
        int remainingSeconds = remainingPhaseReadyTime % 60; // 초

        if (IsPhase) { timeText.SetText($"전투 진행중"); }
        else if (minutes > 0) { timeText.SetText($"{minutes}: {remainingSeconds}"); } //분으로 나타낼 수 있다면 분까지 나타낸다.
        else { timeText.SetText($"{remainingSeconds}"); }
    }

    private void RotateClockHand(Vector3 vector, float targetTime, Ease ease, params Action[] actions) // 시계 업데이트
    {
        clockHandImgTrm.DOLocalRotate(vector, targetTime).SetEase(ease).OnComplete(() =>
        {
            foreach (var action in actions) //실행할 함수가 있다면 실행
            {
                action?.Invoke();
            }
        });
    }

    public void InvokePhaseStartEvent() // 전투페이즈 시작 이벤트 실행용 함수
    {
        OnPhaseStartEvent?.Invoke();
    }

    public void InvokePhaseEndEvent(bool _isWin) // 전투페이즈 종료 이벤트 실행용 함수
    {
        isWin = _isWin;
        OnPhaseEndEvent?.Invoke();
    }

    public void OnIceArrivedEventHanlder()
    {
        OnIceArrivedEvent?.Invoke();
    }

    private void OnDisable()
    {
        OnPhaseStartEvent -= OnPhaseStartHandle;
        OnPhaseEndEvent -= OnPhaseEndHandle;
        OnIceArrivedEvent -= OnIceArrivedEventHanlder;
    }

    public void UpdateUIOnEnemyCount()
    {
        int enemyCnt = GameManager.Instance.GetCurrentEnemyCount();
        int friendlyCnt = GameManager.Instance.GetCurrentPenguinCount();

        if (enemyCnt == maxEnemyCnt)
        {
            RotateClockHand(new Vector3(0, 0, 0), 0.2f, Ease.Linear, StartPhaseReadyRoutine);
        }
        else if (enemyCnt <= 0)
        {
            GetReward();
            InvokePhaseEndEvent(true);
        }
        else
        {
            float rotationAngle = -(180 / maxEnemyCnt) * (maxEnemyCnt - enemyCnt) + 180;
            Vector3 rotationVec = new Vector3(0, 0, 5);
            RotateClockHand(rotationVec, 0.2f, Ease.Linear);
        }


        if (friendlyCnt <= 0 && !isFirst)
        {
            ShowDefeatUI();
            return;
        }

        enemyCntText.SetText($"Enemy: {enemyCnt}");

        isFirst = false;    
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
            _curPTspawnPenguins[i].SetTarget(_tentTrm.position);
            //else // 군단에 들어가 있다면 알아서 군단위치로 가게
        }
    }
}
