using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MaskingUIManager : Singleton<MaskingUIManager>
{
    private MaskingImage _maskingImage;

    private Transform _prevMaskingUiTrms;
    private Transform _prevMaskingUiParentTrms;

    private int _maskingTrmIdx = 0;

    private int maskingUIIdx;

    [SerializeField]
    private List<TransformMaskPointsByQuest> pointsByQuests = new List<TransformMaskPointsByQuest>();

    Queue<string> questPointsQueue = new Queue<string>();

    CameraSystem _cameraSystem;

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

    public void SetMaskingImagePos() // 귀찮으니 일단 이따구로 스레기처럼 함 
    {
        if (questPointsQueue.Count == 0)
        {
            foreach (var trm in pointsByQuests[maskingUIIdx++].MaskPointTransforms)
            {
                questPointsQueue.Enqueue(trm);
            }
        }

        CoroutineUtil.CallWaitForSeconds(0.25f, null, () => UIManager.Instance.ShowPanel("Masking", true));

        string points = questPointsQueue.Dequeue();

        Debug.Log(points);


        Transform OnTrm = GameObject.Find(points).transform;

        Vector3 onPos = OnTrm.position;

        if (!OnTrm.TryGetComponent(out RectTransform rect)) // ui가 아니면
        {
            _cameraSystem.Look(new Vector3(onPos.x, 35.55f, onPos.z));

            CoroutineUtil.CallWaitForSeconds(0.1f, null, () =>
            {
                onPos = Camera.main.WorldToScreenPoint(OnTrm.position);
                _maskingImage.transform.position = onPos;
            });
            SignalHub.OnDefaultBuilingClickEvent += OffMaskingImageObj;
        }
        else // UI 면
        {
            _prevMaskingUiTrms = OnTrm;
            _maskingTrmIdx = _prevMaskingUiTrms.GetSiblingIndex(); //부모한테 몇번째 어 그거
            _prevMaskingUiParentTrms = OnTrm.parent;

            OnTrm.transform.SetParent(_maskingImage.ButtonTrm);

            if (OnTrm.TryGetComponent(out Button btn))
            {
                OnTrm.GetComponent<Button>().onClick.AddListener(OffMaskingButtonUI);
            }
            else
            {
                SignalHub.OnClickPenguinSpawnButtonEvent += OffMaskingImageUI;

            }
        }

        _maskingImage.transform.position = onPos;
    }

    public void OffMaskingButtonUI()
    {
        _prevMaskingUiTrms.SetParent(_prevMaskingUiParentTrms);
        _prevMaskingUiTrms.SetSiblingIndex(_maskingTrmIdx);

        _prevMaskingUiTrms.GetComponent<Button>().onClick.RemoveListener(OffMaskingImageUI);

        OffMask();
    }

    public void OffMaskingImageUI()
    {
        _prevMaskingUiTrms.SetParent(_prevMaskingUiParentTrms);
        _prevMaskingUiTrms.SetSiblingIndex(_maskingTrmIdx);

        SignalHub.OnClickPenguinSpawnButtonEvent += OffMaskingImageUI;

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
        }
    }

    public bool IsArrowSignPoint(int idx) { return maskingUIIdx == idx; }

    private void OnDisable()
    {
        SignalHub.OnClickPenguinSpawnButtonEvent -= OffMaskingImageUI;
        SignalHub.OnDefaultBuilingClickEvent -= OffMaskingImageObj;
    }
}
