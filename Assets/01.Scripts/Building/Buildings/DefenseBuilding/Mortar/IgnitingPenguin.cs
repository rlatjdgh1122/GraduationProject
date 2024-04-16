using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitingPenguin : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
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
}
