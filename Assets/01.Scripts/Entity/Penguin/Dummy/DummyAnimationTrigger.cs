using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAnimationTrigger : MonoBehaviour
{
    private DummyPenguin _penguin;

    private void Awake()
    {
        _penguin = transform.parent.GetComponent<DummyPenguin>();
    }

    public void EndAnimationTrigger()
    {
        _penguin?.AnimationFinishTrigger();
    }

}
