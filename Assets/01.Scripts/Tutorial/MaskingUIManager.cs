using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MaskingUIManager : Singleton<MaskingUIManager>
{
    private MaskingImage _maskingImage;
    public MaskingImage MaskingImageCompo => _maskingImage;

    private Transform _prevMaskingUiTrms;
    private Transform _prevMaskingUiParentTrms;

    private int maskingUIIdx;

    [SerializeField]
    private List<TransformMaskPointsByQuest> pointsByQuests = new List<TransformMaskPointsByQuest>();

    Queue<string> pointsByQuestsQueue = new Queue<string>();

    public override void Awake()
    {
        _maskingImage = FindObjectOfType<MaskingImage>();

        SetMaskingImagePos(null);
        maskingUIIdx = 0;

        CoroutineUtil.CallWaitForSeconds(0.5f, null, () => UIManager.Instance.HidePanel("Masking"));
    }

    public void SetMaskingImagePos(Transform _OnTrm) // �������� �ϴ� �̵����� ������ó�� �� 
    {
        if(pointsByQuestsQueue.Count == 0)
        {
            Debug.Log("?!");
            foreach(var trm in pointsByQuests[maskingUIIdx].MaskPointTransforms)
            {
                pointsByQuestsQueue.Enqueue(trm);
            }
            maskingUIIdx++;
        }

        CoroutineUtil.CallWaitForSeconds(0.1f, null, () => UIManager.Instance.ShowPanel("Masking", true));

        Transform OnTrm = GameObject.Find(pointsByQuestsQueue.Dequeue().ToString()).transform;

        Vector3 onPos = OnTrm.position;

        if (!OnTrm.TryGetComponent(out RectTransform rect)) // ui�� �ƴϸ�
        {
            onPos = Camera.main.WorldToScreenPoint(OnTrm.position);
            SignalHub.OnDefaultBuilingClickEvent += OffMaskingImageObj;

        }
        else // UI ��
        {
            _prevMaskingUiTrms = OnTrm;
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

        _prevMaskingUiTrms.GetComponent<Button>().onClick.RemoveListener(OffMaskingImageUI);

        OffMask();
    }

    public void OffMaskingImageUI()
    {
        _prevMaskingUiTrms.SetParent(_prevMaskingUiParentTrms);

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

        if (pointsByQuestsQueue.Count != 0)
        {
            SetMaskingImagePos(null);
        }
    }

    public bool IsArrowSignPoint(int idx) { return maskingUIIdx == idx;}
}
