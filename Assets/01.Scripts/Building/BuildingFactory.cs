using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingFactory : EntityFactory<BaseBuilding>
{
    public void SpawnBuildingHandler(BaseBuilding building)
    {
        BaseBuilding spawnPenguin = SpawnObject(building, Input.mousePosition) as BaseBuilding;  //�Ű������� �޾ƿ� Building�� �����Ѵ�
    }

    protected override PoolableMono Create(BaseBuilding type)
    {
        string originalString = type.ToString();
        // �����鿡 �ִ� ��ũ��Ʈ�� �̸��� �������Ƿ� �ʿ��� �κи� �����´�. (ex: MeleePenguin (MeleePenguin) ���� (MeleePenguin)����.)
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        BaseBuilding spawnBuilding = PoolManager.Instance.Pop(resultString) as BaseBuilding;
        return spawnBuilding;
    }
}
