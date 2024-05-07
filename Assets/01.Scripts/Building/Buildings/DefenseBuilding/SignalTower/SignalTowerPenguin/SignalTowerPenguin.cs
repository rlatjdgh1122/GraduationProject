using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SignalTowerPenguin : MonoBehaviour
{
    [SerializeField] private GameObject _findEnemyParticle;
    //[SerializeField] public GameObject Target;
    //지금은 설정되어 있는 빙하를 타겟에 할당해줌, 근데 나중에 랜덤 빙하 만들면 수정
    public List<GameObject> Target = new List<GameObject>();
    
    public int _targetCnt = 0;

    private Animator _anim;

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
        _targetCnt++;
    }

    private void IdleAni()
    {
        _anim.SetBool("Idle", true);
        _anim.SetBool("Watch", false);
        _findEnemyParticle.SetActive(false);
    }
    private void LookTarget()
    {
        Vector3 target = Target[_targetCnt].transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(target);

        transform.rotation = targetRotation;
    }

    public void AnimationEndTrigger()
    {
        _anim.SetBool("Watch", false);
        _anim.SetBool("Idle", true);
    }
}
