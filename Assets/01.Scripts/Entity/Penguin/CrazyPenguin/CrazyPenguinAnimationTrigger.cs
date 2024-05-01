using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrazyPenguinAnimationTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnAnimationTriggerEvent = null;

    [SerializeField]
    private UnityEvent OnAnimationEndTriggerEvent = null;

    private readonly int _jump = Animator.StringToHash("Jump");
    private readonly int _return = Animator.StringToHash("Return");

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void JumpAnim(bool value)
    {
        _anim.SetBool(_jump, value);
    }

    public void ReturnAnim(bool value)
    {
        _anim.SetBool(_return, value);
    }

    public void AnimationTrigger()
    {
        OnAnimationTriggerEvent?.Invoke();
    }

    public void AnimationEndTrigger()
    {
        OnAnimationEndTriggerEvent?.Invoke();
    }
}
