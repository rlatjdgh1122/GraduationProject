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

    public void ReceiveResourceOnce() //Ķ ������ ��� �ڿ�
    {
        ResourceManager.Instance.AddResource(_resourceData, _receiveCountAtOnce);
    }

    public void RecieveResourceComplete() //�� ĳ�� ��� �ڿ�
    {
        ResourceManager.Instance.AddResource(_resourceData, _receiveCountWhenCompleted);
    }

    public void RemoveResource(int count)
    {
        ResourceManager.Instance.RemoveResource(_resourceData, count);
    }

    private void OnMouseDown() //�ӽ� ����׿�
    {
        ReceiveResourceOnce();
    }
}
