using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : BaseTrap
{
    Animator _animator;

    public override void Init()
    {
        base.Init(); 
    }

    protected override void Awake()
    {
        base.Awake();
        _animator = transform.Find("Visual").GetComponent<Animator>();
    }

    protected override void CatchEnemy(Enemy enemy)
    {
        Debug.Log($"{enemy} catch");
    }
}
