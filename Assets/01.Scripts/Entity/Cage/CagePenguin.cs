using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagePenguin : MonoBehaviour
{
    [SerializeField] 
    private PenguinTypeEnum _penguinType;
    public PenguinTypeEnum PenguinType => _penguinType;

    private Animator _anim;

    private void Awake()
    {
        _anim = transform.Find("Visual").GetComponent<Animator>();

        _anim.SetBool("FreelyIdle", true);
    }

    public void DestroyCage()
    {
        _anim.SetBool("FreelyIdle", false);

        _anim.SetBool("DumbToDo", true);

        _anim.SetFloat("RandomValue", 0);
    }

    public void AnimationFinishTrigger()
    {
        _anim.gameObject.SetActive(false);
    }
}