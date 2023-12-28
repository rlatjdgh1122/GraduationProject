using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public abstract class EntityFactory<T>: MonoBehaviour
{
    // �ܺο��� ȣ���ϴ� �Լ� ���丮���ο��� ó���Ҽ��ִ� ��� ó���� ���⼭����.
    public Entity SpawnPenguin(T type, Transform spawnTrm)
    {
        Entity entity = this.Create(type, spawnTrm);
        entity.transform.position = spawnTrm.position;
        entity.transform.rotation = Quaternion.identity;

        return entity;
    }

    protected abstract Entity Create(T _type, Transform spawnTrm); //������ ���丮���� ������ �Ѵ�.

}
