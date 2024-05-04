using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrazyPenguin : MonoBehaviour
{
    [SerializeField] Transform _targetPos;
    [SerializeField] Transform _nexusPos;

    NavMeshAgent _agent;
    Animator _anim;

    private bool IsFirst = true;
    private bool CanIn = false;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        MoveToTarget();
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, _targetPos.transform.position) <= 0.5f && IsFirst)
        {
            _anim.SetBool("Run", false);
            _anim.SetBool("FallDown", true);
            IsFirst = false;
            CanIn = true;
        }

        if(Vector3.Distance(transform.position,_nexusPos.transform.position) <= 0.5f && CanIn)
        {
            gameObject.SetActive(false);
            IsFirst = true;
            CanIn = false;
        }
    }

    private void MoveToTarget()
    {
        _agent.SetDestination(_targetPos.transform.position);
    }

    private void ReturnToNexus()
    {
        _agent.SetDestination(_nexusPos.transform.position);
    }

    public void AnimationEndTrigger()
    {
        _anim.SetBool("FallDown", false);
        _anim.SetBool("Run", true);
        ReturnToNexus();
    }
}
