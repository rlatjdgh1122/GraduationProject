using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyArmySpawnPattern")]
public class EnemyArmySpawnPatternsSO : ScriptableObject
{
    [SerializeField]
    private List<EnemyArmySpawnPattern> _enemyArmySpawnPatterns = new List<EnemyArmySpawnPattern>();
    public List<EnemyArmySpawnPattern> EnemyArmySpawnPatterns => _enemyArmySpawnPatterns;
}
