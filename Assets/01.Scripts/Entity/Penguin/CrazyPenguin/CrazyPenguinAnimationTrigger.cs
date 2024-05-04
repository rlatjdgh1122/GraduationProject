using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyPenguinAnimationTrigger : MonoBehaviour
{
    CrazyPenguin _penguin;

    private void Awake()
    {
        _penguin = transform.parent.GetComponent<CrazyPenguin>();
    }

    public void AnimationFinishTrigger()
    {
        _penguin.AnimationEndTrigger();
    }
}
