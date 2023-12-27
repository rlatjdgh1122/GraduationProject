using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public abstract class EntityFactory<T>: MonoBehaviour
{
    // �ܺο��� ȣ���ϴ� �Լ�  ���丮���ο��� ó���Ҽ��ִ� ��� ó���� ���⼭����.
    public void SpawnPenguin(T type, Transform spawnTrm, Vector3? setVec = null)
    {
        Entity entity = this.Create(type, spawnTrm, setVec);
        entity.transform.position = spawnTrm.position;
        entity.transform.rotation = Quaternion.identity;

        if (setVec != null)
        {
            entity.SetFirstPosition((Vector3)setVec);
        }

        Debug.Log(entity.transform.position);
    }

    protected abstract Entity Create(T _type, Transform spawnTrm, Vector3? setVec = null);

}
