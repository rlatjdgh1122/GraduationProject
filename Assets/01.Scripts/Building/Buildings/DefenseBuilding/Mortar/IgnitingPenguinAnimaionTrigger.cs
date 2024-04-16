using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IgnitingPenguinAnimaionTrigger : MonoBehaviour
{
    Animator _animator;

    [SerializeField]
    private GameObject _torch;

    [SerializeField]
    private UnityEvent GetTorchEvent, UseTorchEvent;

    private float animaionLength;

    public float AnimaionLength
    {
        get
        {
            if (animaionLength <= 0)
            {
                animaionLength = _animator.runtimeAnimatorController.animationClips[1].length - 1.5f;
            }
            return animaionLength;
        }
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SetGetTourchAnimation();
        }
    }

    public void SetGetTourchAnimation()
    {
        _animator.SetTrigger("GetTorchTrigger");
    }

    public void GetTorch()
    {
        _torch.SetActive(true);
        GetTorchEvent?.Invoke();
    }

    public void UseTorch()
    {
        _torch.SetActive(false);
        UseTorchEvent?.Invoke();
    }
}
