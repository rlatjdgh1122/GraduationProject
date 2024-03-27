using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private SoldierListSO soldierTypeListSO = null;
    private Dictionary<PenguinTypeEnum, Penguin> soldierTypeDictionary = new();

    public override void Awake()
    {
        //이코드 스폰 매니저에서 해줄 거임
        foreach (var solider in soldierTypeListSO.soldierTypes)
        {
            var type = solider.type;
            soldierTypeDictionary.Add(type, solider.obj);
        }
    }

    public Penguin ReturnPenguinByType(PenguinTypeEnum type)
    {
        if (soldierTypeDictionary.TryGetValue(type, out var value)) return value;

        Debug.Log("해당하는 타입의 오브젝트는 없습니다. SoldierListSO를 확인해주세요.");
        return null;
    }
}
