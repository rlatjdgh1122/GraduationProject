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

    public override void Awake()
    {
        _maskingImage = FindObjectOfType<MaskingImage>();

        SignalHub.OnTutorialArrowSignEvent += SetMaskingImagePos;
        maskingUIIdx = 0;
    }

    private void SetMaskingImagePos(Transform OnTrm) // �������� �ϴ� �̵����� ������ó�� �� 
    {
        maskingUIIdx++;

        UIManager.Instance.ShowPanel("Masking");

        Vector3 onPos = OnTrm.position;

        if (!OnTrm.TryGetComponent(out RectTransform rect)) // ui�� �ƴϸ�
        {
            onPos = Camera.main.WorldToScreenPoint(OnTrm.position);
            SignalHub.OnDefaultBuilingClickEvent += OffMaskingImageObj;

        }
        else // UI ��
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
