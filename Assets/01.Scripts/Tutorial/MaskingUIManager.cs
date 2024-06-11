using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MaskingUIManager : Singleton<MaskingUIManager>
{
    private MaskingImage _maskingImage;

    private Transform _curMaskingUiTrms;
    private Transform _prevMaskingUiParentTrms;

    private int _maskingTrmIdx = 0;

    private int maskingUIIdx;

    [SerializeField]
    private List<TransformMaskPointsByQuest> pointsByQuests = new List<TransformMaskPointsByQuest>();

    Queue<string> questPointsQueue = new Queue<string>();

    CameraSystem _cameraSystem;

    private bool isMasking;
    public bool IsMasking => isMasking;

    public override void Awake()
    {
        _maskingImage = FindObjectOfType<MaskingImage>();
        _cameraSystem = FindObjectOfType<CameraSystem>();

        foreach (var trm in pointsByQuests[0].MaskPointTransforms)
        {
            questPointsQueue.Enqueue(trm);
        }

        maskingUIIdx = 1;
    }

    public void SetMaskingImagePos() // 시간없으니 일단 이따구로 스레기처럼 함 
    {
        if (questPointsQueue.Count == 0)
        {
            foreach (var trm in pointsByQuests[maskingUIIdx++].MaskPointTransforms)
            {
                questPointsQueue.Enqueue(trm);
            }
        }

        isMasking = true;

        CoroutineUtil.CallWaitForSeconds(0.1f, () => UIManager.Instance.ShowPanel("Masking"));

        string points = questPointsQueue.Dequeue();

        Debug.Log(points);

        Transform OnTrm = GameObject.Find(points).transform;

        Vector3 onPos = OnTrm.position;

        if (!OnTrm.TryGetComponent(out RectTransform rect)) // ui가 아니면
        {
            _cameraSystem.Look(new Vector3(onPos.x, 35.55f, onPos.z));

            CoroutineUtil.CallWaitForSeconds(0.1f, () =>
            {
                onPos = Camera.main.WorldToScreenPoint(OnTrm.position);
                _maskingImage.transform.position = onPos;
            });
            SignalHub.OnDefaultBuilingClickEvent += OffMaskingImageObj;
        }
        else // UI 면
        {
            _curMaskingUiTrms = OnTrm;
            _maskingTrmIdx = _curMaskingUiTrms.GetSiblingIndex(); //부모한테 몇번째 어 그거
            _prevMaskingUiParentTrms = OnTrm.parent;

            OnTrm.transform.SetParent(_maskingImage.ButtonTrm);

            if (OnTrm.TryGetComponent(out Button btn))
            {
                Button button = OnTrm.GetComponent<Button>();
                button.onClick.AddListener(OffMaskingButtonUI);
            }
            else
            {
                SignalHub.OnClickPenguinSpawnButtonEvent += OffMaskingImageUI;

            }
        }

        _cameraSystem.ResetFOV();
        _maskingImage.transform.position = onPos;
    }
    
    private void OffMaskingUI()
    {
        _curMaskingUiTrms.SetParent(_prevMaskingUiParentTrms);
        _curMaskingUiTrms.SetSiblingIndex(_maskingTrmIdx);
    }

    public void OffMaskingButtonUI()
    {
        OffMaskingUI();

        Button btn = _curMaskingUiTrms.GetComponent<Button>();
        btn.onClick.RemoveListener(OffMaskingButtonUI);

        OffMask();
    }

    public void OffMaskingImageUI()
    {
        OffMaskingUI();

        SignalHub.OnClickPenguinSpawnButtonEvent -= OffMaskingImageUI;

        OffMask();
    }

    private void OffMaskingImageObj()
    {
        SignalHub.OnDefaultBuilingClickEvent -= OffMaskingImageObj;

        OffMask();
    }

    private void OffMask()
    {
        UIManager.Instance.HidePanel("Masking");

        if (questPointsQueue.Count != 0)
        {
            SetMaskingImagePos();
            return;
        }

        isMasking = false;
    }

    public bool IsArrowSignPoint(int idx) { return maskingUIIdx == idx; }

    private void OnDisable()
    {
        SignalHub.OnClickPenguinSpawnButtonEvent -= OffMaskingImageUI;
        SignalHub.OnDefaultBuilingClickEvent -= OffMaskingImageObj;
    }
}
