using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private SoldierListSO soldierTypeListSO = null;
    private Dictionary<PenguinTypeEnum, Penguin> soldierTypeDictionary = new();

    public override void Awake()
    {
        //���ڵ� ���� �Ŵ������� ���� ����
        foreach (var solider in soldierTypeListSO.soldierTypes)
        {
            var type = solider.type;
            soldierTypeDictionary.Add(type, solider.obj);
        }
    }

    public Penguin ReturnPenguinByType(PenguinTypeEnum type)
    {
        if (soldierTypeDictionary.TryGetValue(type, out var value)) return value;

        Debug.Log("�ش��ϴ� Ÿ���� ������Ʈ�� �����ϴ�. SoldierListSO�� Ȯ�����ּ���.");
        return null;
    }
}
