using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEditor.Rendering;
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
                    Debug.LogError("WaveManager �ν��Ͻ��� ã�� �� �����ϴ�.");
                }
            }
            return _instance;
        }
    }

    #region ��� ������

    [Header("Wave Settings")]
    [SerializeField]
    private int maxPhaseReadyTime;
    private int remainingPhaseReadyTime;
    private GameObject _currentEnemyGround;


    [Header("UI References")]
    [SerializeField]
    private RectTransform clockHandImgTrm;
    [SerializeField]
    private TextMeshProUGUI timeText, wavCntText;
    [SerializeField]
    private RectTransform loseUI;

    [Header("�׽�Ʈ ��")]
    public bool isWin;
    [SerializeField]
    Color targetColor;

    private int waveCnt = 0;

    private bool isPhase = false;
    public bool IsPhase => isPhase;

    public event Action OnPhaseStartEvent = null;
    public event Action OnPhaseEndEvent = null;

    #endregion

    private void Awake()
    {
        if (_instance != this && _instance != null) // �̱���
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        OnPhaseStartEvent += OnPhaseStartHandle; // ���������� ���� �̺�Ʈ ����
        OnPhaseEndEvent += OnPhaseEndHandle;     // ���������� ���� �̺�Ʈ ����
    }

    private void Start()
    {
        SetReadyTime(); // �ð� �ʱ�ȭ
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // ����׿�
        {
            InvokePhaseEndEvent(isWin);
        }
    }

    private void SetReadyTime() // �غ� �ð��� �ʱ�ȭ�Ѵ�.
    {
        remainingPhaseReadyTime = maxPhaseReadyTime;
    }

    private void OnPhaseStartHandle() // ���������� ����
    {
        isPhase = true;
        wavCntText.SetText($"Current Wave:{waveCnt}");
        RotateClockHand(new Vector3(0, 0, 90), 0.7f, Ease.InOutElastic, SpawnEnemy); // ���� ���� ����ؼ� �ð� ���ư����� �ٲ� ��
    }

    private void OnPhaseEndHandle() // ���������� ����
    {
        isPhase = false;

        if (isWin)
        {
            waveCnt++;
            wavCntText.SetText($"Next Wave:{waveCnt}");
            GetReward();
            RotateClockHand(new Vector3(0, 0, 0), 0.2f, Ease.Linear, StartPhaseReadyRoutine);
        }
        else
        {
            ShowLoseUI();
        }
        
    }

    private void ShowLoseUI()
    {
        loseUI.gameObject.SetActive(true);
    }

    private void GetReward() // ���� ȹ�� �Լ�
    {
        //���⼭ �� ������ �ڽ����� �ΰų� �ؼ� �������� �������ҵ�

        ShowEffect(); // ����Ʈ
    }

    private void ShowEffect() // ����Ʈ
    {
        try
        {
            var mr = _currentEnemyGround.GetComponent<MeshRenderer>();
            Color color = mr.material.color;

            DOTween.To(() => color, c => color = c, targetColor, 0.7f).OnUpdate(() =>
            {
                mr.material.color = color;
            });
        }
        catch
        {
            Debug.Log("MeshRenderer is Missing Or First Fight");
        }
    }

    private void StartPhaseReadyRoutine() // �غ�ð� ��� �ڷ�ƾ ����� �Լ�
    {
        StartCoroutine(PhaseReadyRoutine());
    }

    private IEnumerator PhaseReadyRoutine() // �غ�ð� ��� �ڷ�ƾ
    {
        while (remainingPhaseReadyTime >= 0) // ���� ���� �غ� �ð��� 0���� ũ�ٸ�
        {
            UpdateClockHandRotation(); // �ð踦 ������Ʈ
            UpdateTimeText(); // �ð� �ؽ�Ʈ ������Ʈ

            yield return new WaitForSeconds(1.0f); // 1����
            remainingPhaseReadyTime--; // ���� �غ�ð� -1
        }

        // �غ�ð��� �����ٸ�

        SetReadyTime(); // ���� �غ�ð� �ʱ�ȭ
        InvokePhaseStartEvent(); // ���� ������ ����
    }

    private void UpdateClockHandRotation() // �ð� ������Ʈ
    {
        // ȸ���� ��
        float rotationAngle = Mathf.Lerp(0,
                                         -180,
                                         1f - (remainingPhaseReadyTime / (float)maxPhaseReadyTime));
        RotateClockHand(new Vector3(0, 0, rotationAngle), 1f, Ease.Linear); // ���� ������ ȸ��
    }

    private void UpdateTimeText() // �ð� �ؽ�Ʈ ������Ʈ
    {
        int minutes = remainingPhaseReadyTime / 60;          // ��
        int remainingSeconds = remainingPhaseReadyTime % 60; // ��

        if (minutes > 0) { timeText.SetText($"{minutes}:{remainingSeconds}"); } //������ ��Ÿ�� �� �ִٸ� �б��� ��Ÿ����.
        else { timeText.SetText($"{remainingSeconds}"); }
    }

    private void RotateClockHand(Vector3 vector, float targetTime, Ease ease, params Action[] actions) // �ð� ������Ʈ
    {
        clockHandImgTrm.DOLocalRotate(vector, targetTime).SetEase(ease).OnComplete(() =>
        {
            foreach (var action in actions) //������ �Լ��� �ִٸ� ����
            {
                action?.Invoke();
            }
        });
    }

    private void SpawnEnemy()
    {
        // �� ����
        Debug.Log("�� ����");
    }

    public void InvokePhaseStartEvent() // ���������� ���� �̺�Ʈ ����� �Լ�
    {
        OnPhaseStartEvent?.Invoke();
    }

    public void InvokePhaseEndEvent(bool _isWin) // ���������� ���� �̺�Ʈ ����� �Լ�
    {
        isWin = _isWin;
        OnPhaseEndEvent?.Invoke();
    }

    private void OnDisable()
    {
        OnPhaseStartEvent -= OnPhaseStartHandle;
        OnPhaseEndEvent -= OnPhaseEndHandle;
    }

    public void SetCurrentEnemyGround(GameObject ice) // ���߿� �ٲܵ�
    {
        _currentEnemyGround = ice;
    }
}
