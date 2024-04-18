using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : BaseTrap
{
    Animator _animator;

    public override void Init()
    {
        base.Init();
        _animator.SetBool("IsCatched", false);
    }

    protected override void Awake()
    {
        base.Awake();
        _animator = transform.Find("Visual").GetComponent<Animator>();
    }

    protected override void CatchEnemy(Enemy enemy)
    {
        Debug.Log($"{enemy} catch");
        _animator.SetBool("IsCatched", true);
    }
}
