using System;
using System.Collections;
using System.Net.NetworkInformation;
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

    private int curWaveCnt;
    private int WaveCnt => curWaveCnt;

    [SerializeField]
    private float remainingPhaseReadyTime; //  ���� ���� ������ �غ� �ð�
    [SerializeField]
    private float maxPhaseReadyTime; // ������ �غ� �ð�

    [SerializeField]
    private Image timeBox;

    private bool isPhase = false; //���� ������������
    public bool IsPhase => isPhase;

    public event Action OnPhaseStartEvent = null; // ������ ���� �̺�Ʈ
    public event Action OnPhaseEndEvent = null; // ������ ���� �̺�Ʈ

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
        while(remainingPhaseReadyTime >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            remainingPhaseReadyTime -= 1f;
            timeBox.fillAmount = remainingPhaseReadyTime / maxPhaseReadyTime;
        }

        SetReadyTime();
        OnPhaseStartEvent?.Invoke();
    }

    private void SpawnEnemy()
    {
        // �� ����
        Debug.Log("�� ����");
    }

    private void OnDisable()
    {
        OnPhaseStartEvent -= OnPhaseStartHandle;
        OnPhaseEndEvent -= OnPhaseEndHandle;
    }
}
