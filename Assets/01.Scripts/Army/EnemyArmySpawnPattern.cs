using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmySpawnPattern : MonoBehaviour
{
    [SerializeField]
    private Transform[] _enemyArmySpawnPoints;
    public Transform[] EnemyArmySpawnPoints => _enemyArmySpawnPoints;
}
