using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingFactory : EntityFactory<BaseBuilding>
{
    private InstallSysytem _installSysytem;

    private void Awake()
    {
        _installSysytem = GetComponent<InstallSysytem>();
    }

    public void SpawnBuildingHandler(BaseBuilding building)
    {
        BaseBuilding spawnbuilding = SpawnObject(building, Input.mousePosition) as BaseBuilding;  //매개변수로 받아온 Building을 생성한다
        _installSysytem.SelectBuilding(spawnbuilding);
    }

    protected override PoolableMono Create(BaseBuilding type)
    {
        string originalString = type.ToString();
        // 프리펩에 있는 스크립트의 이름을 가져오므로 필요한 부분만 가져온다. (ex: MeleePenguin (MeleePenguin) 에서 (MeleePenguin)제거.)
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        BaseBuilding spawnBuilding = PoolManager.Instance.Pop(resultString) as BaseBuilding;
        return spawnBuilding;
    }
}
