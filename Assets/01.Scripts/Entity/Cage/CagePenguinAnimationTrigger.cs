using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagePenguinAnimationTrigger : MonoBehaviour
{
    private CagePenguin _penguin;

    private void Awake()
    {
        _penguin = transform.parent.GetComponent<CagePenguin>();
    }

    public void EndAnimationTrigger()
    {
        _penguin.AnimationFinishTrigger();
    }
}
