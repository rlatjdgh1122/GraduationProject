using UnityEngine;

public class ResourceObject : Entity
{
    [Header("Resource Info")]
    [SerializeField] private ResourceDataSO _resourceData;
    [SerializeField] private ResourceStat _resourceStat;

    private Sprite _resourceIcon;
    private string _resourceName;
    private int _needWorkerCount;
    private int _currentWorkerCount;
    private int _receiveCountAtOnce;
    private int _receiveCountWhenCompleted;

    #region property
    public Sprite ResourceImage => _resourceIcon;
    public string ResourceName => _resourceName;
    public int NeedWorkerCount => _needWorkerCount;
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

        //HealthCompo.SetHealth(_resourceStat);
        SetCount();
    }

    public void SetCount()
    {
        _needWorkerCount = _resourceStat.requiredWorkerCount;
        _resourceName = _resourceStat.resourceName;
        _resourceIcon = _resourceData.resourceIcon;
        _receiveCountAtOnce = _resourceStat.receiveCountAtOnce;
        _receiveCountWhenCompleted = _resourceStat.receiveCountWhenCompleted;
    }    

    public void ReceiveResourceOnce() //Ķ ������ ��� �ڿ�
    {
        ResourceManager.Instance.AddResource(_resourceData, _receiveCountAtOnce);
    }

    public void RecieveResourceComplete() //�� ĳ�� ����
    {
        ResourceManager.Instance.AddResource(_resourceData, _receiveCountWhenCompleted);
        WorkerManager.Instance.ReturnWorkers(this);
        gameObject.SetActive(false);
    }

    public void RemoveResource(int count)
    {
        ResourceManager.Instance.RemoveResource(_resourceData, count);
    }

    private void OnMouseDown() 
    {
        if (!WaveManager.Instance.IsPhase)
        {
            resourceUI.EnableUI(1f, this);
        }
    }

    protected override void HandleDie()
    {
        RecieveResourceComplete();
    }
}
