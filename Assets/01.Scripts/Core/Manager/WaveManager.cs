﻿using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    #region 사용 변수들

    [Header("Wave Settings")]
    [SerializeField]
    private int maxPhaseReadyTime;
    private int remainingPhaseReadyTime;
    private IceMove _currentEnemyGround;


    [Header("UI References")] //임시로 여기있지만 나중에 UIManager로 옮겨야 한다.
    [SerializeField]
    private RectTransform clockHandImgTrm;
    [SerializeField]
    private TextMeshProUGUI timeText, wavCntText, enemyCntText;
    [SerializeField]
    private RectTransform loseUI;

    [Header("테스트 용")]
    public bool isWin;
    [SerializeField]
    Color targetColor;

    private int waveCnt = 0;

    private bool isPhase = false;
    public bool IsPhase => isPhase;

    public event Action OnPhaseStartEvent = null;
    public event Action OnPhaseEndEvent = null;
    public event Action OnIceArrivedEvent = null;

    private int maxEnemyCnt;

    #endregion

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
        if (_instance != this && _instance != null) //  ̱   
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
        maxEnemyCnt = GameManager.Instance.GetEnemyPenguinCount; // 테스트용
        SetReadyTime(); // 시간 초기화
        InvokePhaseEndEvent(isWin);

    }

    private void SetReadyTime() // 준비 시간을 초기화한다.
    {
        remainingPhaseReadyTime = maxPhaseReadyTime;
    }

    private void OnPhaseStartHandle() // 전투페이즈 시작
    {
        isPhase = true;
        maxEnemyCnt = GameManager.Instance.GetEnemyPenguinCount;
        wavCntText.SetText($"Current Wave: {waveCnt}");
    }

    private void OnPhaseEndHandle() // 전투페이즈 종료
    {
        isPhase = false;

        if (isWin)
        {
            waveCnt++;
            wavCntText.SetText($"Next Wave: {waveCnt}");
            GetReward();
            UpdateUIOnEnemyCount();
        }
        else
        {
            ShowLoseUI();
        }

        _currentEnemyGround = null;
    }

    private void ShowLoseUI()
    {
        loseUI.gameObject.SetActive(true);
    }

    private void GetReward() // 보상 획득 함수
    {
        //여기서 뭐 내땅의 자식으로 두거나 해서 내땅으로 만들어야할듯

        ShowEffect(); // 이펙트
    }

    private void ShowEffect() // 이펙트
    {
        try // 전투에서 이겼다면 적의 땅을 흡수한다. (색을 바꿈)
        {
            _currentEnemyGround?.EndWave();

            var mr = _currentEnemyGround.GetComponent<MeshRenderer>();
            Color color = mr.material.color;

            DOTween.To(() => color, c => color = c, targetColor, 3f).OnUpdate(() =>
            {
                mr.material.color = color;
            });

        }
        catch
        {
            Debug.Log("MeshRenderer is Missing Or Current Wave is First Wave");
        }
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
            remainingPhaseReadyTime--; // 남은 준비시간 -1
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

        if (minutes > 0) { timeText.SetText($"{minutes}: {remainingSeconds}"); } //분으로 나타낼 수 있다면 분까지 나타낸다.
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
    }

    public void SetCurrentEnemyGround(IceMove ice)
    {
        _currentEnemyGround = ice;
    }

    public void UpdateUIOnEnemyCount()
    {
        int enemyCnt = GameManager.Instance.GetEnemyPenguinCount;

        if (enemyCnt == maxEnemyCnt)
        {
            RotateClockHand(new Vector3(0, 0, 0), 0.2f, Ease.Linear, StartPhaseReadyRoutine);
        }
        else if (enemyCnt <= 0)
        {
            InvokePhaseEndEvent(true);
        }
        else
        {
            float rotationAngle = -(180 / maxEnemyCnt) * (maxEnemyCnt - enemyCnt) + 180;
            Vector3 rotationVec = new Vector3(0, 0, rotationAngle);
            RotateClockHand(rotationVec, 0.2f, Ease.Linear);
        }

        enemyCntText.SetText($"Enemy: {enemyCnt}");
    }
}
