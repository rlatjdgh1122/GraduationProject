using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//�ֻ��� ���丮 Ŭ����. �� ��ƼƼ���� �� Ŭ������ ��ӹ޾� ������ ���丮�� ������ش�.
public abstract class EntityFactory : MonoBehaviour
{
    public Penguin CreatePenguin()
    {
        Penguin penguin = new Penguin();
        return penguin;
    }

    public DummyPenguin CreateDummyPenguin()
    {
        DummyPenguin duumyPenguin = new DummyPenguin();
        return duumyPenguin;
    }

    public Enemy CreateEnemy()
    {
        Enemy enemy = new Enemy();
        return enemy;
    }

    public abstract Penguin CreateMonster();
    //public abstract Weapon CreateWeapon();
}
