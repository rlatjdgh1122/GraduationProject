using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// 죽는걸 Dead상태로 처리하면 호출 순서 문제로
// DeadTarget에서 타겟을 찾을 때 죽은 엔티티도 찾는 문제가 생김
public abstract class EntityDeadController<T> : PoolableMono, IDeadable
    where T : Entity
{
    protected readonly int HASH_DEAD = Animator.StringToHash("Dead");

    protected T _owner;

    protected Animator _anim;
    protected NavMeshAgent _agent;
    protected Collider _collider;

    protected virtual void Awake()
    {
        _owner = GetComponent<T>();
        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        _anim = transform.Find("Visual").GetComponent<Animator>();
    }

    public virtual void OnDied()
    {

        // 모든 bool 변수를 순회하며 비활성화하기
        var parameters = _anim.parameters;
        foreach (var param in parameters)
            _anim.SetBool(param.name, false);

        _anim.speed = 1f;

        //죽는 애니메이션 처리
        _anim.SetBool(HASH_DEAD, true);
        //엔티티 네브메쉬와 콜라이더 꺼줌
        _agent.enabled = false;
        _collider.enabled = false;
        //엔티티 죽음 처리
        _owner.IsDead = true;
        //엔티티 스크립트 꺼줌
        _owner.enabled = false;

        SignalHub.OnBattlePhaseEndEvent += PushObj;
    }

    public virtual void OnResurrected()
    {

        _owner.gameObject.SetActive(true);
        //체력 다시 채움
        _owner.HealthCompo.SetHealth(_owner.Stat);

        _anim.speed = 1f;
        //애니메이션 켜줌
        _anim.enabled = true;
        //죽는 애니메이션 꺼줌
        _anim.SetBool(HASH_DEAD, false);
        //엔티티 네브메쉬와 콜라이더 켜줌
        _collider.enabled = true;
        _agent.enabled = true;
        //엔티티 살려줌
        _owner.IsDead = false;
        //엔티티 스크립트 켜줌
        _owner.enabled = true;
    }

    protected virtual void PushObj()
    {
        PoolManager.Instance.Push(_owner);
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= PushObj;
    }
}
