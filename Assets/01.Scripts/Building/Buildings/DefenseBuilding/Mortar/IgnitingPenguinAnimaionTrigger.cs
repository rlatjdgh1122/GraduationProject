using AssetKits.ParticleImage;
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

    [SerializeField]
    private ExclamationMarkParticle _exclamationMarkParticle;

    private float animaionLength;

    private bool isSwinging;
    private float remainWaitTime;

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
        isSwinging = false;
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

    public void StartSwingAnimation(float remainTime)
    {
        _animator.SetBool("IsSwinging", true);
        remainWaitTime = remainTime;
    }

    public void StartSwingAnimaionEvent()
    {
        for (int i = 0; i < _fans.Length; i++)
        {
            _fans[i].SetActive(true);
        }

        if (!isSwinging)
        {
            _exclamationMarkParticle.Play(transform.position);
            isSwinging = true;
        }

        CoroutineUtil.CallWaitForSeconds(remainWaitTime + 1f, StopSwingAnimation);
    }

    public void StopSwingAnimation()
    {
        _animator.SetBool("IsSwinging", false);
        for (int i = 0; i < _fans.Length; i++)
        {
            _fans[i].SetActive(false);
        }
        isSwinging = false;
    }
}
