using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;


public abstract class EntityFactory<T>: MonoBehaviour
{
    public TextMeshProUGUI SpawnFailHudText;

    // 외부에서 호출하는 함수 팩토리내부에서 처리할수있는 모든 처리는 여기서하자.
    public PoolableMono SpawnObject(T type, Vector3 spawnTrm)
    {
        PoolableMono entity = this.Create(type);
        entity.transform.position = spawnTrm;
        entity.transform.rotation = Quaternion.identity;

        return entity;
    }

    public void SetSpawnFailHudText(string st)
    {
        SpawnFailHudText.SetText(st);
    }

    protected abstract PoolableMono Create(T _type); //각각의 팩토리에서 재정의 한다.

}
