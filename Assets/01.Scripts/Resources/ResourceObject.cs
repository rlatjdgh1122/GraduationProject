using UnityEngine;
using UnityEngine.Events;

public class ResourceObject : WorkableObject
{
    [Header("Resource Info")]
    [SerializeField] private ResourceDataSO _resourceData;
    [SerializeField] private ResourceStat _resourceStat;
    [SerializeField] private UnityEvent OnRecieveResourceEvent = null;
    [SerializeField] private UnityEvent OnReviveInitEvent = null;

    private Sprite _resourceIcon;
    private string _resourceName;
    private int _requiredWorkerCount;
    private int _currentWorkerCount;
    private int _receiveCountAtOnce;
    private int _receiveCountWhenCompleted;

    #region property
    public Sprite ResourceImage => _resourceIcon;
    public string ResourceName => _resourceName;
    public int RequiredWorkerCount => _requiredWorkerCount;
    public int CurrentWorkerCount { get { return _currentWorkerCount; } set { _currentWorkerCount = value; } }   
    public int ReceiveCountAtOnce => _receiveCountAtOnce;
    public int ReceiveCountWhenCompleted => _receiveCountWhenCompleted;
    #endregion

    NormalUI resourceUI
    {
        get
        {
            UIManager.Instance.overlayUIDictionary.TryGetValue(UIType.Resource, out NormalUI resourceUI);
            return resourceUI;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        SetCount();
    }

    public void SetCount()
    {
        _requiredWorkerCount = _resourceStat.requiredWorkerCount;
        _resourceName = _resourceStat.resourceName;
        _resourceIcon = _resourceData.resourceIcon;
        _receiveCountAtOnce = _resourceStat.receiveCountAtOnce;
        _receiveCountWhenCompleted = _resourceStat.receiveCountWhenCompleted;
    }    

    public void ReceiveResourceOnce() //캘 때마다 얻는 자원
    {
        ResourceManager.Instance.AddResource(_resourceData, _receiveCountAtOnce);
    }

    public void RecieveResourceComplete() //다 캐면 실행
    {
        OnRecieveResourceEvent?.Invoke();

        ResourceManager.Instance.AddResource(_resourceData, _receiveCountWhenCompleted);
        WorkerManager.Instance.ReturnWorkers(this);
    }

    public void RemoveResource(int count)
    {
        ResourceManager.Instance.RemoveResource(_resourceData, count);
    }

    private void OnMouseDown() 
    {
        if (!WaveManager.Instance.IsBattlePhase)
        {
            resourceUI.EnableUI(1f, this);
        }
    }

    protected override void HandleDie()
    {
        RecieveResourceComplete();
    }

    public override void Init()
    {
        OnReviveInitEvent?.Invoke();
    }
}
