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
    private GameObject[] _fans;

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

    public void StartSwingAnimation()
    {
        for(int i = 0; i < _fans.Length; i++)
        {
            _fans[i].SetActive(true);
        }

        _animator.SetBool("IsSwinging", true);
    }

    public void StopSwingAnimation()
    {
        for (int i = 0; i < _fans.Length; i++)
        {
            _fans[i].SetActive(false);
        }
        _animator.SetBool("IsSwinging", false);
    }
}
