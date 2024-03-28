using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingFactory : EntityFactory<BaseBuilding>
{
    private InstallSystem _installSysytem;

    private void Awake()
    {
        _installSysytem = GetComponent<InstallSystem>();
    }

    public void SpawnBuildingHandler(BaseBuilding building, BuildingItemInfo info)
    {
        BaseBuilding spawnbuilding = SpawnObject(building, Input.mousePosition) as BaseBuilding;  //�Ű������� �޾ƿ� Building�� �����Ѵ�
        _installSysytem.SelectBuilding(spawnbuilding, info);
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
