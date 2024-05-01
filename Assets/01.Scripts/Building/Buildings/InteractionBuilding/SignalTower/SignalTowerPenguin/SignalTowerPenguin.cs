using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SignalTowerPenguin : MonoBehaviour
{
    [SerializeField] private GameObject _findEnemyParticle;
    [SerializeField] public GameObject Target;

    private Animator _anim;
    private int _stageCount = 0;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += WatchAni;
        SignalHub.OnBattlePhaseEndEvent += IdleAni;
    }

    private void WatchAni()
    {
        _anim.SetBool("Idle", false);
        _anim.SetBool("Watch", true);
        _findEnemyParticle.SetActive(true);
        LookTarget();
    }

    private void IdleAni()
    {
        _anim.SetBool("Idle", true);
        _anim.SetBool("Watch", false);
        _findEnemyParticle.SetActive(false);
    }
    private void LookTarget()
    {
        Vector3 target = Target.transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(target);

        transform.rotation = targetRotation;
    }

    public void AnimationEndTrigger()
    {
        _anim.SetBool("Watch", false);
        _anim.SetBool("Idle", true);
    }
}
