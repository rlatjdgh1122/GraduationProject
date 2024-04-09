using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSwapHandler : MonoBehaviour
{
    private void Awake()
    {
        SignalHub.OnBattlePhaseStartEvent += DummyToPenguinSwapHandler;
        SignalHub.OnBattlePhaseEndEvent += PenguinToDummySwapHandler;
    }

    private void PenguinToDummySwapHandler()
    {

    }
    private void DummyToPenguinSwapHandler()
    {

    }
    private void OnDestroy()
    {
        SignalHub.OnBattlePhaseStartEvent -= DummyToPenguinSwapHandler;
        SignalHub.OnBattlePhaseEndEvent -= PenguinToDummySwapHandler;
    }
}
