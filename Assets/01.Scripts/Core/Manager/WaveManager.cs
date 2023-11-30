using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
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
                    Debug.LogError("WaveManager 인스턴스를 찾을 수 없습니다.");
                }
            }
            return _instance;
        }
    }

    #region 사용 변수들

    [Header("Wave Settings")]
    [SerializeField]
    private int maxPhaseReadyTime;
    private int remainingPhaseReadyTime;

    [Header("UI References")]
    [SerializeField]
    private RectTransform clockHandImgTrm;
    [SerializeField]
    private TextMeshProUGUI timeText, wavCntText;

    private int waveCnt = 0;

    private bool isPhase = false;
    public bool IsPhase => isPhase;

    public event Action OnPhaseStartEvent = null;
    public event Action OnPhaseEndEvent = null;

    #endregion

    private void Awake()
    {
        if (_instance != this && _instance != null) // 싱글톤
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        OnPhaseStartEvent += OnPhaseStartHandle; // 전투페이즈 시작 이벤트 구독
        OnPhaseEndEvent += OnPhaseEndHandle;     // 전투페이즈 종료 이벤트 구독
    }

    private void Start()
    {
        SetReadyTime(); // 시간 초기화
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 디버그용
        {
            InvokePhaseEndEvent();
        }
    }

    private void SetReadyTime() // 준비 시간을 초기화한다.
    {
        remainingPhaseReadyTime = maxPhaseReadyTime;
    }

    private void OnPhaseStartHandle() // 전투페이즈 시작
    {
        isPhase = true;
        wavCntText.SetText($"Current Wave:{waveCnt}");
        RotateClockHand(new Vector3(0, 0, 90), 0.7f, Ease.InOutElastic, SpawnEnemy);
    }

    private void OnPhaseEndHandle() // 전투페이즈 종료
    {
        isPhase = false;
        waveCnt++;
        wavCntText.SetText($"Next Wave:{waveCnt}");
        RotateClockHand(new Vector3(0, 0, 0), 0.2f, Ease.Linear, StartPhaseReadyRoutine);
    }

    private void StartPhaseReadyRoutine() // 준비시간 계산 코루틴 실행용 함수
    {
        StartCoroutine(PhaseReadyRoutine());
    }

    private IEnumerator PhaseReadyRoutine() // 준비시간 계산 코루틴
    {
        while (remainingPhaseReadyTime >= 0) // 현재 남은 준비 시간이 0보다 크다면
        {
            UpdateClockHandRotation(); // 시계를 업데이트
            UpdateTimeText(); // 시간 텍스트 업데이트 

            yield return new WaitForSeconds(1.0f); // 1초후
            remainingPhaseReadyTime--; // 남은 준비시간 - 1
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

        if (minutes > 0) { timeText.SetText($"{minutes}:{remainingSeconds}"); } //분으로 나타낼 수 있다면 분까지 나타낸다.
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

    private void SpawnEnemy()
    {
        // 적 생성
        Debug.Log("적 생성");
    }

    private void InvokePhaseStartEvent() // 전투페이즈 시작 이벤트 실행용 함수
    {
        OnPhaseStartEvent?.Invoke();
    }

    private void InvokePhaseEndEvent() // 전투페이즈 종료 이벤트 실행용 함수
    {
        OnPhaseEndEvent?.Invoke();
    }

    private void OnDisable()
    {
        OnPhaseStartEvent -= OnPhaseStartHandle;
        OnPhaseEndEvent -= OnPhaseEndHandle;
    }
}
