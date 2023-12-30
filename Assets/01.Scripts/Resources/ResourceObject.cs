using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    [SerializeField] private ResourceDataSO _resourceData;
    [SerializeField] private ResourceStat _resourceStat;

    private int _receiveCountAtOnce;
    private int _receiveCountWhenCompleted;

    public Health HealthCompo { get; private set; }

    private void Awake()
    {
        HealthCompo = GetComponent<Health>();

        HealthCompo.SetHealth(_resourceStat);
        SetReceiveCount();
    }

    public void SetReceiveCount()
    {
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

    private void OnMouseDown() //임시 디버그용
    {
        ReceiveResourceOnce();
    }
}
