using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDancePenguinAnimationTrigger : MonoBehaviour
{
    private readonly int AnimID = Animator.StringToHash("RandomValue");

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        RandomDance();
    }

    public void AnimationEndTrigger()
    {
        RandomDance();
    }

    private void RandomDance()
    {
        float randomValue = Random.Range(0, 4);

        _anim.SetFloat(AnimID, randomValue);
    }
}
