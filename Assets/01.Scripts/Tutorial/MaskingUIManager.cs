using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskingUIManager : Singleton<MaskingUIManager>
{
    private MaskingImage _maskingImage;
    public MaskingImage MaskingImageCompo => _maskingImage;
    private KeyValuePair<Transform, Button> _prevMaskingUiTrms;

    private int maskingUIIdx;

    [SerializeField]
    private List<Transform> _maskingPointTrms = new List<Transform>();
    private Queue<Transform> _maskingPointTrmsQueue = new Queue<Transform>();

    public override void Awake()
    {
        _maskingImage = FindObjectOfType<MaskingImage>();

        SignalHub.OnTutorialArrowSignEvent += SetMaskingImagePos;
        maskingUIIdx = 0;

        foreach (Transform t in _maskingPointTrms)
        {
            _maskingPointTrmsQueue.Enqueue(t);
        }
    }

    private void SetMaskingImagePos(Transform _OnTrm) // 귀찮으니 일단 이따구로 스레기처럼 함 
    {
        maskingUIIdx++;

        UIManager.Instance.ShowPanel("Masking");

        Transform OnTrm = _maskingPointTrmsQueue.Dequeue();

        Vector3 onPos = OnTrm.position;

        if (!OnTrm.TryGetComponent(out RectTransform rect)) // ui가 아니면
        {
            onPos = Camera.main.WorldToScreenPoint(OnTrm.position);
            SignalHub.OnDefaultBuilingClickEvent += OffMaskingImageObj;

        }
        else // UI 면
        {
            OnTrm.transform.SetParent(_maskingImage.ButtonTrm);
            _prevMaskingUiTrms = new KeyValuePair<Transform, Button>(OnTrm, OnTrm.GetComponent<Button>());
            _prevMaskingUiTrms.Value.onClick.AddListener(OffMaskingImageUI);
        }

        _maskingImage.transform.position = onPos;
    }

    private void OffMaskingImageUI()
    {
        _prevMaskingUiTrms.Key.SetParent(_prevMaskingUiTrms.Key.parent);

        _prevMaskingUiTrms.Value.onClick.RemoveListener(OffMaskingImageUI);

        UIManager.Instance.HidePanel("Masking");
    }

    private void OffMaskingImageObj()
    {
        SignalHub.OnDefaultBuilingClickEvent -= OffMaskingImageObj;

        UIManager.Instance.HidePanel("Masking");
    }

    private void OnDisable()
    {
        SignalHub.OnTutorialArrowSignEvent -= SetMaskingImagePos;
    }

    public bool IsArrowSignPoint(int idx) { return maskingUIIdx == idx;}
}
