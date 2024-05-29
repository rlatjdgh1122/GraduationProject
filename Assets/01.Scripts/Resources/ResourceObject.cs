using UnityEngine;
using UnityEngine.Events;

public class ResourceObject : WorkableObject
{
    [Header("Resource Info")]
    [SerializeField] private ResourceDataSO _resourceData;
    [SerializeField] private UnityEvent OnRecieveResourceEvent = null;
    [SerializeField] private UnityEvent OnReviveInitEvent = null;

    private Sprite _resourceIcon;
    private Sprite _workerIcon;
    private string _resourceName;
    private int _requiredWorkerCount;
    private int _currentWorkerCount;
    private int _receiveCountAtOnce;
    private int _receiveCountWhenCompleted;

    private ResourceStat _resourceStat;
    private IDeadable _deadCompo = null;

    #region property
    public ResourceDataSO ResourceData => _resourceData;
    public Sprite ResourceImage => _resourceIcon;
    public Sprite WorkerIcon => _workerIcon;
    public string ResourceName => _resourceName;
    public int RequiredWorkerCount => _requiredWorkerCount;
    public int CurrentWorkerCount { get { return _currentWorkerCount; } set { _currentWorkerCount = value; } }
    public int ReceiveCountAtOnce => _receiveCountAtOnce;
    public int ReceiveCountWhenCompleted => _receiveCountWhenCompleted;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        _resourceStat = ReturnGenericStat<ResourceStat>();
        _deadCompo = GetComponent<IDeadable>();    
        SetCount();
    }

    public void SetCount()
    {
        _requiredWorkerCount = _resourceStat.requiredWorkerCount;
        _resourceName = _resourceStat.resourceName;
        _workerIcon = _resourceData.workerIcon;
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
        WorkerManager.Instance.ReturnWorker(this);
    }

    public void RemoveResource(int count)
    {
        ResourceManager.Instance.RemoveResource(_resourceData, count);
    }

    protected override void HandleDie()
    {
        RecieveResourceComplete();
        _deadCompo.OnDied();
    }

    public override void Init()
    {
        OnReviveInitEvent?.Invoke();
        _deadCompo.OnResurrected();
    }
}
