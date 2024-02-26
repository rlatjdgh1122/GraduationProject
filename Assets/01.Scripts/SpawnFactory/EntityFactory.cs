using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;


public abstract class EntityFactory<T>: MonoBehaviour
{
    public TextMeshProUGUI SpawnFailHudText;

    // �ܺο��� ȣ���ϴ� �Լ� ���丮���ο��� ó���Ҽ��ִ� ��� ó���� ���⼭����.
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

    protected abstract PoolableMono Create(T _type); //������ ���丮���� ������ �Ѵ�.

}
