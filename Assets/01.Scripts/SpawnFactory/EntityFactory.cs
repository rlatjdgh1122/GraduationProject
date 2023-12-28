using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public abstract class EntityFactory<T>: MonoBehaviour
{
    // 외부에서 호출하는 함수 팩토리내부에서 처리할수있는 모든 처리는 여기서하자.
    public Entity SpawnPenguin(T type, Transform spawnTrm)
    {
        Entity entity = this.Create(type, spawnTrm);
        entity.transform.position = spawnTrm.position;
        entity.transform.rotation = Quaternion.identity;

        return entity;
    }

    protected abstract Entity Create(T _type, Transform spawnTrm); //각각의 팩토리에서 재정의 한다.

}
