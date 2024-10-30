using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceEnemyPenguin : EnemyBasicPenguin
{
    protected override void Start() 
    {
        base.Start();

        foreach (var enemy in MyArmy.Soldiers)
        {
            enemy.StartEffect("Health");
        }    
    }
}
