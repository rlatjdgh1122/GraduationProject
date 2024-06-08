using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class EnemyMouseEventHandler : MonoBehaviour
{
    [SerializeField] private Enemy _owner = null;

    private EnemyArmy _enemyArmy => _owner.MyArmy;
    private void OnMouseEnter()
    {
        _enemyArmy.OnMouseEnter();
    }

    private void OnMouseExit()
    {
        _enemyArmy.OnMouseExit();
    }

    private void OnMouseDown()
    {
        _enemyArmy.OnSelect();
    }
}
