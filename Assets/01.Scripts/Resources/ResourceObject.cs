using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    [SerializeField] private ResourceDataSO _resourceData;
    [SerializeField] private ResourceStat _resourceStat;

    private string _resourceName;
    public string ResourceName => _resourceName;
    private Sprite _resourceIcon;
    public Sprite ResourceImage => _resourceIcon;

    private int _receiveCountAtOnce;
    public int ReceiveCountAtOnce => _receiveCountAtOnce;
    private int _receiveCountWhenCompleted;
    public int ReceiveCountWhenCompleted => _receiveCountWhenCompleted;

    public Health HealthCompo { get; private set; }

    NormalUI resourceUI
    {
        get
        {
            UIManager.Instance.overlayUIDictionary.TryGetValue(UIType.Resource, out NormalUI resourceUI);
            return resourceUI;
        }
    }

    private void Awake()
    {
        HealthCompo = GetComponent<Health>();

        HealthCompo.SetHealth(_resourceStat);
        SetReceiveCount();
    }

    public void SetReceiveCount()
    {
        _resourceName = _resourceStat.resourceName;
        _resourceIcon = _resourceData.resourceIcon;
        _receiveCountAtOnce = _resourceStat.receiveCountAtOnce;
        _receiveCountWhenCompleted = _resourceStat.receiveCountWhenCompleted;
    }    

    public void ReceiveResourceOnce() //캘 때마다 얻는 자원
    {
        ResourceManager.Instance.AddResource(_resourceData, _receiveCountAtOnce);
    }

    public void RecieveResourceComplete() //다 캐면 얻는 자원
    {
        ResourceManager.Instance.AddResource(_resourceData, _receiveCountWhenCompleted);
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
}
