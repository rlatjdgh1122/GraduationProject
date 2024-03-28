using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private SoldierListSO soldierTypeListSO = null;

    #region ���� ��� ����
    private List<DummyPenguin> _dummyPenguins = new();
    public int DummyPenguinCount => _dummyPenguins.Count;
    #endregion

    private Dictionary<PenguinTypeEnum, Penguin> soldierTypeDictionary = new();
    public override void Awake()
    {
        foreach (var solider in soldierTypeListSO.soldierTypes)
        {
            var type = solider.type;
            soldierTypeDictionary.Add(type, solider.obj);
        }
    }

    /// <summary>
    /// �ر� �����ϴ� �Լ�
    /// </summary>
    /// <typeparam name="T"> Penguin ��ӹ��� �ֵ�</typeparam>
    /// <param name="SpawnPoint"> ���� ��ġ</param>
    /// <param name="seatPos"> ��ġ ��ġ *�ǵ����̸� ������� ����*</param>
    /// <returns> �ϰ� ���� ���</returns>
    public Penguin SpawnSoldier(PenguinTypeEnum type, Vector3 SpawnPoint, Vector3 seatPos = default)
    {
        Penguin obj;
        var prefab = ReturnPenguinByType(type);

        obj = PoolManager.Instance.Pop(prefab.name) as Penguin;
        obj.gameObject.SetActive(false);
        obj.transform.position = SpawnPoint;
        obj.SeatPos = seatPos;
        return obj;
    }
    private Penguin ReturnPenguinByType(PenguinTypeEnum type)
    {
        if (soldierTypeDictionary.TryGetValue(type, out var value)) return value;

        Debug.Log("�ش��ϴ� Ÿ���� ������Ʈ�� �����ϴ�. SoldierListSO�� Ȯ�����ּ���.");
        return null;
    }
    public void GetDummyPenguinList(List<DummyPenguin> dummyPenguinList)
    {
        _dummyPenguins.AddRange(dummyPenguinList);
    }

}
