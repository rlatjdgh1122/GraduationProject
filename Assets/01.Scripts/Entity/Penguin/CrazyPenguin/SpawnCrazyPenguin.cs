using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrazyPenguin : MonoBehaviour
{
    [SerializeField]
    private CrazyPenguin _penguinPrefab;

    [SerializeField]
    private Transform _targetTransform;

    private void Start()
    {
        SignalHub.OnBattlePhaseStartEvent += StartStage;
    }

    public void StartStage()
    {
        CrazyPenguin penguin = Instantiate(_penguinPrefab);
        penguin.transform.position = transform.position;
        penguin.CreatePenguin(transform, _targetTransform);
    }
}
