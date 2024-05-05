using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrazyPenguin : MonoBehaviour
{
    [SerializeField] Transform _targetPos;
    [SerializeField] Transform _nexusPos;
    [SerializeField] private SoundName _fallDownSound = SoundName.FallDown;

    NavMeshAgent _agent;
    Animator _anim;

    private bool _isFirst = true;
    private bool _canIn = false;
    private bool _soundOn = false;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += MoveToTarget;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _targetPos.transform.position) <= 0.5f)
        {
            _canIn = true;
            ReturnToNexus();
        }

        if (Vector3.Distance(transform.position,_nexusPos.transform.position) <= 0.5f && _canIn)
        {
            gameObject.SetActive(false);
            _canIn = false;
        }

        if(_soundOn)
        {
            SoundManager.Play3DSound(_fallDownSound, transform.position);
            _soundOn = false;
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
        _soundOn = true;
    }
}
