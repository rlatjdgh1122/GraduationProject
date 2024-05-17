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

    private int maskingUIIdx;

    [SerializeField]
    private List<TransformMaskPointsByQuest> pointsByQuests = new List<TransformMaskPointsByQuest>();

    Queue<string> pointsByQuestsQueue = new Queue<string>();

    public override void Awake()
    {
        _maskingImage = FindObjectOfType<MaskingImage>();

        foreach (var trm in pointsByQuests[0].MaskPointTransforms)
        {
            pointsByQuestsQueue.Enqueue(trm);
        }

        maskingUIIdx = 1;
    }

    public void SetMaskingImagePos() // 귀찮으니 일단 이따구로 스레기처럼 함 
    {
        if(pointsByQuestsQueue.Count == 0)
        {
            foreach(var trm in pointsByQuests[maskingUIIdx++].MaskPointTransforms)
            {
                pointsByQuestsQueue.Enqueue(trm);
            }
        }

        CoroutineUtil.CallWaitForSeconds(0.1f, null, () => UIManager.Instance.ShowPanel("Masking", true));

        Transform OnTrm = GameObject.Find(pointsByQuestsQueue.Dequeue().ToString()).transform;

        Vector3 onPos = OnTrm.position;

        if (!OnTrm.TryGetComponent(out RectTransform rect)) // ui가 아니면
        {
            onPos = Camera.main.WorldToScreenPoint(OnTrm.position);
            SignalHub.OnDefaultBuilingClickEvent += OffMaskingImageObj;

        }
        else // UI 면
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
            SetMaskingImagePos();
        }
    }

    public bool IsArrowSignPoint(int idx) { return maskingUIIdx == idx;}
}
