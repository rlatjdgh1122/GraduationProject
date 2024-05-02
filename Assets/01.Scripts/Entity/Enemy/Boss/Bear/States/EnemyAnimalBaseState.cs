using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimalBaseState : EnemyBaseState
{

    public EnemyAnimalBaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
       // _animalAttack = enemy.GetComponent<AnimalAttackableEntity>();
    }

    

    /*protected void AttackComboExit()
    {
        ++_animalAttack.ComboCounter;
        _animalAttack.LastAttackTime = Time.time;

        _enemy.AnimatorCompo.speed = 1;
    }*/
}
