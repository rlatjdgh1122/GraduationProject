using DG.Tweening;
using System;
using System.Collections;
using System.Net.NetworkInformation;
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

    private int curWaveCnt;
    public int WaveCnt => curWaveCnt;

    [SerializeField]
    private int remainingPhaseReadyTime; //  현재 남은 페이지 준비 시간
    [SerializeField]
    private int maxPhaseReadyTime; // 페이지 준비 시간

    [SerializeField]
    private RectTransform clockHandImg;
    [SerializeField]
    private TextMeshProUGUI timeText;

    private bool isPhase = false; //현재 페이지진행중
    public bool IsPhase => isPhase;

    public event Action OnPhaseStartEvent = null; // 페이지 시작 이벤트
    public event Action OnPhaseEndEvent = null; // 페이지 시작 이벤트

    #endregion

    private void Awake()
    {
        if (_instance != this && _instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        OnPhaseStartEvent += OnPhaseStartHandle;
        OnPhaseEndEvent += OnPhaseEndHandle;
    }

    private void Start()
    {
        SetReadyTime();
        OnPhaseEndEvent?.Invoke();
    }

    private void Update()
    {
        Debug.Log(clockHandImg.transform.localRotation.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPhaseEndEvent?.Invoke();
        }
    }

    private void SetReadyTime()
    {
        remainingPhaseReadyTime = maxPhaseReadyTime;
    }

    private void OnPhaseStartHandle()
    {
        SpawnEnemy();
    }

    private void OnPhaseEndHandle()
    {
        StartCoroutine(PhaseReadyRoutine());
    }

    private IEnumerator PhaseReadyRoutine()
    {
        int minutes = 0;
        int remainingSeconds = 0;
        clockHandImg.DOLocalRotate(new Vector3(0, 0, -180), remainingPhaseReadyTime);


        while (remainingPhaseReadyTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            remainingPhaseReadyTime -= 1;
            minutes = remainingPhaseReadyTime / 60;
            remainingSeconds = remainingPhaseReadyTime % 60;

            if (minutes > 0) { timeText.SetText($"{minutes}:{remainingSeconds}"); }
            else { timeText.SetText($"{remainingSeconds}"); }
        }

        SetReadyTime();
        OnPhaseStartEvent?.Invoke();
    }

    private void SpawnEnemy()
    {
        // 적 생성
        Debug.Log("적 생성");
    }

    private void OnDisable()
    {
        OnPhaseStartEvent -= OnPhaseStartHandle;
        OnPhaseEndEvent -= OnPhaseEndHandle;
    }
}
