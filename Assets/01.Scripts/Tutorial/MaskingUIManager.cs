using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MaskingUIManager : Singleton<MaskingUIManager>
{
    private MaskingImage _maskingImage;
    public MaskingImage MaskingImageCompo => _maskingImage;
    private KeyValuePair<Transform, Button> _prevMaskingUiTrms;

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

    private void SetMaskingImagePos(Transform _OnTrm) // 귀찮으니 일단 이따구로 스레기처럼 함 
    {
        if(pointsByQuestsQueue.Count == 0)
        {
            foreach(var trm in pointsByQuests[TutorialManager.Instance.CurTutoQuestIdx].MaskPointTransforms)
            {
                pointsByQuestsQueue.Enqueue(trm);
            }
        }

        CoroutineUtil.CallWaitForSeconds(0.1f, null, () => UIManager.Instance.ShowPanel("Masking", true));

        maskingUIIdx++;
        Transform OnTrm = GameObject.Find(pointsByQuestsQueue.Dequeue().ToString()).transform;

        Vector3 onPos = OnTrm.position;

        if (!OnTrm.TryGetComponent(out RectTransform rect)) // ui가 아니면
        {
            onPos = Camera.main.WorldToScreenPoint(OnTrm.position);
            SignalHub.OnDefaultBuilingClickEvent += OffMaskingImageObj;

        }
        else // UI 면
        {
            OnTrm.transform.SetParent(_maskingImage.ButtonTrm);
            _prevMaskingUiTrms = new KeyValuePair<Transform, Button>(OnTrm, OnTrm.GetOrAddComponent<Button>());
            _prevMaskingUiTrms.Value.onClick.AddListener(OffMaskingImageUI);
        }

        _maskingImage.transform.position = onPos;
    }

    private void OffMaskingImageUI()
    {
        _prevMaskingUiTrms.Key.SetParent(_prevMaskingUiTrms.Key.parent);

        _prevMaskingUiTrms.Value.onClick.RemoveListener(OffMaskingImageUI);

        if (pointsByQuestsQueue.Count != 0)
        {
            SetMaskingImagePos(null);
        }
        else
        {
            UIManager.Instance.HidePanel("Masking");
        }

    }

    private void OffMaskingImageObj()
    {
        SignalHub.OnDefaultBuilingClickEvent -= OffMaskingImageObj;

        if (pointsByQuestsQueue.Count != 0)
        {
            SetMaskingImagePos(null);
        }
        else
        {
            UIManager.Instance.HidePanel("Masking");
        }
    }

    public bool IsArrowSignPoint(int idx) { return maskingUIIdx == idx;}
}
