using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTowerPenguinAnimationTrigger : MonoBehaviour
{
    private SignalTowerPenguin _penguin;

    private void Awake()
    {
        _penguin = transform.parent.GetComponent<SignalTowerPenguin>();
    }

    public void AnimationFinishTrigger()
    {
        _penguin.AnimationEndTrigger();
    }
}
