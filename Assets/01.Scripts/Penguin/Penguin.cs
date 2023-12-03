using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Penguin : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 12f;
    public float attackDelay = 0.5f;
    public float attackTime = 0.5f;

    public bool IsClickToMoving = false;
    protected bool _isDead = false;

    public bool IsInside => Vector3.Distance(transform.position, target.position) <= innerDistance;
    public bool AttackInable => Vector3.Distance(transform.position, target.position) <= attackDistance;

    [SerializeField] private InputReader _inputReader;
    public InputReader Input => _inputReader;


    protected override void Awake()
    {
        base.Awake();
    }

    public override void Attack()
    {
        base.Attack();
    }

    protected override void HandleDie()
    {
        //Á×¾úÀ»¶§ ¹¹ÇØÁÙÁö
        Debug.Log("Áê±İ");
        _isDead = true;
    }
}
