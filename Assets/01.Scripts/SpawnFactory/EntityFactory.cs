using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//최상위 팩토리 클래스. 각 엔티티별로 이 클래스를 상속받아 각자의 팩토리를 만들어준다.
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
